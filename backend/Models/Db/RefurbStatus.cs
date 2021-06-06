using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RefurbStatus 
    {
        [Key]
        public int refurbStatusId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}