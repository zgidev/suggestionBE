using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CreateComplaintRequestDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string PolicyNumber { get; set; }

        [Phone]
        [Required]
        public string MobilePhone { get; set; }

        [Required]
        public ComplaintCommentRequestDto Comment { get; set; }

    }
}