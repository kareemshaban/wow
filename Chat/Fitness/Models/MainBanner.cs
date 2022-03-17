using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class MainBanner
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(420)")]
        public string MainImage { get; set; }//required
        [Column(TypeName = "nvarchar(420)")]
        public string Img1 { get; set; }//Required
        [Column(TypeName = "nvarchar(420)")]
        public string Img2 { get; set; }
        [Column(TypeName = "nvarchar(420)")]
        public string Img3 { get; set; }
        [Column(TypeName = "nvarchar(420)")]
        public string Img4 { get; set; }
        [Column(TypeName = "nvarchar(120)")]
        public string actions { get; set; }
    }
}
