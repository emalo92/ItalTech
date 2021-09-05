using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItalTech.Models;
using ItalTech.Areas.Admin.Models;
using ItalTech.Models.Paginate;
using ItalTech.Areas.Identity.Data;
using ItalTech.Areas.Admin.Models.RolesGroups;

namespace ItalTech.Manager
{
    public interface IAdminManager
    {
        int GetNumUsers();
        Task<Response<IdentityResult>> CreateAdminUserAsync(string userName, string password);
        Task<Response<IdentityResult>> CreateUserAsync(string userName, string password);
        Task<Response<ResultPaginated<UserViewModel>>> GetAllUserWithRolesAsysnc(Pagination pagination);
        Task<Response<List<RoleViewModel>>> GetAllRolesAsync();

        /// <summary>
        /// Restituisce i ruoli dell'utente cercando nella tabella AspNetUserRoles. Da non confondere con il metodo GetUserRolesAsync
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response<UserViewModel>> GetRolesByUserId(string userId);
        Task<Response<IdentityResult>> AssociateUserRolesAsync(string userId, List<string> roles);
        HttpContext HttpContext { get; set; }
        Task<Response<bool>> DisableUser(string userId, bool userDisabled);
        Task<Response<IdentityResult>> ResetPassword(string userId, string password, bool isSendedEmail);
        Task<Response<IdentityResult>> ChangeEmail(string userId, string email);
        Task<Response<ItalTechUser>> GetUser(string userId);
        Task<Response<ItalTechUser>> UpdateUserDetailsAsync(ItalTechUser user);
        Task<Response<int>> SaveGroupRolesMasterAsync(GroupsMaster gruppo);

        /// <summary>
        /// Salva il grupo di ruoli che ha l'id specificato con i ruoli elencati in details. Poi aggiorna i ruoli di tutti gli utenti che dipendono dal gruppo modificato
        /// </summary>
        /// <param name="groupMasterId"></param>
        /// <returns></returns>
        Task<Response> SaveGroupRolesDetailsAsync(int groupMasterId, List<GroupsDetail> details);

        /// <summary>
        /// Salva il grupo di ruoli che ha l'id specificato con i ruoli elencati. Poi aggiorna i ruoli di tutti gli utenti che dipendono dal gruppo modificato
        /// </summary>
        /// <param name="groupMasterId"></param>
        /// <returns></returns>
        Task<Response> SaveGroupRolesDetailsOnlyRolesAsync(int groupMasterId, List<int> roles);
        Task<Response<List<GroupsMaster>>> GetAllGroupRolesAsync();
        Task<Response<GroupsMaster>> GetGroupRolesWithDetailsAsync(int groupId);

        /// <summary>
        /// Elimina il grupo di ruoli che ha l'id specificato. Poi aggiorna i ruoli di tutti gli utenti che dipendono dal gruppo eliminato
        /// </summary>
        /// <param name="groupMasterId"></param>
        /// <returns></returns>
        Task<Response> DeleteGroupRolesAsync(int groupMasterId);

        /// <summary>
        /// Aggiorna i ruoli di tutti gli utenti che dipendono dal gruppo di ruoli passato in input
        /// </summary>
        /// <param name="groupMasterId">Id del gruppo di ruoli da considerare</param>
        /// <param name="isDeleted">If true significa che l'update riguarda un gruppo che è stato eliminato</param>
        /// <param name="messageSuccess">Messaggi opzionale da usare nella response in caso di success</param>
        /// <returns></returns>
        Task<Response> UpdateUserRolesWithGroup(int groupMasterId, bool isDeleted = false, string messageSuccess = null);


        Task<Response<List<UserGroups>>> GetUserGroupsAsync(string userId);
        
        /// <summary>
        /// Restituisce i ruoli dell'utente cercando nella tabella UserRoles. Da non confondere con il metodo GetRolesByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response<List<UserRoles>>> GetUserRolesAsync(string userId);

        /// <summary>
        /// Salva i gruppi e i ruoli associati all'utente. Poi salva i ruoli nella tabella AspNetUserRoles
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userGroups"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        Task<Response> SaveUserGroupsAndRolesAsync(string userId, List<int> userGroups, List<string> userRoles);
    }
}
