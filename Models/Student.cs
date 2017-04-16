using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAW_API.Models
{

    public class Student : User {

        public int Number { get; set; }

        public ICollection<ClassStudent> Classes { get; set; }

        public ICollection<GroupStudent> Groups { get; set; }
    }
}