using System.ComponentModel.DataAnnotations;

namespace backend.Models.Auth
{
    public class TwitterAuthRequest : IAuthRequest
    {
      [Required]
      public string Token {get; set;}
      
      [Required]
      public string Email {get; set;}
    }
}