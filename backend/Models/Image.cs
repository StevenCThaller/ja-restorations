using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace backend.Models
{
    public class S3Image
    {
        [Key]
        public int s3ImageId { get; set; }
        public string url { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }

    public class FileModel
    {
        // public string FileName { get; set; }
        public List<string> FileNames { get; set; }
        public List<IFormFile> FormFiles { get; set; }
    }
}