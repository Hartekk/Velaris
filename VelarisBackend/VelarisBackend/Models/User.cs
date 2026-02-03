using System;
using System.ComponentModel.DataAnnotations;

namespace VelarisBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}