using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    
    public class Color
    {
        [Key]
        public int colorId { get; set; }
        public string name { get; set; }
        public List<FurnitureHasColor> pieces { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}