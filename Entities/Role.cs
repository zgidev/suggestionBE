using System;

namespace API.Entities
{
    public class Role
    {


        public Role( string roleName, bool status, DateTime dateCreated)
        {
             
            this.RoleName = roleName;
            this.Status = status;
            this.DateCreated = dateCreated;

        }
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;


    }
}