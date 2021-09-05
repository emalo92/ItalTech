using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using ItalTech.Areas.Identity.Data;
using ItalTech.Models;
using ItalTech.ExtensionMethods;
using ItalTech.Models.Paginate;
using ItalTech.Areas.Admin.Models;
using ItalTech.Areas.Admin.Models.RolesGroups;
using Infrastruttura;
using ItalTech.Areas.Identity.Data.DAL;
namespace ItalTech.Manager
{
    public class AdminManager : IAdminManager
    {
        private readonly UserManager<ItalTechUser> _userManager;
        private readonly RoleManager<ItalTechRole> _roleManager;
        public readonly IIdentityDal _identityDal;
        private readonly Config _config;

        public AdminManager(Config config, UserManager<ItalTechUser> userManager, RoleManager<ItalTechRole> roleManager, IIdentityDal identityDal)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityDal = identityDal;
            _config = config;

        }

        /// <summary>
        /// Restituisce quanti giorni dura una password
        /// </summary>


        public HttpContext HttpContext { get; set; }
        /// <summary>
        /// Restituisce il numero di utenti
        /// </summary>
        /// <returns></returns>
        public int GetNumUsers()
        {
            try
            {
                return _userManager.Users.Count();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// Aggiunge dei ruoli ad un utente
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        private async Task<IdentityResult> AddToUserRolesAsync(ItalTechUser user, List<String> roles)
        {
            try
            {
                var result = await _userManager.AddToRolesAsync(user, roles);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Crea un utente con  ruolo Admin e SuperAdmin
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Response<IdentityResult>> CreateAdminUserAsync(string userName, string password)
        {
            try
            {
                var email = userName.GetEmailByUsername(_config.EmailPostfix);
                var user = new ItalTechUser { UserName = userName, Email = email };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var userAdmin = await _userManager.FindByNameAsync(user.UserName);
                    if (userAdmin != null)
                    {
                        result = await AddToUserRolesAsync(userAdmin, new List<string> { RoleNames.SuperAdmin, RoleNames.Amministratore });
                        if (result != null && result.Succeeded)
                        {
                            return new Response<IdentityResult>
                            {
                                IsSucces = result.Succeeded,
                                Message = "Utente creato",
                                Result = result
                            };
                        }
                        try
                        {
                            await _userManager.DeleteAsync(user);
                        }
                        catch (Exception ex)
                        {
                            return new Response<IdentityResult>
                            {
                                IsSucces = false,
                                Message = "Si è verificato un errore durante la creazione dell'utente amministratore"
                            };
                        }
                        return new Response<IdentityResult>
                        {
                            IsSucces = false,
                            Message = result == null ? "Si è verificato un errore in fase di assegnazione del ruolo all'utente Admin" : result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b),
                            Result = null
                        };
                    }
                }
                return new Response<IdentityResult>
                {
                    IsSucces = false,
                    Message = result.Errors.Count() == 0 ? "Si è verificato un errore in fase di creazione dell'utente" : result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b),
                    Result = null
                };
            }
            catch (Exception ex)
            {
                return new Response<IdentityResult>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore in fase di creazione dell'utente",
                    Result = null
                };
            }


        }
        /// <summary>
        /// Crea un utente
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Response<IdentityResult>> CreateUserAsync(string userName, string password)
        {
            try
            {
                var email = userName.GetEmailByUsername(_config.EmailPostfix);
                var user = new ItalTechUser { UserName = userName, Email = email };
                var result = await _userManager.CreateAsync(user, password);
                string error = null;
                if (result.Succeeded)
                {
                }
                else
                {
                    error = result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b);
                }
                return new Response<IdentityResult>
                {
                    IsSucces = result.Succeeded,
                    Message = result.Succeeded ? "Utente creato con successo" : $"Utente non creato: {error}",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new Response<IdentityResult>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore in fase di creazione dell'utente",
                    Result = null
                };
            }
        }

        /// <summary>
        /// Restituisce tutti gli utenti oppure uno specifico su impostato, ognuno con i suoi ruoli
        /// </summary>
        /// <returns></returns>
        public async Task<Response<ResultPaginated<UserViewModel>>> GetAllUserWithRolesAsysnc(Pagination pagination)
        {
            try
            {
                var allUsers = await _identityDal.GetAllUserRolesAsysnc(pagination);
                return new Response<ResultPaginated<UserViewModel>>
                {
                    IsSucces = true,
                    Message = "Lista utenti fornita correttamente",
                    Result = allUsers
                };
            }
            catch (Exception ex)
            {
                return new Response<ResultPaginated<UserViewModel>>
                {
                    IsSucces = false,
                    Message = "Errore durante la restituzione della lista utenti",
                    Result = null
                };
            }
        }

        /// <summary>
        /// Restituisce i ruoli di un utente
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Response<UserViewModel>> GetRolesByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new Exception("userId is null");
                var rolesUser = await _identityDal.GetRolesByUserId(userId);
                if(rolesUser == null)
                    throw new Exception("rolesUser is null");
                return new Response<UserViewModel>
                {
                    IsSucces = true,
                    Message = "Utente fornito correttamente",
                    Result = rolesUser
                };
            }
            catch (Exception ex)
            {
                return new Response<UserViewModel>
                {
                    IsSucces = false,
                    Message = "Errore durante il recupero dei ruoli dell'utente",
                    Result = null
                };
            }
        }

        /// <summary>
        /// Restituisce tutti i ruoli presenti a db
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<RoleViewModel>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleManager.Roles.Where(r => !r.Name.Contains(RoleNames.SuperAdmin) && !r.Name.Contains(RoleNames.Amministratore)).Select(r => new RoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDescription = r.Description,
                    AssociatedMenu = r.Menu,
                    AssociatedSubMenu1 = r.SubMenu1,
                    AssociatedSubMenu2 = r.SubMenu2
                }).OrderBy(r => r.RoleName).ToListAsync();

                var adminAndSuperAdmin = await _roleManager.Roles.Where(r => r.Name.Contains(RoleNames.SuperAdmin) || r.Name.Contains(RoleNames.Amministratore)).Select(r => new RoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDescription = r.Description,
                    AssociatedMenu = r.Menu,
                    AssociatedSubMenu1 = r.SubMenu1,
                    AssociatedSubMenu2 = r.SubMenu2
                }).OrderByDescending(r => r.RoleName).ToListAsync();

                var allRoles = adminAndSuperAdmin.Union(roles).ToList();

                return new Response<List<RoleViewModel>>
                {
                    IsSucces = true,
                    Message = "Lista ruoli fornita correttamente",
                    Result = allRoles
                };
            }
            catch (Exception ex)
            {
                return new Response<List<RoleViewModel>>
                {
                    IsSucces = false,
                    Message = "Errore durante la restituzione della lista ruoli",
                    Result = null
                };
            }
        }

        /// <summary>
        /// Associa dei ruoli ad un utente
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<Response<IdentityResult>> AssociateUserRolesAsync(string userId, List<string> roles)
        {
            try
            {
                var currentUser = HttpContext.Session.GetString("UserId");
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null && currentUser != null)
                {
                    var oldRoles = await _userManager.GetRolesAsync(user);

                    //Nessun utente al di fuori del SuperAdmin può modificare i ruoli del SuperAdmin
                    if (oldRoles.Contains(RoleNames.SuperAdmin) && currentUser != userId)
                    {
                        return new Response<IdentityResult>
                        {
                            IsSucces = false,
                            Message = "Non è consentito modificare i ruoli dell'utente SuperAdmin",
                            Result = null
                        };
                    }
                    // serve per evitare che un utente ADMIN o SUPERADMIN possa rimuovere il suo stesso ruolo e che qualche altro Admin possa togliere i ruoli al SuperAdmin
                    if (currentUser == userId)
                    {
                        oldRoles.Remove(RoleNames.Amministratore);
                        oldRoles.Remove(RoleNames.SuperAdmin);
                    }
                    try
                    {
                        await _userManager.RemoveFromRolesAsync(user, oldRoles);
                    }
                    catch (Exception ex)
                    {
                        return new Response<IdentityResult>
                        {
                            IsSucces = false,
                            Message = "Associazione ruoli fallita",
                            Result = null
                        };
                    }
                    if (currentUser == userId)
                    {
                        roles.Remove(RoleNames.Amministratore);
                        roles.Remove(RoleNames.SuperAdmin);
                    }
                    var result = await AddToUserRolesAsync(user, roles);
                    if (result.Succeeded)
                    {
                        return new Response<IdentityResult>
                        {
                            IsSucces = true,
                            Message = "Ruoli associati con successo",
                            Result = result
                        };
                    }
                    return new Response<IdentityResult>
                    {
                        IsSucces = false,
                        Message = result.Errors.Count() == 0 ? "Associazione ruoli fallita" : result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b),
                        Result = null
                    };
                }
                return new Response<IdentityResult>
                {
                    IsSucces = false,
                    Message = "Associazione ruoli fallita",
                    Result = null
                };
            }
            catch (Exception ex)
            {
                return new Response<IdentityResult>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore in fase di associazione ruoli",
                    Result = null
                };
            }
        }

        /// <summary>
        /// Disabilita/Abilita un utente
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="disabled"></param>
        /// <returns></returns>
        public async Task<Response<bool>> DisableUser(string userId, bool userDisabled)
        {
            try
            {
                var currentUser = HttpContext.Session.GetString("UserId");
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null && currentUser != null)
                {
                    if (currentUser == userId)
                    {
                        return new Response<bool>
                        {
                            IsSucces = false,
                            Message = "Impossibile disabilitare il tuo utente",
                            Result = false
                        };
                    }
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains(RoleNames.SuperAdmin))
                    {
                        return new Response<bool>
                        {
                            IsSucces = false,
                            Message = "Non è consentito disabilitare l'utente con il ruolo di SuperAdmin",
                            Result = false
                        };
                    }
                    user.UserDisabled = userDisabled;
                    await _userManager.UpdateAsync(user);
                    return new Response<bool>
                    {
                        IsSucces = true,
                        Message = (userDisabled) ? "Utente disabilitato" : "Utente abilitato",
                        Result = userDisabled
                    };
                }
                return new Response<bool>
                {
                    IsSucces = false,
                    Message = "Disabilitazione/Abilitazione fallita",
                    Result = false
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore in fase di disabilitazione/abilitazione utente",
                    Result = false
                };
            }
        }

        /// <summary>
        /// Modifica la mail dell'utente
        /// </summary>
        /// <param name="userId">userId dell'utente</param>
        /// <param name="email">Nuova mail</param>
        /// <returns></returns>
        public async Task<Response<IdentityResult>> ChangeEmail(string userId, string email)
        {
            try
            {
                var currentUser = HttpContext.Session.GetString("UserId");
                var user = await _userManager.FindByIdAsync(userId);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(RoleNames.SuperAdmin) && currentUser != userId)
                {
                    return new Response<IdentityResult>
                    {
                        IsSucces = false,
                        Message = "Non è consentito modificare la mail dell'utente SuperAdmin",
                        Result = null
                    };
                }
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, email);
                var result = await _userManager.ChangeEmailAsync(user, email, token);
                return new Response<IdentityResult>
                {
                    IsSucces = result.Succeeded,
                    Message = result.Succeeded ? "Email modificata con successo" : result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b),
                    Result = null
                };
            }
            catch (Exception ex)
            {
                return new Response<IdentityResult>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore in fase di modifica della mail dell'utente",
                    Result = null
                };
            }
        }

        /// <summary>
        /// Recupera l'utente dato il suo userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Response<ItalTechUser>> GetUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                return new Response<ItalTechUser>
                {
                    IsSucces = true,
                    Result = user
                };
            }
            catch (Exception ex)
            {
                return new Response<ItalTechUser>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il recupero delle informazioni utente",
                    Result = null
                };
            }
        }
        
        /// <summary>
        /// Consente di aggiornare le porprietà Email e NextPasswordUpdate di un utente
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Response<ItalTechUser>> UpdateUserDetailsAsync(ItalTechUser user)
        {
            try
            {
                var currentUser = HttpContext.Session.GetString("UserId");
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(RoleNames.SuperAdmin) && currentUser != user.Id)
                {
                    return new Response<ItalTechUser>
                    {
                        IsSucces = false,
                        Message = "Non è consentito modificare i dati dell'utente SuperAdmin",
                        Result = null
                    };
                }
                var responseUser = await GetUser(user.Id);
                if (!responseUser.IsSucces)
                {
                    new Response<ItalTechUser>
                    {
                        IsSucces = responseUser.IsSucces,
                        Message = responseUser.Message,
                        Result = null
                    };
                }
                var oldUser = responseUser.Result;

                if (oldUser.Email != user.Email)
                {
                    var responseChangeEmail = await ChangeEmail(user.Id, user.Email);
                    if (!responseChangeEmail.IsSucces)
                    {
                        new Response<ItalTechUser>
                        {
                            IsSucces = false,
                            Message = responseChangeEmail.Message,
                            Result = null
                        };
                    }
                    responseUser = await GetUser(user.Id);
                    if (!responseUser.IsSucces)
                    {
                        new Response<ItalTechUser>
                        {
                            IsSucces = false,
                            Message = responseUser.Message,
                            Result = null
                        };
                    }
                    oldUser = responseUser.Result; //dopo aver cambiato la mail aggiorno oldUser
                }
                oldUser.AccountExpiration = user.AccountExpiration;
                oldUser.NextPasswordUpdate = user.NextPasswordUpdate;
                var updateResult = await _userManager.UpdateAsync(oldUser);
                return new Response<ItalTechUser>
                {
                    IsSucces = true,
                    Message = "Dati modificati con successo",
                    Result = oldUser
                };
            }
            catch (Exception ex)
            {
                return new Response<ItalTechUser>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore in fase di aggiornamento dell'utente",
                    Result = null
                };
            }
        }

        public async Task<Response<int>> SaveGroupRolesMasterAsync(GroupsMaster gruppo)
        {
            var isToUpdateRoles = gruppo.Id > 0;
            if (gruppo.Id > 0)
            {
                var allGroupsResponse = await _identityDal.GetAllGroupRolesAsync();
                if (allGroupsResponse.IsSucces)
                {
                    isToUpdateRoles = allGroupsResponse.Result.Where(g => g.Id == gruppo.Id).Single().Disabled != gruppo.Disabled; //Se ho modificato il Disabled devo aggiornare i ruoli degli utenti associati
                }
            }
            var response = await _identityDal.SaveGroupRolesMasterAsync(gruppo);
            if (response.IsSucces && isToUpdateRoles)
            {
                var updateRolesResponse = await UpdateUserRolesWithGroup(gruppo.Id);
            }
            return new Response<int>()
            {
                IsSucces = response.IsSucces,
                Message = response.Message,
                Result = response.Result
            };
        }

        public async Task<Response> SaveGroupRolesDetailsAsync(int groupMasterId, List<GroupsDetail> details)
        {
            var response = await _identityDal.SaveGroupRolesDetailsAsync(groupMasterId, details);
            return new Response()
            {
                IsSucces = response.IsSucces,
                Message = response.Message,
            };
        }

        public async Task<Response> SaveGroupRolesDetailsOnlyRolesAsync(int groupMasterId, List<int> roles)
        {
            var oldDetailsResponse = await GetGroupRolesWithDetailsAsync(groupMasterId);
            var oldDetails = new List<GroupsDetail>();  //OldDetails serve per ricavare gli Id dei record dei Details. Se dovesse fallire il recupero degli id non è un problema: invece di aggiornare i record li cancella e li reinserisce
            if (oldDetailsResponse.IsSucces)
            {
                oldDetails = oldDetailsResponse.Result.GroupsDetails.ToList();
            }
            var details = roles.Select(r => new GroupsDetail()
            {
                GroupId = groupMasterId,
                RoleId = r,
                Id = GetSafeDetailId(oldDetails, r)
            }).ToList();

            var response = await SaveGroupRolesDetailsAsync(groupMasterId, details);
            if (!response.IsSucces)
            {
                return new Response()
                {
                    IsSucces = false,
                    Message = response.Message
                };
            }
            return await UpdateUserRolesWithGroup(groupMasterId, isDeleted:false, response.Message);
        }

        private int GetSafeDetailId(List<GroupsDetail> oldDetails, int roleId)
        {
            try
            {
                return oldDetails.Any(o => o.RoleId == roleId) ? 
                    oldDetails.Where(o => o.RoleId == roleId).Select(o => o.Id).Single() : 
                    0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<Response<List<GroupsMaster>>> GetAllGroupRolesAsync()
        {
            var response = await _identityDal.GetAllGroupRolesAsync();
            List<GroupsMaster> groups = new List<GroupsMaster>();
            if (response.IsSucces)
            {
                groups = response.Result;
                foreach (var group in groups)
                {
                    var associatedUsersResponse = await _identityDal.GetUsersAssociatedToGroupAsync(group.Id);
                    group.AssociatedUsers = associatedUsersResponse.IsSucces ? associatedUsersResponse.Result : new List<string>();
                } 
            }
            return new Response<List<GroupsMaster>>()
            {
                IsSucces = response.IsSucces,
                Message = response.Message,
                Result = groups
            };
        }

        public async Task<Response<GroupsMaster>> GetGroupRolesWithDetailsAsync(int groupId)
        {
            var response = await _identityDal.GetGroupRolesWithDetailsAsync(groupId);
            return new Response<GroupsMaster>()
            {
                IsSucces = response.IsSucces,
                Message = response.Message,
                Result = response.Result
            };
        }

        public async Task<Response> DeleteGroupRolesAsync(int groupMasterId)
        {
            var response = await _identityDal.DeleteGroupRolesAsync(groupMasterId);
            if (!response.IsSucces)
            {
                return new Response()
                {
                    IsSucces = false,
                    Message = response.Message
                };
            }
            return await UpdateUserRolesWithGroup(groupMasterId, isDeleted: true, response.Message);
        }

        public async Task<Response<List<UserGroups>>> GetUserGroupsAsync(string userId)
        {
            var response = await _identityDal.GetUserGroupsAsync(userId);
            var responseAllGroupRoles = await GetAllGroupRolesAsync();
            List<UserGroups> groupList = null;
            if (response.IsSucces && responseAllGroupRoles.IsSucces)
            {
                groupList = response.Result;
                foreach (var userGroup in groupList)
                {
                    var group = responseAllGroupRoles.Result.Where(a => a.Id == userGroup.IdGroup).SingleOrDefault();
                    if (group == null)
                    {

                    }
                    else
                    {
                        userGroup.GroupName = group.Name;
                        userGroup.GroupDescription = group.Description;
                    }
                }
            }
            return new Response<List<UserGroups>>()
            {
                IsSucces = response.IsSucces && responseAllGroupRoles.IsSucces,
                Message = !response.IsSucces ? response.Message + " " + responseAllGroupRoles.Message : "",
                Result = groupList
            };
        }

        public async Task<Response> SaveUserGroupsAndRolesAsync(string userId, List<int> userGroupsIds, List<string> userRolesIds)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null && currentUser == null)
            {
                return new Response()
                {
                    IsSucces = false,
                    Message="Si è verificato un errore durante il salvataggio dei ruoli"
                };
            }
            var oldRoles = await _userManager.GetRolesAsync(user);
            //Nessun utente al di fuori del SuperAdmin può modificare i ruoli del SuperAdmin
            if (oldRoles.Contains(RoleNames.SuperAdmin) && currentUser != userId)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = "Non è consentito modificare i ruoli dell'utente SuperAdmin"
                };
            }
            var oldUserGroup = await GetUserGroupsAsync(userId);
            var oldUserGroupIds = oldUserGroup.IsSucces ? oldUserGroup.Result.Select(u => new UserGroups { Id= u.Id, IdGroup = u.IdGroup }).ToList() : new List<UserGroups>();
            var oldUserRoles = await GetUserRolesAsync(userId);
            var oldUserRolesIds = oldUserRoles.IsSucces ? oldUserRoles.Result.Select(r => new UserRoles { Id = r.Id, IdRole = r.IdRole }).ToList() : new List<UserRoles>();

            var userGroups = userGroupsIds.Where(u => u != -1).Select(u => new UserGroups { IdUser = userId, IdGroup = u, Id = GetOldIdForUserGroup(u, oldUserGroupIds) }).ToList();       
            var userRoles = userRolesIds.Where(u => u != "-1").Select(r => new UserRoles { IdUser = userId, IdRole = r, Id = GetOldIdForUserRole(r, oldUserRolesIds) }).ToList();

            var response = await _identityDal.SaveUserGroupsAsync(userId, userGroups);

            response = await _identityDal.SaveUserRolesAsync(userId, userRoles);

            var rolesAssociatedResponse = await UpdateUserRolesForUser(userId, userGroups, userRoles);

            return rolesAssociatedResponse;
        }

        private int GetOldIdForUserGroup(int idGroup, List<UserGroups> oldUserGroup)
        {
            return oldUserGroup.Where(u => u.IdGroup == idGroup).Select(u => u.Id).SingleOrDefault();
        }

        private int GetOldIdForUserRole(string idRole, List<UserRoles> oldUserRole)
        {
            return oldUserRole.Where(r => r.IdRole == idRole).Select(u => u.Id).SingleOrDefault();
        }

        public async Task<Response<List<UserRoles>>> GetUserRolesAsync(string userId)
        {
            var response = await _identityDal.GetUserRolesAsync(userId);
            var allRolesResponse = await GetAllRolesAsync();
            List<UserRoles> roleList = null;
            if (response.IsSucces && allRolesResponse.IsSucces)
            { 
                roleList = response.Result;
                foreach (var role in roleList)
                {
                    var roleEx = allRolesResponse.Result.Where(a => a.RoleId == role.IdRole).SingleOrDefault();
                    if (roleEx == null)
                    {
                    }
                    else
                    {
                        role.RoleDescription = roleEx.RoleDescription;
                        role.RoleName = roleEx.RoleName;
                        role.RoleMenu = roleEx.AssociatedMenu;
                        role.RoleSubMenu1 = roleEx.AssociatedSubMenu1;
                        role.RoleSubMenu2 = roleEx.AssociatedSubMenu2;
                        role.RoleDescription = roleEx.RoleDescription;
                    }
                }
            }
            return new Response<List<UserRoles>>()
            {
                IsSucces = response.IsSucces && allRolesResponse.IsSucces,
                Message = response.Message + " " + allRolesResponse.Message,
                Result = roleList
            };
        }

        /// <summary>
        /// Aggiorna i ruoli della tabella AspNetUserRoles di tutti gli utenti che sono associati al gruppo di ruoli specificato. Da usare quando un gruppo viene modificato
        /// </summary>
        /// <param name="groupMasterId"></param>
        /// <param name="messageSuccess"></param>
        /// <param name="isDeleted">Se true significa che l'update è per una eliminazione del gruppo</param>
        /// <returns></returns>
        public async Task<Response> UpdateUserRolesWithGroup(int groupMasterId, bool isDeleted = false, string messageSuccess = null)
        {

            var associatedUsersResponse = await _identityDal.GetUsersAssociatedToGroupAsync(groupMasterId);
            if (!associatedUsersResponse.IsSucces)
            {
                return new Response()
                {
                    IsSucces = false,
                    Message = associatedUsersResponse.Message
                };
            }
            var userFailedList = new List<string>();
            var isFailed = false;
            var cumulativeErrors = new System.Text.StringBuilder();

            var associatedUsers = associatedUsersResponse.Result.Count;
            foreach (var userId in associatedUsersResponse.Result)
            {
                var userGroupResponse = await GetUserGroupsAsync(userId);
                var userRolesResponse = await GetUserRolesAsync(userId);
                if(!userGroupResponse.IsSucces || !userRolesResponse.IsSucces)
                {
                    isFailed = true;
                    userFailedList.Add(userId);
                    cumulativeErrors.Append($"UserId: {userId}; ");
                    continue;
                }
                var response = await UpdateUserRolesForUser(userId, userGroupResponse.Result, userRolesResponse.Result);
                if (!response.IsSucces)
                {
                    isFailed = true;
                    userFailedList.Add(userId);
                    cumulativeErrors.Append($"UserId: {userId}; ");
                }
                else if(isDeleted)
                {
                    //Bisogna effettuare la dissociazione tra utente e gruppo
                    var deleteResponse = await _identityDal.DeleteUserGroupAsync(userId, groupMasterId);
                    if (!deleteResponse.IsSucces)
                    {
                        isFailed = true;
                    }
                }
            }
            return new Response()
            {
                IsSucces = !isFailed,
                Message = isFailed ? "Si è verificato un errore durante l'aggiornamento degli utenti associati al gruppo modificato" :
                    associatedUsers > 0 ? "Ruoli aggiornati correttamente per tutti gli utenti associati al gruppo modificato" : "Ruoli aggiornati correttamente"
            };
        }

        /// <summary>
        /// Aggiorna i ruoli della tabella AspNetUserRoles in funzione dei gruppi e dei singoli ruoli dell'utente
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userGroups"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public async Task<Response> UpdateUserRolesForUser(string userId, List<UserGroups> userGroups, List<UserRoles> userRoles)
        {

            var allGroupResponse = await GetAllGroupRolesAsync();

            if (!allGroupResponse.IsSucces)
            {
                return new Response()
                {
                    IsSucces = false,
                    Message = allGroupResponse.Message
                };
            }
            var groups = allGroupResponse.Result.Where(g => !g.Disabled && userGroups.Select(u => u.IdGroup).Contains(g.Id)).Select(g => g.Id).ToList();
            var groupsDetailsResponse = await _identityDal.GetGroupDetailsAsync(groups); //Ruoli contenuti nei gruppi associati all'utente
            if (!groupsDetailsResponse.IsSucces)
            {
                return new Response()
                {
                    IsSucces = false,
                    Message = groupsDetailsResponse.Message
                };
            }
            var allRolesResponse = await GetAllRolesAsync();
            if (!allRolesResponse.IsSucces)
            {
                return new Response()
                {
                    IsSucces = false,
                    Message = allRolesResponse.Message
                };
            }
            var roles = (groupsDetailsResponse.Result.Select(g => GetRoleName(g.RoleId.ToString(), allRolesResponse.Result)).Union(userRoles.Select(u => GetRoleName(u.IdRole, allRolesResponse.Result)))).ToList(); //Ruoli da salvare in AspNetUserRoles
            var response = await AssociateUserRolesAsync(userId, roles);
            return new Response()
            {
                IsSucces = response.IsSucces,
                Message = response.Message
            };
        }

        private string GetRoleName(string roleId, List<RoleViewModel> roles)
        {
            return roles.Where(r => r.RoleId == roleId).Select(r => r.RoleName).SingleOrDefault();
        }

        public Task<Response<IdentityResult>> ResetPassword(string userId, string password, bool isSendedEmail)
        {
            throw new NotImplementedException();
        }
    }
}

