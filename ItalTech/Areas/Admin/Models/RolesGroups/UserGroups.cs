namespace ItalTech.Areas.Admin.Models.RolesGroups
{
    public class UserGroups
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdGroup { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
    }
}
