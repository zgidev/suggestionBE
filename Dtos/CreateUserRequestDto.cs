using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CreateUserRequestDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string DepartmentId { get; set; }

        [Required]
        public string RoleId { get; set; }
    }
}