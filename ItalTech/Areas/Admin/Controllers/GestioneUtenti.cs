using Infrastruttura.Models;
using ItalTech.Areas.Admin.Models;
using ItalTech.Areas.Admin.Models.RolesGroups;
using ItalTech.Areas.Identity.Data;
using ItalTech.ExtensionMethods;
using ItalTech.Manager;
using ItalTech.Models.Paginate;
using ItalTech.Models.TableToExport;
using ItalTech.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GestioneUtenti : Controller
    {
        private readonly ILogger<GestioneUtenti> log;
        private readonly IAdminManager _adminManager;
  

        public GestioneUtenti(ILogger<GestioneUtenti> logger, IAdminManager adminManager)
        {
            log = logger;
            _adminManager = adminManager;          
        }

        /// <summary>
        /// UserId dell'utente connesso
        /// </summary>
        public string UserId => ControllerContext.HttpContext.Session.GetString("UserId");

        public async Task<IActionResult> Index(string username, int page = 1)
        {
            ViewData["Title"] = "Lista utenti";
            var pagination = new Pagination()
            {
                CurrentPage = page,
                ItemsPerPage = 20,
                Route = new Route
                {                    
                    Area = "Admin",
                    Controller = "GestioneUtenti",
                    Action = "Index",
                },
                ParametriRicerca = new Dictionary<string, dynamic>
                {
                    { "username" , username }
                }
            };
            var response = await _adminManager.GetAllUserWithRolesAsysnc(pagination);
            if (response.IsSucces)
            {
                return View(response.Result);
            }
            ViewMessage.Show(this, response);
            return View(response.Result);
        }
        public IActionResult CreateUser()
        {            
            ViewData["Title"] = "Nuovo utente";
            var inputModel = new RegisterUserViewModel();
            return View(inputModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserPostAsync(RegisterUserViewModel input)
        {            
            if (ModelState.IsValid)
            {
                var response = await _adminManager.CreateUserAsync(input.UserName, input.Password);
                ViewMessage.Show(this, response);
                if (response.IsSucces)
                {                    
                    return RedirectToAction("Index");
                }
            }
            return View("CreateUser", input);
        }
        public async Task<IActionResult> AssociatedRolesAsync(string userId)//, string orderBy = null, bool? isDesc = null)
        {
            _adminManager.HttpContext = HttpContext;
            var userRoles = await _adminManager.GetRolesByUserId(userId);
            ViewBag.AspNetUserRoles = userRoles.Result;
            var userGroups = await _adminManager.GetUserGroupsAsync(userId);
            if (!userGroups.IsSucces)
            {
                ViewMessage.Show(this, userGroups);
            }
            var userSingleRoles = await _adminManager.GetUserRolesAsync(userId);
            if (!userSingleRoles.IsSucces)
            {
                ViewMessage.Show(this, userSingleRoles);
            }
            var allGroupsRoles = await _adminManager.GetAllGroupRolesAsync();
            if (!allGroupsRoles.IsSucces)
            {
                ViewMessage.Show(this, allGroupsRoles);
            }
            ViewBag.UserGroups = userGroups.Result ?? new List<UserGroups>();
            ViewBag.UserSingleRoles = userSingleRoles.Result ?? new List<UserRoles>();
            ViewBag.AllGroupsRoles = allGroupsRoles.Result ?? new List<GroupsMaster>();
            return View();
        }

        public async Task<IActionResult> GetTableAllGroupRolesAsync()
        {
            log.LogInformation("GetTableAllGroupRolesAsync START");
            var allGroupsRoles = await _adminManager.GetAllGroupRolesAsync();
            if (!allGroupsRoles.IsSucces)
            {
                ViewMessage.ShowLocal(this, allGroupsRoles);
                return PartialView("_GenericTable", null);
            }
            var tableAllGroupRoles = GetGenericGroupRolesTable("Gruppi di ruoli", allGroupsRoles.Result, "NotVisible_Id", "Nome", "Descrizione");
            return PartialView("_GenericTable", tableAllGroupRoles);
        }

        public async Task<IActionResult> GetTableAllRolesAsync()
        {
            var allRoles = await _adminManager.GetAllRolesAsync();
            if (!allRoles.IsSucces)
            {
                ViewMessage.ShowLocal(this, allRoles);
                return PartialView("_GenericTable", null);
            }
            var tableAllRoles = GetGenericRolesTable("Lista di ruoli", allRoles.Result.Where(r => r.RoleName != "SuperAdmin").ToList(), "NotVisible_Id", "Nome", "Menu principale", "Primo sottomenu", "Secondo sottomenu", "Descrizione");
            ViewBag.SizeModal = " modal-xl";
            return PartialView("_GenericTable", tableAllRoles);
        }


        public async Task<IActionResult> GetUserRolesDetailAsync(string idUser, string orderBy = null, bool? isDesc = null)
        {
            _adminManager.HttpContext = HttpContext;
            var userRoles = await _adminManager.GetRolesByUserId(idUser);
            if (!userRoles.IsSucces)
            {
                ViewMessage.ShowLocal(this, userRoles);
            }
            var allRoles = await _adminManager.GetAllRolesAsync();
            if (!userRoles.IsSucces)
            {
                ViewMessage.Show(this, userRoles);
            }
            ViewBag.AspNetUserRoles = userRoles.Result;
            ViewBag.CurrentUser = idUser;
            ViewData["UserName"] = userRoles.Result?.Username;
            ViewBag.OrderBy = orderBy;
            ViewBag.IsDesc = isDesc;
            return PartialView("_UserRolesDetails", allRoles.Result);
        }

        private Table GetGenericRolesTable(string titolo, List<RoleViewModel> roles, params string[] columnNames)
        {
            var genericTable = new Table()
            {
                Title = titolo,
                ColumnNames = columnNames.ToList(),
                Elements = new List<List<object>>()
            };
            if (roles != null)
            {
                var count = 0;
                for (var i = 0; i < roles.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[count].Add(roles[i].RoleId);
                    genericTable.Elements[count].Add(roles[i].RoleName);
                    genericTable.Elements[count].Add(roles[i].AssociatedMenu);
                    genericTable.Elements[count].Add(roles[i].AssociatedSubMenu1);
                    genericTable.Elements[count].Add(roles[i].AssociatedSubMenu2);
                    genericTable.Elements[count].Add(roles[i].RoleDescription);

                    count++;
                }
            }
            return genericTable;
        }

        private Table GetGenericGroupRolesTable(string titolo, List<GroupsMaster> groups, params string[] columnNames)
        {
            var genericTable = new Table()
            {
                Title = titolo,
                ColumnNames = columnNames.ToList(),
                Elements = new List<List<object>>()
            };
            if (groups != null)
            {
                var count = 0;
                for (var i = 0; i < groups.Count; ++i)
                {
                    if (groups[i].Disabled)
                        continue;
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[count].Add(groups[i].Id);
                    genericTable.Elements[count].Add(groups[i].Name);
                    genericTable.Elements[count].Add(groups[i].Description);
                    count++;
                }
            }
            return genericTable;
        }

        [HttpPost]
        public async Task<IActionResult> AssociateRolesPostNewAsync(string userId, List<int> groupRoles, List<String> roles)
        {
            _adminManager.HttpContext = HttpContext;

            var response = await _adminManager.SaveUserGroupsAndRolesAsync(userId, groupRoles, roles);
            ViewMessage.Show(this, response);
            if (response.IsSucces)
            {
                var roleIds = new List<string>();
                foreach (var groupId in groupRoles)
                {
                    var groupMasterRespnse = await _adminManager.GetGroupRolesWithDetailsAsync(groupId);
                    if (groupMasterRespnse.IsSucces)
                    {
                        var details = groupMasterRespnse.Result.GroupsDetails.Select(d => d.RoleId.ToString()).ToList();
                        roleIds = roleIds.Union(details).ToList();
                    }
                }
                roleIds = roleIds.Union(roles).ToList();
                var allRoles = await _adminManager.GetAllRolesAsync();
                var roleNames = roleIds;
                if (allRoles.IsSucces)
                {
                    roleNames = roleIds.Select(r => GerRoleName(r, allRoles.Result)).ToList();
                }
                var responseUser = await _adminManager.GetUser(userId);
                if (responseUser.IsSucces)
                {
                    var userName = responseUser.Result.UserName;
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private string GerRoleName(string roleId, List<RoleViewModel> allRoles)
        {
            var roleName = allRoles.Where(ar => ar.RoleId == roleId).Select(ar => ar.RoleName).SingleOrDefault();
            if (roleName == null)
            {
            }
            return roleName ?? roleId;
        }

        [HttpPost]
        public async Task<IActionResult> AssociateRolesPostAsync(string userId, List<String> roles)
        {
            _adminManager.HttpContext = HttpContext;
            var response = await _adminManager.AssociateUserRolesAsync(userId, roles);
            ViewMessage.Show(this, response);
            if (response.IsSucces)
            {
                var responseUser = await _adminManager.GetUser(userId);
                if (responseUser.IsSucces)
                {
                    var userName = responseUser.Result.UserName;
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DisableUserAsync(string userId, bool userDisabled, string searchUsername, int currentPage)
        {
            _adminManager.HttpContext = HttpContext;
            var response = await _adminManager.DisableUser(userId, userDisabled);
            ViewMessage.Show(this, response);
            if (response.IsSucces)
            {
                var responseUser = await _adminManager.GetUser(userId);
                if (responseUser.IsSucces)
                {
                    var userName = responseUser.Result.UserName;
                }
                return RedirectToAction("Index", new { username = searchUsername, page = currentPage });
            }
            return RedirectToAction("Index");
        }
        public IActionResult ChangeUserPassword(string userId, string username)
        {
            ViewData["Title"] = "Reset password di " + username;
            ViewBag.UserId = userId;
            ViewBag.Username = username;
            var inputModel = new RegisterUserViewModel();
            return View(inputModel);

        }
        [HttpPost]
        public async Task<IActionResult> ChangeUserPasswordPostAsync(string userId, RegisterUserViewModel input)
        {
            if (ModelState.IsValid)
            {
                var response = await _adminManager.ResetPassword(userId, input.Password, input.IsSendedEmail);
                ViewMessage.Show(this, response);
                if (response.IsSucces)
                {
                    var responseUser = await _adminManager.GetUser(userId);
                    if (responseUser.IsSucces)
                    {
                        var userName = responseUser.Result.UserName;
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string userId)
        {
            var response = await _adminManager.GetUser(userId);
            if (!response.IsSucces)
            {
                ViewMessage.Show(this, response);
                return RedirectToAction("Index");
            }
            return View(response.Result);
        }

        [HttpPost]
        public async Task<IActionResult> UserDetailsAsync(ItalTechUser user)
        {
            _adminManager.HttpContext = HttpContext;
            var response = await _adminManager.GetUser(user.Id);
            if (!response.IsSucces)
            {
                ViewMessage.Show(this, response);
                return RedirectToAction("Index");
            }
            var oldUser = response.Result;

            var oldUserForLog = oldUser.CloneSomeProperties("UserName", "Email", "LastLogin", "NextPasswordUpdate", "AccountExpiration");

            var updateResult = await _adminManager.UpdateUserDetailsAsync(user);
            if (!updateResult.IsSucces)
            {
                ViewMessage.Show(this, updateResult);
                return RedirectToAction("Index");
            }
            var newUser = updateResult.Result;

            var responseUpdateDetails = new ItalTech.Models.Response();
            responseUpdateDetails.IsSucces = updateResult.IsSucces;
            if (!updateResult.IsSucces)
            {
                responseUpdateDetails.Message = "Si è verificato un errore durante il salvataggio dei dati dell'utente";
            }
            else
            {
                responseUpdateDetails.Message = "Dati modificati con successo";

                var newUserForLog = newUser.CloneSomeProperties("UserName", "Email", "LastLogin", "NextPasswordUpdate", "AccountExpiration");
                var dati = new Dictionary<string, dynamic> { { "NEW", newUserForLog }, { "OLD", oldUserForLog } };
            }
            ViewMessage.Show(this, responseUpdateDetails);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AllGroupRoles(string orderBy = null, bool? isDesc = null)
        {
            var response = await _adminManager.GetAllGroupRolesAsync();
            if (!response.IsSucces)
            {
                ViewMessage.Show(this, response);
                return RedirectToAction("Index");
            }
            ViewBag.OrderBy = orderBy;
            ViewBag.IsDesc = isDesc;
            return View(response.Result);
        }


        [HttpGet]
        public async Task<IActionResult> EditGroupRoles(int? groupId = null, int? numUsers = null)
        {
            GroupsMaster result = null;
            if (groupId.HasValue)
            {
                var response = await _adminManager.GetGroupRolesWithDetailsAsync(groupId.Value);
                if (!response.IsSucces)
                {
                    ViewMessage.Show(this, response);
                    return RedirectToAction("AllGroupRoles");
                }
                result = response.Result;
                ControllerContext.HttpContext.Session.SetObj("GroupRoles", result);
            }
            ViewBag.NumUsersAssociates = numUsers;
            return View(result);
        }

        public async Task<IActionResult> DeleteGroupRoles(int groupId)
        {
            log.LogInformation("DeleteGroupRoles START");
            _adminManager.HttpContext = HttpContext;
            var responseGroup = await _adminManager.GetGroupRolesWithDetailsAsync(groupId);
            if (!responseGroup.IsSucces)
            {
                ViewMessage.Show(this, responseGroup);
                return RedirectToAction("AllGroupRoles");
            }
            var oldGroup = responseGroup.Result;
            var response = await _adminManager.DeleteGroupRolesAsync(groupId);
            if (!response.IsSucces)
            {
                ViewMessage.Show(this, response);
            }
            var oldGroupForLog = oldGroup.CloneSomeProperties("Id", "Name", "Description", "Disabled");
            var dati = new Dictionary<string, dynamic> { { "OLD", oldGroupForLog } };
            var descrizione = $"Eliminato gruppo ruoli {oldGroup.Name}";
            response.Message = "Eliminazione gruppo effettuata con successo";
            ViewMessage.Show(this, response);
            return RedirectToAction("AllGroupRoles");
        }

        [HttpPost]
        public async Task<IActionResult> EditGroupRolesAsync(GroupsMaster gruppo)
        {
            _adminManager.HttpContext = HttpContext;
            var newGroup = gruppo.CloneSomeProperties("Id", "Name", "Description", "Disabled");
            dynamic oldGroupForLog = null;
            if (gruppo.Id > 0)
            {
                var oldGroup = ControllerContext.HttpContext.Session.GetObj<GroupsMaster>("GroupRoles");
                oldGroupForLog = oldGroup.CloneSomeProperties("Id", "Name", "Description", "Disabled");
                ControllerContext.HttpContext.Session.Remove("GroupRoles");
            }
            var response = await _adminManager.SaveGroupRolesMasterAsync(gruppo);

            if (response.IsSucces)
            {
                var descrizione = gruppo.Id == 0 ? "Creato il gruppo " : "Modificato il gruppo ";
                descrizione += gruppo.Name;
                var dati = gruppo.Id > 0 ? new Dictionary<string, dynamic> { { "NEW", newGroup }, { "OLD", oldGroupForLog } } :
                    new Dictionary<string, dynamic> { { "NEW", newGroup } };
                response.Message = gruppo.Id > 0 ? "Modifica al gruppo effettuata con successo" : " Creazione gruppo effettuata con successo";
                ViewMessage.Show(this, response);
                return RedirectToAction("AllGroupRoles");
            }
            ViewMessage.Show(this, response);
            return View(gruppo);
        }

        public async Task<IActionResult> EditGroupRolesDetails(int groupId, string orderBy = null, bool? isDesc = null)
        {
            var responseGroup = await _adminManager.GetGroupRolesWithDetailsAsync(groupId);
            if (!responseGroup.IsSucces)
            {
                ViewMessage.Show(this, responseGroup);
                return RedirectToAction("AllGroupRoles");
            }
            var allRolesResponse = await _adminManager.GetAllRolesAsync();
            if (!allRolesResponse.IsSucces)
            {
                ViewMessage.Show(this, allRolesResponse);
                return RedirectToAction("AllGroupRoles");
            }
            ViewBag.GruppoRuoli = responseGroup.Result;
            ViewBag.OrderBy = orderBy;
            ViewBag.IsDesc = isDesc;
            return View(allRolesResponse.Result);
        }

        [HttpPost]
        public async Task<IActionResult> EditGroupRolesDetailsAsync(int groupId, List<int> roles)
        {
            _adminManager.HttpContext = HttpContext;

            var responseGroup = await _adminManager.GetGroupRolesWithDetailsAsync(groupId);
            if (!responseGroup.IsSucces)
            {
                ViewMessage.Show(this, responseGroup);
                return RedirectToAction("AllGroupRoles");
            }
            var response = await _adminManager.SaveGroupRolesDetailsOnlyRolesAsync(groupId, roles);
            if (!response.IsSucces)
            {
                return RedirectToAction("EditGroupRolesDetails", new { groupId });
            }
            var allRolesResponse = await _adminManager.GetAllRolesAsync();
            List<string> selectedRoles = null;
            List<string> oldSelectedRoles = null;
            var oldRoles = responseGroup.Result.GroupsDetails.Select(d => d.RoleId.ToString()).ToList();
            var rolesStr = roles.Select(r => r.ToString()).ToList();
            if (allRolesResponse.IsSucces)
            {
                selectedRoles = allRolesResponse.Result.Where(r => rolesStr.Contains(r.RoleId)).Select(r => r.RoleName).ToList();
                oldSelectedRoles = allRolesResponse.Result.Where(r => oldRoles.Contains(r.RoleId)).Select(r => r.RoleName).ToList();
            }
            else
            {
                selectedRoles = roles.Select(r => r.ToString()).ToList();
                oldSelectedRoles = oldRoles;
            }
            var dati = new Dictionary<string, dynamic> { { "NEW", selectedRoles }, { "OLD", oldSelectedRoles } };
            ViewMessage.Show(this, response);
            return RedirectToAction("AllGroupRoles");
        }
    }
}

