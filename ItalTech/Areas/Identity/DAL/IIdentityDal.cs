using ItalTech.Areas.Admin.Models;
using ItalTech.Areas.Admin.Models.RolesGroups;
using ItalTech.Models;
using ItalTech.Models.Paginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ItalTech.Areas.Identity.Data.DAL
{
    public interface IIdentityDal
    {
        /// <summary>
        /// Crea o aggiorna un gruppo. E' possibile anche cambiare il nome del gruppo a patto che non sia già usato da un altro gruppo. Restituisce l'id del gruppo, utile in fase di creazione
        /// </summary>
        /// <param name="gruppo"></param>
        /// <returns></returns>
        Task<Response<int>> SaveGroupRolesMasterAsync(GroupsMaster gruppo);

        /// <summary>
        /// Crea o aggiorna la lista dei ruoli di un gruppo
        /// </summary>
        /// <param name="groupMasterId">Id della tabella master</param>
        /// <param name="details"></param>
        /// <returns></returns>
        Task<Response> SaveGroupRolesDetailsAsync(int groupMasterId, List<GroupsDetail> details);

        /// <summary>
        /// Restituisce una lista con tutti i gruppi esistenti
        /// </summary>
        /// <returns></returns>
        Task<Response<List<GroupsMaster>>> GetAllGroupRolesAsync();

        /// <summary>
        /// Restituisce il gruppo con il dato id ed il dettaglio dei ruoli associati al gruppo
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<Response<GroupsMaster>> GetGroupRolesWithDetailsAsync(int groupId);

        /// <summary>
        /// Restituisce i dettagli dei gruppi passati in input
        /// </summary>
        /// <param name="groupIds">Lista di id dei gruppi</param>
        /// <returns></returns>
        Task<Response<List<GroupsDetail>>> GetGroupDetailsAsync(List<int> groupIds);

        /// <summary>
        /// Elimina un gruppo di ruoli
        /// </summary>
        /// <param name="groupMasterId"></param>
        /// <returns></returns>
        Task<Response> DeleteGroupRolesAsync(int groupMasterId);


        /// <summary>
        /// Restituisce la lista dei gruppi di ruoli associati all'utente
        /// </summary>
        /// <param name="userId">Id dell'utente</param>
        /// <returns></returns>
        Task<Response<List<UserGroups>>> GetUserGroupsAsync(string userId);

        /// <summary>
        /// Elimina l'associazione tra un utente ed un gruppo
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<Response> DeleteUserGroupAsync(string userId, int groupId);

        /// <summary>
        /// Associa all'utente il gruppo di ruoli passato in input
        /// </summary>
        /// <param name="userId">Id dell'utente</param>
        /// <param name="userGroups">Gruppo di ruoli da associare all'utente</param>
        /// <returns></returns>
        Task<Response> SaveUserGroupsAsync(string userId, List<UserGroups> userGroups);

        /// <summary>
        /// Restituisce la lista di ruoli associati all'utente. Legge dalla tabella UserRoles
        /// </summary>
        /// <param name="userId">Id dell'utente</param>
        /// <returns></returns>
        Task<Response<List<UserRoles>>> GetUserRolesAsync(string userId);

        /// <summary>
        /// Restituisce una lista con gli utenti associati al gruppo passato in input
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<Response<List<string>>> GetUsersAssociatedToGroupAsync(int groupId);

        /// <summary>
        /// Associa all'utente i ruoli passato in input
        /// </summary>
        /// <param name="userId">Id dell'utente</param>
        /// <param name="userRoles">Lista di ruoli da associare all'utente</param>
        /// <returns></returns>
        Task<Response> SaveUserRolesAsync(string userId, List<UserRoles> userRoles);
        Task<ResultPaginated<UserViewModel>> GetAllUserRolesAsysnc(Pagination pagination);
        Task<UserViewModel> GetRolesByUserId(string userId);
    }
}
