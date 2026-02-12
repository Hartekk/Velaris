

using System;

namespace VelarisBackend.DTOs
{
    public class ToDoItemAddReq
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
    }
}