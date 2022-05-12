using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Dtos.Response;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    public class ComplaintsController : BaseApiController
    {
        public IConfiguration _configuration { get; }

        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IComplaintRepository _complaintrepository;
        private   SendEmail _sendmail;
        private readonly IMapper _mapper;

        public ComplaintsController(DataContext context, IMapper mapper,
         IComplaintRepository complaintrepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _complaintrepository = complaintrepository;
            _userRepository = userRepository;

            _sendmail = new SendEmail(_configuration);
        }

        // api/Complaint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetComplaintsResponseDto>>> GetComplaints()
        {
            // GetComplaintsResponseDto  
            // var compl = await _context.Complaints.Include(p => p.ComplaintsComment).ToListAsync(); 
            // List<GetComplaintsResponseDto>? complaintsResponse = new List<GetComplaintsResponseDto>(); 
            // complaintsResponse = new GetComplaintsResponseDto(
            //         firstName: "",
            //         lastName: "",
            //         email: "",n             
            //         policyNumber: "",
            //         mobilePhone: "",
            //         dateCreated: "",
            //         comments: GetComplaintComments(1)
            //           );   
            var compl = await _complaintrepository.GetComplaintsAsync();
            var complToreturn = _mapper.Map<List<GetComplaintsResponseDto>>(compl);

            // for (int i = 0; i < complToreturn[i].ComplaintsComment.Count; i++)
            // {
            //     complToreturn.
            // }


            return Ok(complToreturn);
        }

        // api/Complaint/3
        [HttpGet("{id}")]
        public async Task<ActionResult<GetComplaintsResponseDto>> GetComplaint(int id)
        {
            Complaint? complaint = await _complaintrepository.GetComplaintsByIsAsync(id);
            var mappedComplaint = _mapper.Map<GetComplaintsResponseDto>(complaint);

            return Ok(mappedComplaint);
        }


        // api/Complaint/dept
        [HttpPost("departmentcomplaints")]
        public async Task<ActionResult<IEnumerable<GetComplaintsResponseDto>>> GetDeptComplaints(GetComplaintByDepartmentRequestDto getComplaintByDepartmentRequestDto)
        {
            var compl = await _complaintrepository.GetdepartmentComplaintsAsync(getComplaintByDepartmentRequestDto.DepartmentName);
            var complToreturn = _mapper.Map<List<GetComplaintsResponseDto>>(compl);

            return complToreturn;

        }

        [HttpPost("createcomplaint")]
        public async Task<ActionResult<CreateComplaintResponseDto>> Createcomplaint(CreateComplaintRequestDto createComplaintRequestDto)
        {
            // var complaint = new Complaint(
            // firstName: createComplaintRequestDto.FirstName,
            // lastName: createComplaintRequestDto.LastName,
            // mobilePhone: createComplaintRequestDto.MobilePhone,
            // policyNumber: createComplaintRequestDto.PolicyNumber,
            // email: createComplaintRequestDto.Email,
            // dateCreated: DateTime.Now
            //     );

            var complaint = new Complaint
            {
                FirstName = createComplaintRequestDto.FirstName,
                LastName = createComplaintRequestDto.LastName,
                MobilePhone = createComplaintRequestDto.MobilePhone,
                PolicyNumber = createComplaintRequestDto.PolicyNumber,
                Email = createComplaintRequestDto.Email,
                LastDepartment = createComplaintRequestDto.Comment.AssignedDepartment,
                DateCreated = DateTime.Now,
                // ComplaintsComment = (ICollection<ComplaintsComment>)createComplaintRequestDto.Comment

            };


            // commit to save
            _context.Complaints.Add(complaint);
            // save into db
            var created = await _context.SaveChangesAsync();


            var createComplaintCommentRequestDto = new CreateComplaintCommentRequestDto
            {
                CommentDetails = createComplaintRequestDto.Comment,
                ComplaintId = complaint.Id
            };

            if (created > 0)
            {
                await Createcomment(createComplaintCommentRequestDto);
            }

            return new CreateComplaintResponseDto(
                message: "Complaint created",
                status: true
             );
        }


        private async Task<ActionResult<Complaint>> UpdateComplaint(int complaintId, string lastdeptmt)
        {

            Complaint? complaint = await _complaintrepository.GetComplaintsByIsAsync(complaintId);

            complaint.LastDepartment = lastdeptmt;
            // commit to save
            _context.Complaints.Update(complaint);
            // save into db
            var created = await _context.SaveChangesAsync();

            return complaint;

        }

        [HttpPost("createcomment")]
        public async Task<ActionResult<ComplaintsComment>> Createcomment(CreateComplaintCommentRequestDto createComplaintCommentRequestDto)

        {
            var comment = new ComplaintsComment
            {
                ComplaintId = createComplaintCommentRequestDto.ComplaintId,
                Comment = createComplaintCommentRequestDto.CommentDetails.Comment,
                ComplaintStatus = createComplaintCommentRequestDto.CommentDetails.CompalintStatus,
                AssignedDepartment = createComplaintCommentRequestDto.CommentDetails.AssignedDepartment,
                FileLink = createComplaintCommentRequestDto.CommentDetails.FileLink,
                CreatedBy = createComplaintCommentRequestDto.CommentDetails.CreatedBy
            };

            //
            await UpdateComplaint(createComplaintCommentRequestDto.ComplaintId, createComplaintCommentRequestDto.CommentDetails.AssignedDepartment);

            // commit to save
            _context.ComplaintsComments.Add(comment);
            // save into db
            await _context.SaveChangesAsync();

            // send email

            Department dept = await _context.Departments.SingleOrDefaultAsync(data => data.DepartmentName == createComplaintCommentRequestDto.CommentDetails.AssignedDepartment);

            bool mailSent = _sendmail.SendComplaintMail(dept.GroupEmail, dept.DepartmentName);

            return comment;

        }

       /* private void SendEmail()
        {

            _sendmail.SendComplaintMail();
        }*/


        // POST api/<InitiateController>/upload
        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var tempFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileName = DateTime.Now.ToString().Replace("/", "-").Replace(":", "-").Replace(" ", "_") + "_" + tempFileName.Replace(" ", "-");

                    var fullPath = Path.Combine(pathToSave, fileName);
                    var filePath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { filePath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, new
                {
                    error = e
                });
            }
        }



    }
}

