using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace VelarisFrontend.Models
{
    public class ButtonViewModel
    {
        public string Label { get; set; }
        public string Url { get; set; }
        public string Type { get; set; } 
        public string CssClass { get; set; }
    }
}