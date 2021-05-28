using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class AuthRequest
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public string email { get; set; }
    }
}