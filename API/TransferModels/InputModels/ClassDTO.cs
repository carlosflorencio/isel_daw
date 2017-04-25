namespace API.TransferModels.InputModels
{
    public class ClassDTO
    {
        public string Name { get; set; }
        public int MaxGroupSize { get; set; }
        public bool AutoEnrollment { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
    }
}