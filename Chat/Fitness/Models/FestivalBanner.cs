using Fitness.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class FestivalBanner
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(420)")]
        public string MainImage { get; set; }//required
        [Column(TypeName = "nvarchar(Max)")]
        public string Msg { get; set; }//Required
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int RoomId { get; set; }
        [Column(TypeName = "nvarchar(120)")]
        public int DaysCount { get; set; }
        public bool Approve { get; set; }
        public DateTime StartDate { get; set; }
        public bool Refused { get; set; }
    }
}
