using Newtonsoft.Json;


namespace VelarisFrontend.Models
{
    public class LoginResponseViewModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}