namespace API.Dtos.Response
{
    public class GetComplaintCommentResponseDto
    {

        public string Comment { get; set; }

        public string ComplaintStatus { get; set; }
        public string AssignedDepartment { get; set; }

        // public string CompalintStatus { get; set; } 
        // public string AssignedDepartment { get; set; }
        public string FileLink { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated { get; set; }
    }
}