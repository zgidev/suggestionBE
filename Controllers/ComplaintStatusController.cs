
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
    public class ComplaintStatusController : BaseApiController
    {

        private readonly DataContext _context;

        public ComplaintStatusController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintStatus>>> GetStatuses()
        {
            return await _context.ComplaintStatuses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintStatus>> GetStatus(int statid)
        {
            ComplaintStatus? status = await _context.ComplaintStatuses.FindAsync(statid);
            return status;
        }


        [HttpPost("createstatus")]
        public async Task<ActionResult<ComplaintStatus>> CreateStatus(CreateStatusRequestDto createStatusRequestDto)
        {
            if (await StatusExist(createStatusRequestDto.StatusName)) return BadRequest("Status is already taken");

            var stat = new ComplaintStatus(
                statusName: createStatusRequestDto.StatusName.ToLower(),
                 dateCreated: DateTime.Now,
                status: true
            );
            _context.ComplaintStatuses.Add(stat);
            await _context.SaveChangesAsync();
            return stat;
        }

        private async Task<bool> StatusExist(string statusame)
        {
            return await _context.ComplaintStatuses.AnyAsync(data => data.StatusName == statusame.ToLower());
        }



    }
}


// CreateRoleRequestDto