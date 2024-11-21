using System;
using API.Enums;

namespace API.DTOs
{
    public class VoteDto
    {
        public System.Guid ImageOneId { get; set; }
        public System.Guid ImageTwoId { get; set; }

        public System.Guid QuestionId { get; set; }
        
        public Answer UserAnswer { get; set; }
    }
}
