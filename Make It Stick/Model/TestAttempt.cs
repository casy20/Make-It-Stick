using System;

namespace Make_It_Stick.Model
{
    public class TestAttempt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionSetId { get; set; }
        public decimal Score { get; set; }
        public DateTime Date { get; set; }
    }
}
