using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{

    public class Student {

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

        public ICollection<ClassStudent> Classes { get; set; }

        public ICollection<GroupStudent> Groups { get; set; }
    }
}