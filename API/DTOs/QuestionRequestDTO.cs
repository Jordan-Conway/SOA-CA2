namespace API.DTOs
{
    //This DTO represents a complete request from the client
    public class QuestionRequestDTO
    {
        public required ImageDto ImageOne { get; set; }
        public required ImageDto ImageTwo { get; set; }
        public required QuestionDTO Question { get; set; }
    }
}
