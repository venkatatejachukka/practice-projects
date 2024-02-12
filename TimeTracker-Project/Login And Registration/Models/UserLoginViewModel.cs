using System.ComponentModel.DataAnnotations;

namespace MVC_TimeTracker.Models
{
    public class UserLoginViewModel
    {
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
