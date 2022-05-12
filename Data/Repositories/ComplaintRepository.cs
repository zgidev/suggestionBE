using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using Microsoft.Data.SqlClient;

namespace API.Data.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly DataContext _context;

        public ComplaintRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Complaint>> GetComplaintsAsync()
        {

/*            int limit = 10;

            if (page == 0)
                page = 1;

            if (limit == 0)
                limit = int.MaxValue;

            var skip = (page - 1) * limit;

            return await _context.Complaints.Include(p => p.ComplaintsComment).Skip(skip).Take(limit).ToListAsync();*/

            return await _context.Complaints.Include(p => p.ComplaintsComment).ToListAsync();
        }

        public async Task<Complaint> GetComplaintsByIsAsync(int id)
        {
            return await _context.Complaints.Include(p => p.ComplaintsComment).SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Complaint>> GetdepartmentComplaintsAsync(string deptm)
        {

            return await _context.Complaints.Include(p => p.ComplaintsComment).Where(d => d.LastDepartment == deptm).ToListAsync();


            //using System.Linq;
            // IEnumerable<Complaint> complaint = (from complaintComment in _context.ComplaintsComments
            //                        where complaintComment.ComplaintId == id
            //                        select complaintComment.Complaint).Include(p => p.ComplaintsComment).ToList();



            //   var complaintr = _context.Complaints
            //         .FromSqlRaw("SELECT [c].[Id], [c].[DateCreated], [c].[Email], [c].[FirstName], [c].[LastName], [c].[MobilePhone], [c].[PolicyNumber], [c0].[Id], [c0].[AssignedDepartmentId], [c0].[Comment], [c0].[ComplaintId], [c0].[ComplaintStatusId], [c0].[CreatedBy], [c0].[DateCreated], [c0].[FileLink] FROM [Complaints] AS [c] LEFT JOIN [ComplaintsComments] AS [c0] ON [c].[Id] = [c0].[ComplaintId] WHERE [c0].[AssignedDepartmentId] = 1")
            //         .ToList();

            // return (IEnumerable<Complaint>)complaintr;




        }

        public Task<IEnumerable<Complaint>> GetUserComplaintsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAync()
        {
            throw new NotImplementedException();
        }
    }
}