
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas
{
    public class LoginCheck
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}