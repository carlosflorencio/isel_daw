using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1617_2_LI41N_G9.Models
{
    public class User
    {
        [KeyAttribute]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [RequiredAttribute]
        public string Name { get; set; }

        [EmailAddressAttribute]
        [RequiredAttribute]
        public string Email { get; set; }
    }
}
