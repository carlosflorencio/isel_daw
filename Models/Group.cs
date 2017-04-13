using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAW_API.Models
{
    public class Group
    {
        [Required]
        public int GroupNumber { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public string Semester { get; set; }

        public Class Class { get; set; }

        public List<Student> Students { get; set; }
    }
}