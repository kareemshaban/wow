using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(80)")]
        public string CatName { get; set; }      
        [Column(TypeName = "nvarchar(100)")]
        public string ImgUrl { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Gift> gifts { get; set; }
        public bool IsGift { get; set; }

    }
}
