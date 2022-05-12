using System.Collections.Generic;
using API.Entities;

namespace API.Dtos.Response
{
    public class GetComplaintsResponseDto
    {
 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PolicyNumber { get; set; }
        public string MobilePhone { get; set; }
        public string DateCreated { get; set; } 
        public ICollection<GetComplaintCommentResponseDto> ComplaintsComment { get; set; }

    }
}