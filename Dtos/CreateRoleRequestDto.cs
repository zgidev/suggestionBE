using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CreateRoleRequestDto
    {
 
        [Required]
        public string RoletName { get; set; }

    }
}