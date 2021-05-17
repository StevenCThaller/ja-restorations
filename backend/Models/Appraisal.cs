using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Appraisal
    {
        [Key]
        public int appraisalId { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
        public User user { get; set; }
        
        public int phoneNumber { get; set; }

        public string description { get; set; }
        public List<AppraisalImage> images { get; set; }
        public decimal height { get; set; }
        public decimal length { get; set; }
        public decimal width { get; set; }

        public bool sturdy { get; set; }
        public int estimatedWeight { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}