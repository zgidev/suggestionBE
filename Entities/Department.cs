using System;

namespace API.Entities
{
    public class Department
    {

        public Department(  string departmentName, bool status, DateTime dateCreated, string groupEmail )
        {
          
            this.DepartmentName = departmentName;
            this.Status = status;
            this.DateCreated = dateCreated;
            this.GroupEmail = groupEmail;

        }
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string GroupEmail { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}