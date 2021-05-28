using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
  public class RefreshToken
  {
    [Key]
    [JsonIgnore]
    public int tokenId {get; set;}
    public string token {get; set;}
    public DateTime expirationDate {get; set;}
    public bool isExpired => DateTime.UtcNow >= expirationDate;
    public string createdByIp { get; set; }
    public DateTime? revoked { get; set; }
    public string revokedByIp { get; set; }
    public string replacedByToken { get; set; }
    public bool isActive => revoked == null && !isExpired;
    [ForeignKey("User")]
    public int userId { get; set; }
    public User user { get; set; }
    public DateTime createdAt { get; set; } = DateTime.Now;
    public DateTime updatedAt { get; set; } = DateTime.Now;
  }
}