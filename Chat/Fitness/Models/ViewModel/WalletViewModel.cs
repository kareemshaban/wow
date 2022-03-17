using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class WalletViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastUpdate { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal DiamonadBalance { get; set; }// رصيد المستخدم من الماس
    }
}
