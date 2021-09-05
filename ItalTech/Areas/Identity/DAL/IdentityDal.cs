using ItalTech.Areas.Admin.Models;
using ItalTech.Areas.Admin.Models.RolesGroups;
using ItalTech.Areas.Identity.Data;
using ItalTech.Models;
using ItalTech.Models.Paginate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Identity.Data.DAL
{
    public class IdentityDal : IIdentityDal
    {
        private readonly ItalTechAppContext context;
        private readonly ItalTechContext IdContext;

        public IdentityDal(ItalTechAppContext appContext, ItalTechContext IdContext)
        {
            this.context = appContext;
            this.IdContext = IdContext;
        }

        public async Task<Response<int>> SaveGroupRolesMasterAsync(GroupsMaster gruppo)
        {
            try
            {
                var result = await SaveGroupRolesMaster(gruppo);
                if (result == 0)
                    throw new Exception("result is 0");
                return new Response<int>()
                {
                    IsSucces = true,
                    Result = result
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response<int>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il salvataggio del gruppo di ruoli"
                });
            }
        }

        private async Task<int> SaveGroupRolesMaster(GroupsMaster gruppo)
        {
            try
            {
                GroupsMaster oldGroup = null;
                if (gruppo.Id == 0)
                {
                    oldGroup = await context.GroupsMaster.Where(g => g.Name == gruppo.Name).AsNoTracking().SingleOrDefaultAsync();
                    if (oldGroup != null)
                        throw new Exception($"Gruppo {gruppo.Name} già esistente");
                    context.GroupsMaster.Add(gruppo);
                }
                else
                {
                    var group = await context.GroupsMaster.Where(g => g.Id == gruppo.Id).SingleOrDefaultAsync();
                    if (group == null)
                        throw new Exception($"Gruppo con Id {gruppo.Id} inesistente");
                    if (gruppo.Name != group.Name)
                    {
                        var otherGroup = await context.GroupsMaster.Where(g => g.Name == gruppo.Name).AsNoTracking().SingleOrDefaultAsync();
                        if (otherGroup != null)
                            throw new Exception($"Gruppo {gruppo.Name} già esistente");
                    }
                    group.Name = gruppo.Name;
                    group.Description = gruppo.Description;
                    group.Disabled = gruppo.Disabled;
                }
                await context.SaveChangesAsync();
                return gruppo.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> SaveGroupRolesDetailsAsync(int groupMasterId, List<GroupsDetail> details)
        {
            try
            {
                if (groupMasterId == 0)
                    throw new Exception("groupMasterId is 0");
                await SaveGroupRolesDetails(groupMasterId, details);
                return new Response()
                {
                    IsSucces = true,
                    Message = "Ruoli modificati correttamente"
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il salvataggio dei ruoli del gruppo"
                });
            }
        }

        private async Task SaveGroupRolesDetails(int groupMasterId, List<GroupsDetail> details)
        {
            try
            {
                var detailIds = details.Select(d => d.Id).ToList();
                var toRemoveDetails = context.GroupsDetail.Where(d => d.GroupId == groupMasterId && !detailIds.Contains(d.Id));
                context.RemoveRange(toRemoveDetails);
                context.UpdateRange(details);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response<List<GroupsMaster>>> GetAllGroupRolesAsync()
        {
            try
            {
                var groups = await GetAllGroupRoles();
                return new Response<List<GroupsMaster>>()
                {
                    IsSucces = true,
                    Result = groups
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response<List<GroupsMaster>>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il recupero dei gruppi di ruoli"
                });
            }
        }

        private async Task<List<GroupsMaster>> GetAllGroupRoles()
        {
            try
            {
                var groups = await context.GroupsMaster.AsNoTracking().OrderBy(g => g.Name).ToListAsync();
                return groups;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response<GroupsMaster>> GetGroupRolesWithDetailsAsync(int groupId)
        {
            try
            {
                if (groupId == 0)
                    throw new Exception("groupId is 0");
                var details = await GetGroupRolesWithDetails(groupId);
                return new Response<GroupsMaster>()
                {
                    IsSucces = true,
                    Result = details
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response<GroupsMaster>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il recupero dei ruoli del gruppo"
                });
            }
        }

        private async Task<GroupsMaster> GetGroupRolesWithDetails(int groupId)
        {
            try
            {
                var group = await context.GroupsMaster.AsNoTracking().Where(g => g.Id == groupId).Include(g => g.GroupsDetails).SingleOrDefaultAsync();
                if (group == null)
                    throw new Exception($"None group found for id = {groupId}");
                return group;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response<List<GroupsDetail>>> GetGroupDetailsAsync(List<int> groupIds)
        {
            try
            {
                if (groupIds == null)
                    throw new Exception("groupIds is null");
                var details = await GetGroupDetails(groupIds);
                return new Response<List<GroupsDetail>>()
                {
                    IsSucces = true,
                    Result = details
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response<List<GroupsDetail>>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il recupero dei ruoli del gruppo"
                });
            }
        }

        private async Task<List<GroupsDetail>> GetGroupDetails(List<int> groupIds)
        {
            try
            {
                if (groupIds == null)
                    throw new Exception($"groupIds is null");
                var details = await context.GroupsDetail.AsNoTracking().Where(d => groupIds.Contains(d.GroupId)).ToListAsync();
                return details;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> DeleteGroupRolesAsync(int groupMasterId)
        {
            try
            {
                if (groupMasterId == 0)
                    throw new Exception("groupMasterId is 0");
                await DeleteGroupRoles(groupMasterId);
                return new Response()
                {
                    IsSucces = true
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante la cancellazione del gruppo"
                });
            }
        }

        private async Task DeleteGroupRoles(int groupMasterId)
        {
            try
            {
                var group = await context.GroupsMaster.Where(g => g.Id == groupMasterId).Include(g => g.GroupsDetails).SingleOrDefaultAsync();
                context.Remove(group);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response<List<UserGroups>>> GetUserGroupsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new Exception("userId is null");
                var result = await GetUserGroups(userId);
                return new Response<List<UserGroups>>()
                {
                    IsSucces = true,
                    Result = result
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response<List<UserGroups>>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il recupero dei gruppi associati all'utente"
                });
            }
        }

        private async Task<List<UserGroups>> GetUserGroups(string userId)
        {
            try
            {
                var userGroups = await context.UserGroups.AsNoTracking().Where(u => u.IdUser == userId).ToListAsync();
                return userGroups;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> DeleteUserGroupAsync(string userId, int groupId)
        {
            try
            {
                if (userId == null)
                    throw new Exception("userId is null");
                if (groupId == 0)
                    throw new Exception("groupId is 0");
                await DeleteUserGroup(userId, groupId);
                return new Response()
                {
                    IsSucces = true
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante l'eliminazione di un gruppo per un utente"
                });
            }
        }

        private async Task DeleteUserGroup(string userId, int groupId)
        {
            try
            {
                var userGroup = await context.UserGroups.Where(u => u.IdUser == userId && u.IdGroup == groupId).SingleOrDefaultAsync();
                context.Remove(userGroup);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> SaveUserGroupsAsync(string userId, List<UserGroups> userGroups)
        {
            try
            {
                if (userId == null)
                    throw new Exception("userId is null");
                await SaveUserGroups(userId, userGroups);
                return new Response()
                {
                    IsSucces = true,
                    Message = "Gruppi di ruoli associati correttamente all'utente"
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il salvataggio dei gruppi di ruoli associati all'utente"
                });
            }
        }

        private async Task SaveUserGroups(string userId, List<UserGroups> userGroups)
        {
            try
            {
                var groupId = userGroups.Select(d => d.IdGroup).ToList();
                var toRemove = context.UserGroups.Where(d => d.IdUser == userId && !groupId.Contains(d.IdGroup));
                context.RemoveRange(toRemove);
                context.UpdateRange(userGroups);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response<List<UserRoles>>> GetUserRolesAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new Exception("userId is null");
                var result = await GetUserRoles(userId);
                return new Response<List<UserRoles>>()
                {
                    IsSucces = true,
                    Result = result
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response<List<UserRoles>>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il recupero dei ruoli associati all'utente"
                });
            }
        }

        private async Task<List<UserRoles>> GetUserRoles(string userId)
        {
            try
            {
                var userRoles = await context.UserWithRoles.AsNoTracking().Where(u => u.IdUser == userId).ToListAsync();
                return userRoles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response<List<string>>> GetUsersAssociatedToGroupAsync(int groupId)
        {
            try
            {
                if (groupId == 0)
                    throw new Exception("groupId is 0");
                var users = await GetUsersAssociatedToGroup(groupId);
                return new Response<List<string>>()
                {
                    IsSucces = true,
                    Result = users
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response<List<string>>
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il recupero degli utenti interessati dalla modifica al gruppo"
                });
            }
        }

        private async Task<List<string>> GetUsersAssociatedToGroup(int groupId)
        {
            try
            {
                if (groupId == 0)
                    throw new Exception($"groupId is 0");
                var users = await context.UserGroups.AsNoTracking().Where(u => u.IdGroup == groupId).Select(u => u.IdUser).ToListAsync();
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> SaveUserRolesAsync(string userId, List<UserRoles> userRoles)
        {
            try
            {
                if (userId == null)
                    throw new Exception("userId is null");
                await SaveUserRoles(userId, userRoles);
                return new Response()
                {
                    IsSucces = true,
                    Message = "Ruoli associati correttamente all'utente"
                };
            }
            catch (Exception)
            {
                return await Task.FromResult(new Response
                {
                    IsSucces = false,
                    Message = "Si è verificato un errore durante il salvataggio dei ruoli associati all'utente"
                });
            }
        }

        private async Task SaveUserRoles(string userId, List<UserRoles> userRoles)
        {
            try
            {
                var rolesIds = userRoles.Select(d => d.IdRole).ToList();
                var toRemove = context.UserWithRoles.Where(d => d.IdUser == userId && !rolesIds.Contains(d.IdRole));
                context.RemoveRange(toRemove);
                context.UpdateRange(userRoles);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResultPaginated<UserViewModel>> GetAllUserRolesAsysnc(Pagination pagination = null)
        {

            var allUsersDb = (IQueryable<ItalTechUser>)IdContext.Users.AsQueryable();

            var userName = (string)pagination.ParametriRicerca["username"] ?? "";
            allUsersDb = allUsersDb.Where(u => u.UserName.Contains(userName));
           
            if (pagination != null)
            {
                pagination.TotalItems = allUsersDb.Count();
                switch (pagination.OrderBy)
                {
                    case nameof(UserViewModel.Username):
                        if (pagination.OrderAscending)
                        {
                            allUsersDb = allUsersDb.OrderBy(u => u.UserName);
                        }
                        else
                        {
                            allUsersDb = allUsersDb.OrderByDescending(u => u.UserName);
                        }
                        break;
                    default:
                        allUsersDb = allUsersDb.OrderBy(u => u.UserName);
                        break;
                }
                if (pagination.IsPaginated)
                {
                    var skip = (pagination.CurrentPage - 1) * pagination.ItemsPerPage;
                    allUsersDb =  allUsersDb.Skip(skip).Take(pagination.ItemsPerPage);
                }
            }
            else
            {
                pagination = new Pagination();
            }
            var users = await allUsersDb.ToListAsync();


            pagination.TotalPages = (int)Math.Ceiling(pagination.TotalItems / (decimal)pagination.ItemsPerPage);
            pagination.CurrentPage = (pagination.CurrentPage> pagination.TotalPages)? Math.Max(1,pagination.TotalPages) : Math.Max(1, pagination.CurrentPage);

            var usersWithRoles =  (from user in users
                                        select new UserViewModel
                                        {
                                            UserId = user.Id,
                                            Username = user.UserName,
                                            UserDisabled = user.UserDisabled,
                                            LastLogin = user.LastLogin,
                                            AccountExpired = user.AccountExpiration,
                                            Roles = (from userRoles in IdContext.UserRoles.Where(u=>u.UserId== user.Id)
                                                     join roles in IdContext.Roles on userRoles.RoleId equals roles.Id
                                                     select new RoleViewModel {RoleId = roles.Id , RoleName = roles.Name}).ToList()
                                        }).ToList();


            var result = new ResultPaginated<UserViewModel>()
            {
                Result = usersWithRoles,
                Pagination = pagination
            };

            return result;
        }
        public async Task<UserViewModel> GetRolesByUserId(string userId)
        {
            var rolesUser = await (from user in IdContext.Users.Where(u => u.Id == userId)
                                   join userRoles in IdContext.UserRoles on user.Id equals userRoles.UserId into temp
                                   from x in temp.DefaultIfEmpty()
                                   select new UserViewModel
                                   {
                                       UserId = user.Id,
                                       Username = user.UserName,
                                       UserDisabled = user.UserDisabled,
                                       Roles = x == null ? null : IdContext.Roles.Where(r => r.Id == x.RoleId)
                                         .Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name }).ToList()
                                   })
                .ToListAsync();
            var result = rolesUser.SingleOrDefault();
            return result;
        }
    }
}
