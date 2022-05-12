using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace API.Controllers
{
    public class RolesController : BaseApiController
    {
        private readonly DataContext _context;

        public RolesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {

            return await _context.Roles.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Getrole(int role)
        {
            Role? arole = await _context.Roles.FindAsync(role);
            return arole;
        }


        [HttpPost("createrole")]
        public async Task<ActionResult<Role>> Createrole(CreateRoleRequestDto createRoleRequestDto)
        {

            if (await RoleExist(createRoleRequestDto.RoletName)) return BadRequest("Role is already created");
            var role = new Role(
                roleName: createRoleRequestDto.RoletName.ToLower(),
                dateCreated: DateTime.Now,
                status: true
            );

            // commit to save
            _context.Roles.Add(role);
            // save into db
            await _context.SaveChangesAsync();
            return role;
        }



        private async Task<bool> RoleExist(string role)
        {
            return await _context.Roles.AnyAsync(data => data.RoleName == role.ToLower());
        }

    }
}