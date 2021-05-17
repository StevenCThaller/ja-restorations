using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class FurnitureHasColor
    {
        [Key]
        public int furnitureHasColorId { get; set; }

        [ForeignKey("Furniture")]
        public int furnitureId { get; set; }
        public Furniture furniture { get; set; }

        [ForeignKey("Color")]
        public int colorId { get; set; }
        public Color color { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}