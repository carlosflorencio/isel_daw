using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.TransferModels.ResponseModels
{
    public class ProblemJson {

        public const string MediaType = "application/problem+json";

        public string Type { get; set; } // must be a uri
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public List<InvalidParam> InvalidParams { get; set; }
    }

    public class InvalidParam
    {
        public string Name { get; set; }
        public string Reason { get; set; }
    }

}
