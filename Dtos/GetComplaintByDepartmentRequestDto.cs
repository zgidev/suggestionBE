using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class GetComplaintByDepartmentRequestDto
    {

        [Required]
        public string DepartmentName { get; set; }
    }
}



 