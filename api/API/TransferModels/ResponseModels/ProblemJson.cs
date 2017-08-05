using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.TransferModels.ResponseModels
{
    public class InvalidParam
    {
        public string Name { get; set; }
        public string Reason { get; set; }
    }

    public class ProblemJson {

        public const string MediaType = "application/problem+json";

        public string Type { get; set; } // must be a uri
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public List<InvalidParam> InvalidParams { get; set; }


        public static ProblemJson Create(int code, string message = null)
        {
            switch (code)
            {
                case 401:
                    return new ProblemJson{
                        Type = "/unauthorized",
                        Status = (int)code,
                        Title = "Unauthorized Request",
                        Detail = "You are not authenticated to do this action"
                    };
                case 403:
                    return new ProblemJson{
                        Type = "/forbidden",
                        Status = (int)code,
                        Title = "Forbidden Request",
                        Detail = "You are not authorized to do this action"
                    };
                default:
                    return new ProblemJson{
                        Type = "/server-error",
                        Status = (int)code,
                        Title = "Unhandled Exception",
                        Detail = message
                    };
            }
        }
    }

}
