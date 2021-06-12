using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class RefurbishRequest
    {
        [Key]
        public int refurbishRequestId { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
        public User user { get; set; }

        public string description { get; set; }

        public List<S3Image> images { get; set; }
        
    }
}