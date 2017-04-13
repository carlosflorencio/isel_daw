using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAW_API.Models
{
    public class Class
    {
        [Required]
        public string Semester { get; set; }

        [Required]
        public int CourseId { get; set; }

        public string Name{ get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public List<Teacher> Teachers { get; set; }

        public List<Student> Students { get; set; }

        public List<Group> Groups { get; set; }
    }
}