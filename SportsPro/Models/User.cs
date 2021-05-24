using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
