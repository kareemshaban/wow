using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class FestivalBannerViewModel
    {
        public int Id { get; set; }
        public string MainImage { get; set; }//required
        public IFormFile  img{ get; set; }
        public string Msg { get; set; }//Required
        public string ApplicationUserId { get; set; }
        public string FulName { get; set; }
        public string ImgUrl { get; set; }
        public string RoomId { get; set; }
        public string DaysCount { get; set; }
        public string StartDate { get; set; }
        public bool Approve { get; set; }
        public string UserId { get; set; }
    }
}
