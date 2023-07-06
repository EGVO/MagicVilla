using System.Net;
using System.Security.Principal;

namespace MagicVilla_API.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessful { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        public object Result { get; set; }

        public int TotalPages { get; set; }
    }
}
