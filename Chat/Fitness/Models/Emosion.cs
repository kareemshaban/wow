using Fitness.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Emosion
    {
        [Key]
        public int Id { get; set; }
        public decimal Price{ get; set; }
        public string ImgUrl { get; set; }
        public DateTime createDate { get; set; }
        [NotMapped]
        public IFormFile EmoImage{ get; set; }
    }
}
