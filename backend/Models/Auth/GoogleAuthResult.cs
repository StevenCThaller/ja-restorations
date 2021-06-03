using System.ComponentModel.DataAnnotations;

namespace backend.Models.Auth
{
    public class GoogleAuthRequest : IAuthRequest
    {
      [Required]
      public string Token {get; set;}
      
      [Required]
      public string Email {get; set;}
      public string GivenName {get;set;}
      public string FamilyName {get;set;}

    }
}