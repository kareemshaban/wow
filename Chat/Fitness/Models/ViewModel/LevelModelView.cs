using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class LevelModelView
    {
        public int Id { get; set; }
        public int GiftSendCount { get; set; }
        public string LevelName { get; set; }
        public string ImgUrl { get; set; }
        public List<string> ApplicationUserId { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }

    }
}
