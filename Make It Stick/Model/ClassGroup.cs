namespace Make_It_Stick.Model
{
    public class ClassGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ClassCode { get; set; } = string.Empty;
        public int TrainingProgramId { get; set; }
        public int TeacherId { get; set; }
    }
}
