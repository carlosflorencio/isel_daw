using _1617_2_LI41N_G9.Models;
using _1617_2_LI41N_G9.Models.CreationDTO;

namespace _1617_2_LI41N_G9.Mapper
{
    public class CreationToModelMapper
    {
        public static Teacher Map(TeacherCreationDTO dto){
            return new Teacher { Number = dto.Number, Name = dto.Name, Email = dto.Email };
        }

        public static Student Map(StudentCreationDTO dto){
            return new Student { Number = dto.Number, Name = dto.Name, Email = dto.Email };
        }

        public static Course Map(CourseCreationDTO dto){
            return new Course { Name = dto.Name, Acronym = dto.Acronym, CoordinatorId = dto.CoordinatorId };
        }

        public static Class Map(ClassCreationDTO dto){
            return new Class(); //TODO: mapping of class
        }
    }
}