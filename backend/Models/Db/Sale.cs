using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Sale 
    {
        [Key]
        public int saleId { get; set; }

        [ForeignKey("Furniture")]
        public int furnitureId { get; set; }
        public Furniture furniture { get; set; }

        public decimal finalPrice { get; set; }
        public DateTime dateSold { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}