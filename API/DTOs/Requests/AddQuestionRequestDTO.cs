namespace API.DTOs.Requests
{
    public class AddQuestionRequestDTO
    {
        public required System.Guid UserID { get; set; }
        public string? Text { get; set; }
    }
}
