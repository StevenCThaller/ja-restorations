using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Sale 
    {
        public Sale() {}
        public Sale(SaleForm saleForm) 
        {
            furnitureId = saleForm.furnitureId;
            finalPrice = saleForm.finalPrice;
            dateSold = saleForm.dateSold;
        }
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

    public class SaleForm
    {
        [Required]
        [Range(1, Double.PositiveInfinity)]
        public int furnitureId { get; set; }
        [Required(ErrorMessage = "You must enter a price.")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "You can't honestly have sold this for less than $1.")]
        public decimal finalPrice { get; set; }
        [Required(ErrorMessage = "You must enter a date that this was sold.")]
        public DateTime dateSold { get; set; }
    }
}