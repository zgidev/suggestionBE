using System;

namespace API.Dtos.Response
{
    public class LoginResponseDto
    {
       //  public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }  }
}