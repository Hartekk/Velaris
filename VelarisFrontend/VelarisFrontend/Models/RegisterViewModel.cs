

using System.ComponentModel.DataAnnotations;

namespace VelarisFrontend.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string Email { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}