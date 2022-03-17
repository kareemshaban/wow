using Fitness.Controllers;
using Fitness.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class UserDataInfoViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateBirth { get; set; }
        public int Gender { get; set; }
        public string FulName { get; set; }
        public string ImgUrl { get; set; }
        public List<FollowUserViewModel> following { get; set; }
        public List<FollowUserViewModel> followers { get; set; }
        public List<posts> posts { get; set; }
        public List<CategoryViewModelProducts> cat { get; set; }
        public OtherLevelViewModel OtherLevel { get; set; }
        public LevelModelView Level { get; set; }
        public int UserId { get; set; }
        public List<FollowUserViewModel> visitors { get; set; }
        public string About { get; set; }
        public bool userblocked { get; set; }
        public string Name { get; set; } 
        public ChargingLevelViewModel ChargingLevel { get; set; }
        public int InterstedWith { get; set; } 
        public bool ReceiveMsg { get; set; } 
        public bool ReceiveInvitation { get; set; }
        public bool ReceiveChatRoomMsg { get; set; } 
        public List<FollowUserViewModel> otherUsers { get; set; }
        public int days { get; set; } 
        public string Tower { get; set; }
        public List<FollowUserViewModel> Friends { get; set; }
        public List<CustomUserImage> gallery { get; set; }
        public List<CustomUserImageLike> galleryLikers { get; set; }
        public int CountryId { get; set; }
        public List<UserGiftViewModel> GiftSend { get; set; }
        public List<UserGiftViewModel> GiftReceive { get; set; }
    }
    public class posts
    {
        public int Id { get; set; }
        public string content { get; set; }
        public string ImgUrl { get; set; }
        public DateTime publishDate { get; set; }
        public bool IsPost { get; set; }
    }

    public class CustomUserImage{
    public string ImgUrl { get; set; }
}
    public class CustomUserImageLike
    {
        public string LikerId { get; set; }
        public string LikerName { get; set; }
        public string LikerImg { get; set; }
    }
}
