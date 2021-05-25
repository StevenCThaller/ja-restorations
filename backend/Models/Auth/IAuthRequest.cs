namespace backend.Models.Auth
{
  public interface IAuthRequest
  {
    public string Token {get; set;}
    public string Email {get; set;}
  }
}