using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Dto
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
