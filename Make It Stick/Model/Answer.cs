using System;

namespace Make_It_Stick.Model
{
    public class Answer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerOptionId { get; set; }
        public int TestAttemptId { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
