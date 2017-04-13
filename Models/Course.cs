using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAW_API.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Acronym { get; set; }

        public int? CoordinatorId { get; set; }

        [ForeignKey("CoordinatorId")]
        public virtual Teacher Coordinator { get; set; }

        public virtual List<Class> Classes { get; set; }
    }
}