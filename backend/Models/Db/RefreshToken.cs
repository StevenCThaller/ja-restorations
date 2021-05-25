using System;

namespace backend.Models.Auth
{
  public class RefreshToken
  {
    public int TokenId {get; set;}
    public string Token {get; set;}
    public DateTime ExpirationDate {get; set;}
    public bool isRevoked {get; set;} = false;
    
  }
}