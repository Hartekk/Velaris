

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VelarisFrontend.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required(ErrorMessage ="Set a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Enter an email address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password must match")]
        [Compare("Password"), Display(Name ="Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}