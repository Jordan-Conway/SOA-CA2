namespace API.DTOs.Requests
{
    public class DeleteQuestionRequestDTO
    {
        public required System.Guid UserID { get; set; }
        public required int QuestionId { get; set; }
    }
}
