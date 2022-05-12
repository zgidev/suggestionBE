
using API.Dtos.Response;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, LoginResponseDto>();
            CreateMap<Complaint, GetComplaintsResponseDto>();
            CreateMap<ComplaintsComment, GetComplaintCommentResponseDto>();
        }
    }
}