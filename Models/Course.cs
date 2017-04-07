using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _1617_2_LI41N_G9.Models
{
    public class Course
    {
        [KeyAttribute]
        public int Id { get; set; }

        [RequiredAttribute]
        public string Name { get; set; }

        [RequiredAttribute]
        public string Acronym { get; set; }

        public Teacher Coordinator { get; set; }

        public List<Class> Classes { get; set; }
    }
}