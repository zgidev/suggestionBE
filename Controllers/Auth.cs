using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using API.Dtos.Response;
using API.Interfaces;

namespace API.Controllers
{
   
    public class Auth : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;
        private static readonly HttpClient client = new HttpClient();
        private static readonly string _PinAndtokenServer = "http://172.31.60.81/AuthGateway/api/v1/Auth/Login?api-version=1.0";
        public Auth(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        // api/auth/login 
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        { 

            var httpContent = new StringContent(JsonSerializer.Serialize(loginRequestDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_PinAndtokenServer, httpContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            { 
                var user = await _context.Users.FirstOrDefaultAsync(data => data.Username.ToLower() == loginRequestDto.Username.ToLower());
                if (user == null)
                {
                    return StatusCode(400, new
                    {
                        id = 0,
                        username = "",
                        email = "",
                        departmentName = "",
                        role = "",
                        status = false,
                        dateCreated = ""
                    });
                }

                else
                {
                    var dept = await _context.Departments.FindAsync(user.DepartmentId);
                    var role = await _context.Roles.FindAsync(user.RoleId);
                    //
                    LoginResponseDto validuser = new LoginResponseDto{
                        DateCreated = user.DateCreated,
                        DepartmentName = dept.DepartmentName,
                        Email = user.Email,
                        Role =  role.RoleName,
                        Status = user.Status,
                        Username = user.Username,
                    }; 

                    return validuser;
                   // return user;
                } 

            } else {
                return StatusCode(400, new
                        {
                            id = 0,
                            username = "",
                            email = "",
                    departmentName = "",
                    role = "",
                    status = false,
                            dateCreated = ""
                        });
            }

        }


    }
}