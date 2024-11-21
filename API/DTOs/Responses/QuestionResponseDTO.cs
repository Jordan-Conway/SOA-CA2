namespace API.DTOs.Responses
{
    //This DTO represents a complete request from the client
    public class QuestionResponseDTO
    {
        public required ImageDto ImageOne { get; set; }
        public required ImageDto ImageTwo { get; set; }
        public required QuestionDTO Question { get; set; }
    }
}
