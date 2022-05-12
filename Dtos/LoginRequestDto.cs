using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class LoginRequestDto
    {
 
        [Required]
        public string Username { get; set; }
        [Required]
        public string PinOTP { get; set; }
    }
}