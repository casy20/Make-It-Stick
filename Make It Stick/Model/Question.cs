namespace Make_It_Stick.Model
{
    public class Question
    {
        public int Id { get; set; }
        public int QuestionSetId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
    }
}
