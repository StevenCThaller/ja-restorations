using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class AppraisalStatus
    {
        [Key]
        public int appraisalStatusId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}