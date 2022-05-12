using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{

    [Table("ComplaintsComments")]
    public class ComplaintsComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string ComplaintStatus { get; set; }
        public string AssignedDepartment { get; set; }
        public string FileLink { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [JsonIgnore]
        public Complaint Complaint { get; set; }
        public int ComplaintId { get; set; }
    }
}