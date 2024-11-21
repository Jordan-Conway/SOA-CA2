using System;
using API.Enums;

namespace API.DTOs.Requests
{
    public class VoteRequestDto
    {
        public int ImageOneId { get; set; }
        public int ImageTwoId { get; set; }

        public int QuestionId { get; set; }

        public Answer UserAnswer { get; set; }
    }
}
