using Fitness.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class UserProduct
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Column(TypeName = "int")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public DateTime date { get; set; }
        public bool Used { get; set; }
        public string NewUserId { get; set; }
        public virtual ApplicationUser NewUser { get; set; }
        public bool IsReceived { get; set; }
        public int DaysCount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime deleteDate { get; set; }
        [NotMapped]
        public string UserDis { get; set; }

    }
  
}
