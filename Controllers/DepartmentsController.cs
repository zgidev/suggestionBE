using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class DepartmentsController : BaseApiController
    {
        private readonly DataContext _context;

        public DepartmentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepts()
        {

            return await _context.Departments.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDept(int deptid)
        {


            Department? dept = await _context.Departments.FindAsync(deptid);
            return dept;
        }



        [HttpPost("createdepartment")]
        public async Task<ActionResult<Department>> Createdepartment(CreateDepartmentRequestDto createDepartmentDto)
        {

            if (await DeptmExist(createDepartmentDto.DepartmentName)) return BadRequest("Department is already created");
            var department = new Department(
                departmentName: createDepartmentDto.DepartmentName,
                dateCreated: DateTime.Now,
                groupEmail: createDepartmentDto.GroupEmail,
                status: true
            );
            // commit to save
            _context.Departments.Add(department);
            // save into db
            await _context.SaveChangesAsync();
            return department;
        }

        private async Task<bool> DeptmExist(string departmentName)
        {
            return await _context.Departments.AnyAsync(data => data.DepartmentName == departmentName);
        }
    }
}