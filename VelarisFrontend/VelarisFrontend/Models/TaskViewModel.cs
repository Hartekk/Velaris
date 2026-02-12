using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VelarisFrontend.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}