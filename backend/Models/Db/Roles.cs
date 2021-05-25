using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Administrator
    {
        [Key]
        public int adminId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public User user { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
    public class Employee
    {
        [Key]
        public int employeeId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public User user { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}