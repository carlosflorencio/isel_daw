using API.Models;
using API.Models.CreationDTO;

namespace API.Mapper
{
    public class CreationToModelMapper
    {
        public static Teacher Map(TeacherCreationDTO dto){
            return new Teacher { Number = dto.Number, Name = dto.Name, Email = dto.Email };
        }

        public static Student Map(StudentCreationDTO dto){
            return new Student { Number = dto.Number, Name = dto.Name, Email = dto.Email };
        }

//        public static Course Map(CourseCreationDTO dto){
//            return new Course { Name = dto.Name, Acronym = dto.Acronym, CoordinatorId = dto.CoordinatorId };
//        }

        public static Class Map(ClassCreationDTO dto){
            return new Class(); //TODO: mapping of class
        }
    }
}