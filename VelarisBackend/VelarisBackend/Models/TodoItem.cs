using System;
using System.ComponentModel.DataAnnotations;

namespace VelarisBackend.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
        public DateTime DueDate { get; set; }
    }
}