using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Group
    {
        // Composite key, defined in DatabaseContext
        public int Id { get; set; }
        public int ClassId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public Class Class { get; set; }

        public ICollection<GroupStudent> Students { get; set; }
    }

    /*
    |--------------------------------------------------------------------------
    | Join Tables - Many to Many (EF Core needs this at the moment)
    |--------------------------------------------------------------------------
    */
    public class GroupStudent
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}