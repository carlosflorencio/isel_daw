using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAW_API.Models
{

    public class Teacher : User {

        public int Number { get; set; }

        public ICollection<ClassTeacher> Classes { get; set; }
    }
}
