using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class FurnitureType
    {
        [Key]
        public int typeId { get; set; }
        public string name { get; set; }

        public List<Furniture> pieces { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }

    public class TypeView
    {
        [Required(ErrorMessage="This is a required field.")]
        public string name { get; set; }
    }
}