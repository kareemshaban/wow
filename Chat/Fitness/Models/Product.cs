using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Product
    {      
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string ProductName { get; set; }
        [Column(TypeName = "nvarchar(550)")]
        public decimal Price { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ImgUrl { get; set; }
        [ForeignKey("Id")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int DaysCount { get; set; }
    }
}
