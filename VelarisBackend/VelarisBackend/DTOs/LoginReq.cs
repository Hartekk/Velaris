using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VelarisBackend.DTOs
{
    public class LoginReq
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}