using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace backend.Models
{
    public class FurnitureLike 
    {
        public FurnitureLike(){}
        public FurnitureLike(FurnitureLikeForm furnitureLikeForm)
        {
            userId = furnitureLikeForm.userId;
            furnitureId = furnitureLikeForm.furnitureId;
        }

        [Key]
        public int furnitureLikeId { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; } 
        public User user { get; set; }

        [ForeignKey("Furniture")]
        public int furnitureId { get; set; }
        public Furniture furniture { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }

    public class FurnitureLikeForm 
    {
        public int furnitureId { get; set; }
        public int userId { get; set; }
    }
}