using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Gift
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string GiftName { get; set; }
        [Column(TypeName = "nvarchar(550)")]
        public decimal Price { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ImgUrl { get; set; }
        [ForeignKey("Id")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int DaysCount { get; set; }
        public bool IsReceived { get; set; }
        public bool  IsDeleted { get; set; }
        public string SoundUrl { get; set; }

    }
}
