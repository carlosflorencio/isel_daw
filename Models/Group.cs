using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _1617_2_LI41N_G9.Models
{
    public class Group
    {
        [RequiredAttribute]
        public int GroupNumber { get; set; }

        [RequiredAttribute]
        public int ClassId { get; set; }

        [RequiredAttribute]
        public string Semester { get; set; }

        public Class Class { get; set; }

        public List<Student> Students { get; set; }
    }
}