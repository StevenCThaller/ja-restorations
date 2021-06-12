using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class RefurbishRequestImage
    {
        [Key]
        public int refurbishRequestImageId { get; set; }
        [ForeignKey("RefurbishRequest")]
        public int refurbishRequestId { get; set; }
        public RefurbishRequest refurbishRequest { get; set; }

        [ForeignKey("S3Image")]
        public int s3ImageId { get; set; }
        public S3Image s3Image { get; set; }
        [NotMapped]
        public string url 
        {
            get
            {
                if(s3Image == null) {
                    return "";
                } else {
                    return s3Image.url;
                }
            }
            protected set
            {
                url = value;
            }
        }

        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;

    }
}