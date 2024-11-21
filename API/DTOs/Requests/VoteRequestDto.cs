using System;
using API.Enums;

namespace API.DTOs.Requests
{
    public class VoteRequestDto
    {
        public Guid ImageOneId { get; set; }
        public Guid ImageTwoId { get; set; }

        public Guid QuestionId { get; set; }

        public Answer UserAnswer { get; set; }
    }
}
