using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Dtos.Response;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers


{
    public class UsersController : BaseApiController
    {

        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public IMapper _mapper { get; }

        public UsersController(DataContext context, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userRepository = userRepository;
        }

        // api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginResponseDto>>> GetUsers()
        {

            var users = await _userRepository.GetUsersAsync();

            List<LoginResponseDto> tempUsers = new List<LoginResponseDto>();

            foreach (var item in users)
            {
                LoginResponseDto userItem = new LoginResponseDto() 
                { 
                    Username = item.Username,
                    Email = item.Email,
                    Status = item.Status,
                    DateCreated = item.DateCreated
                };

               Department dept = await _context.Departments.FindAsync(item.DepartmentId);
                var depName = dept.DepartmentName;
                userItem.DepartmentName = depName;

                Role arole = await _context.Roles.FindAsync(item.RoleId);
                var roleName = arole.RoleName;

                userItem.Role = roleName;

                tempUsers.Add(userItem);

            }

            var userToReturn = _mapper.Map<IEnumerable<LoginResponseDto>>(tempUsers);

            return Ok(userToReturn);


            //return await _userRepository.GetUsersAsync();
            // return await _context.Users.ToListAsync();
            // userRepository
        }

        // api/users/3
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {


            return await _userRepository.GetUserByUserIdAsync(id);


            // AppUser? user = await _context.Users.FindAsync(id);
            // return user!;
        }

        [HttpPost("createuser")]
        public async Task<ActionResult<AppUser>> CreateUser(CreateUserRequestDto createUserRequestDto)
        {
            if (await _userRepository.UserExist(createUserRequestDto.UserName)) return BadRequest("Username is already taken");
            var user = new AppUser(
                username: createUserRequestDto.UserName.Trim().ToLower(),
                email: createUserRequestDto.Email.Trim().ToLower(),
                departmentId: int.Parse(createUserRequestDto.DepartmentId),
                roleId: int.Parse(createUserRequestDto.RoleId),
                dateCreated: DateTime.Now,
                status: true
                );

            // commit to save
            _context.Users.Add(user);
            // save into db
            await _context.SaveChangesAsync();
            return user;

        }

    }
}