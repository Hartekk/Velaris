using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VelarisBackend.DTOs
{
    public class ToDoItemEditReq
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}