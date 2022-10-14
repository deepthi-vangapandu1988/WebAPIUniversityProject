using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Dto
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
