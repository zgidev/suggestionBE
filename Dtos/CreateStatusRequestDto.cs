using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CreateStatusRequestDto
    {
 
        [Required]
        public string StatusName { get; set; }

    }
}