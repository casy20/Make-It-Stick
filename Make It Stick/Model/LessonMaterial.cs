namespace Make_It_Stick.Model
{
    public class LessonMaterial
    {
        public int Id { get; set; }
        public int TrainingProgramId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
