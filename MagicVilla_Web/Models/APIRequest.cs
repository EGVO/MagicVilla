using static MagicVilla_utility.SD;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        public APIType APIType { get; set; } = APIType.GET;

        public string Url { get; set; }

        public object Data { get; set; }

        public string Token { get; set; }
    }
}
