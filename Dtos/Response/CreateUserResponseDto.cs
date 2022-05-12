using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Response
{
    public class CreateUserResponseDto
    {



        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Role { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

    }
}