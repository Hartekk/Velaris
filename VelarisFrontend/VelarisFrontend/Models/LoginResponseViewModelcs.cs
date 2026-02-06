using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VelarisFrontend.Models
{
    public class LoginResponseViewModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}