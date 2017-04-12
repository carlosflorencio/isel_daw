using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1617_2_LI41N_G9.Models
{
    public class Course
    {
        [KeyAttribute]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [RequiredAttribute]
        public string Name { get; set; }

        [RequiredAttribute]
        public string Acronym { get; set; }

        public int? CoordinatorId { get; set; }

        [ForeignKey("CoordinatorId")]
        public virtual Teacher Coordinator { get; set; }

        public virtual List<Class> Classes { get; set; }
    }
}