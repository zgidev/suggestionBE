namespace API.Dtos.Response
{
    public class CreateComplaintResponseDto
    {
        public CreateComplaintResponseDto(string message, bool status)
        {
            this.message = message;
            this.status = status;
        }
        public string message { get; set; }
        public bool status { get; set; }

    }
}