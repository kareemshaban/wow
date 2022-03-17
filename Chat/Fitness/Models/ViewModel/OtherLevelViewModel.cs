using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class OtherLevelViewModel
    {
        public int Id { get; set; }
        public int GiftRecieverCount { get; set; }
        public string LevelName { get; set; }
        public List<string> ApplicationUserId { get; set; }
        public string ImgUrl { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }
    }
    public class ChargingLevelViewModel
    {
        public int Id { get; set; }
        public int Balance { get; set; }
        public string LevelName { get; set; }
        public List<string> ApplicationUserId { get; set; }
        public string ImgUrl { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }
    }
}
