namespace API.DTOs.Requests
{
    public class DeleteQuestionRequestDTO
    {
        public required System.Guid UserID { get; set; }
        public required System.Guid QuestionId { get; set; }
    }
}
