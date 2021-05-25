using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Furniture
    {
        [Key]
        public int furnitureId { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        [ForeignKey("FurnitureType")]
        public int typeId { get; set; }
        public FurnitureType type { get; set; }

        public List<FurnitureHasColor> colors { get; set; }
        public List<FurnitureImage> images { get; set; }

        public decimal height { get; set; }
        public decimal length { get; set; }
        public decimal width { get; set; }

        public int estimatedWeight { get; set; }
        
        public decimal priceFloor { get; set; }
        public decimal? priceCeiling { get; set; }
        
        public virtual Sale sale { get; set; }

        [NotMapped]
        public bool sold 
        {
            get { return sale != null; }
        }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
    

    public class NewFurnitureForm
    {
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public int typeId { get; set; }

        public string type { get; set; }


        public List<int> colorIds { get; set; }

        [Required(ErrorMessage="A height is required. This should be in inches.")]
        [Range(1, Double.PositiveInfinity)]

        public decimal height { get; set; }
        [Required(ErrorMessage="A length is required. This should be in inches.")]
        [Range(1, Double.PositiveInfinity)]

        public decimal length { get; set; }
        [Required(ErrorMessage="A width is required. This should be in inches.")]
        [Range(1, Double.PositiveInfinity)]

        public decimal width { get; set; }

        [Required(ErrorMessage="Estimated weight is required.")]
        [Range(1, Double.PositiveInfinity)]
        public int estimatedWeight { get; set; }

        [Required(ErrorMessage="You must enter a price.")]
        [Range(1, Double.PositiveInfinity)]
        public decimal priceFloor { get; set; }
        
        public decimal? priceCeiling { get; set; }


        public DateTime updatedAt { get; set; } = DateTime.Now;

    }

    // two types of requests: 
    // 1. furniture sale - people selling
        // dimensions
        // pictures
        // wobbly
        // estimated weight
        // email - required
        // phone number - optional
        // first name - required
        // last name - required
    // 2. restoration - people looking to get somethign restored a certain way
        // dimensions
        // pictures
        // description - any suggestions on what they'd like done
        // wobbly
        // estimated weight
        // email - required
        // phone number - optional
        // first name - required
        // last name - required
    
}