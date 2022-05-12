using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class ComplaintCommentRequestDto
    {
        public string Comment { get; set; }
        [Required]
        public string CompalintStatus{ get; set; }
        [Required]
        public string AssignedDepartment { get; set; }
        [Required]
        public string FileLink { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }

    public class CreateComplaintCommentRequestDto
    {
        [Required]
        public int ComplaintId { get; set; }
        [Required]
        public ComplaintCommentRequestDto CommentDetails { get; set; }
    }
}