using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VelarisFrontend.Models
{
    public class ModalViewModel
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Show { get; set; }
        public TaskViewModel Task { get; set; }
    }
}