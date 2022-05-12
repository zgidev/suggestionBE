using System;

namespace API.Entities
{
    public class ComplaintStatus
    {


        public ComplaintStatus( string statusName, bool status, DateTime dateCreated)
        {
        
            this.StatusName = statusName;
            this.status = status;
            this.DateCreated = dateCreated;

        }
        public int Id { get; set; }
        public string StatusName { get; set; }
        public bool status { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}