using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class UserProductViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string NewUserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public bool Used { get; set; }
        public int DaysCount { get; set; }
        public DateTime date { get; set; }
        public int CategoryId { get; set; }
    }
}
