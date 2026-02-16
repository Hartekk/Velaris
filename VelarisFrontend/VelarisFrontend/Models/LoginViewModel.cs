using System.ComponentModel.DataAnnotations;

namespace VelarisFrontend.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username required for sign in")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
    }
}