namespace API.Models.CreationDTO
{
    public class CourseCreationDTO
    {
        public string Name { get; set; }

        public string Acronym { get; set; }

        public int? CoordinatorId { get; set; }
    }
}