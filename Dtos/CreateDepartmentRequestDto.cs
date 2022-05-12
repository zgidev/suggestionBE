using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CreateDepartmentRequestDto
    { 
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        [EmailAddress]
        public string GroupEmail { get; set; } 
    }
}