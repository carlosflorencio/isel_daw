using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Group
    {
        public int Id { get; set; }
        
        public int Number { get; set; }

        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        public List<GroupStudent> Students { get; set; }
    }

    /*
    |--------------------------------------------------------------------------
    | Join Tables - Many to Many (EF Core needs this at the moment)
    |--------------------------------------------------------------------------
    */
    public class GroupStudent
    {
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public int StudentNumber { get; set; }

        [ForeignKey("StudentNumber")]
        public Student Student { get; set; }
    }
}