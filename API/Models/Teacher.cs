using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{

    public class Teacher {

        [Key]
        public int Number { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MinLength(6)]
        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<ClassTeacher> Classes { get; set; }
    }
}
