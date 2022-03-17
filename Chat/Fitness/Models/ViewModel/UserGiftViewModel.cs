using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class UserGiftViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string NewUserId { get; set; }
        public int GiftId { get; set; }
        public string GiftName { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public bool Used { get; set; }
        public int DaysCount { get; set; }
        public string SoundUrl { get; set; }
        public int? ChatRoomID { get; set; }
    }
}
