using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Complaint
    {
 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PolicyNumber { get; set; }
        public string MobilePhone { get; set; }
        public string LastDepartment { get; set; }

        
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<ComplaintsComment> ComplaintsComment { get; set; }




    }


}