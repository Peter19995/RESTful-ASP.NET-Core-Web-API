using System.Net;

namespace MagicVill_VillaAPI.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSucess { get; set; }

        public List<string> ErrorMessages { get; set;}

        public object Result { get; set; }


    }
}
