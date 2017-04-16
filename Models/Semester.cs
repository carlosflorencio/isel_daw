using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAW_API.Models
{
    public class Semester {

        public int Id { get; set; }

        public int Year {get; set; }

        [Required]
        public Term Term { get; set; }
    }

    public enum Term
    {
        Winter = 0,
        Spring = 1
    }
}

