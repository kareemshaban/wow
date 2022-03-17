using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class FollowUserViewModel
    {
        public int Id { get; set; }
        public string FollowedUserId { get; set; }
        public string FulName { get; set; }
        public DateTime date { get; set; }
        public string ImgUrl { get; set; }      
    }
}
