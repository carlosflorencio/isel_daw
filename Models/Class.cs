using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _1617_2_LI41N_G9.Models
{
    public class Class
    {
        [RequiredAttribute]
        public string Semester { get; set; }

        [RequiredAttribute]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public List<Teacher> Teachers { get; set; }

        public List<Student> Students { get; set; }

        public List<Group> Groups { get; set; }
    }
}