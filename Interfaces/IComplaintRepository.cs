using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IComplaintRepository
    {

        Task<bool> SaveAllAync ();
        Task <IEnumerable<Complaint>> GetComplaintsAsync ();
        Task <IEnumerable<Complaint>> GetUserComplaintsAsync (int id);
        Task <IEnumerable<Complaint>> GetdepartmentComplaintsAsync (string deptm);
        Task  <Complaint> GetComplaintsByIsAsync (int id);  
    }
}