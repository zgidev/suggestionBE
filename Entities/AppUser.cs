using System;
namespace API.Entities
{
    public class AppUser
    {
        public AppUser(string username, string email, int departmentId, int roleId, bool status, DateTime dateCreated)
        {
            this.Username = username;
            this.Email = email;
            this.DepartmentId = departmentId;
            this.RoleId = roleId;
            this.Status = status;
            this.DateCreated = dateCreated;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}