using Chat.Models;
using Chat.Models.ViewModel;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using FolaChat.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowBlockUserController : ControllerBase
    {
        private readonly IFitnessRepositry<ApplicationUser> userManager;
        private readonly IFitnessRepositry<UserImage> userImageDb;
        private readonly IFitnessRepositry<UserImageLike> userImageLikeDb;
        private readonly UserManager<ApplicationUser> userManager1;
        private readonly IFitnessRepositry<Country> countryDb;
        private readonly IFitnessRepositry<FollowUser> followDb;
        private readonly IFitnessRepositry<BlockUser> blockDb;
        private readonly IFitnessRepositry<UserProduct> db;
        private readonly IFitnessRepositry<Category> categorydb;
		private readonly IFitnessRepositry<ChatRoom> dbRoom;
        private readonly IFitnessRepositry<UserGift> dbGift;
        private readonly IFitnessRepositry<Notification> notificationDb;


        public IFitnessRepositry<Visitor> VisitorDb { get; }

        public FollowBlockUserController(IFitnessRepositry<ApplicationUser> userManager, IFitnessRepositry<ChatRoom> dbRoom, IFitnessRepositry<UserGift> dbGift, IFitnessRepositry<UserImage> UserImageDb, IFitnessRepositry<UserImageLike> UserImageLikeDb, UserManager<ApplicationUser>_userManager, IFitnessRepositry<Visitor> visitorDb, IFitnessRepositry<Country> countryDb, IFitnessRepositry<FollowUser> followDb, IFitnessRepositry<BlockUser> blockDb, IFitnessRepositry<UserProduct> db, IFitnessRepositry<Category> categorydb  ,
            IFitnessRepositry<Notification> notificationDb)
        {
            this.userManager = userManager;
            this.dbRoom = dbRoom;
            this.dbGift = dbGift;
            userImageDb = UserImageDb;
            userImageLikeDb = UserImageLikeDb;
            userManager1 = _userManager;
            VisitorDb = visitorDb;
            this.countryDb = countryDb;
            this.followDb = followDb;
            this.blockDb = blockDb;
            this.db = db;
            this.categorydb = categorydb;
            this.notificationDb = notificationDb;
        }
        #region 
        [HttpPost("Follow")]
        public async Task<object> Follow(FollowBlockViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.NewUser))
            {
                if (followDb.firstOrdefault(0, model.ApplicationUserId, model.NewUser) == null)
                {
                    FollowUser f = new FollowUser();
                    f.ApplicationUserId = model.ApplicationUserId;
                    f.date = DateTime.Now;
                    f.FollowedUserId = model.NewUser;
                    try
                    {
                        if (await followDb.Add(f))
                        {
                            Notification notification = new Notification()
                            {
                                Create_at = DateTime.Now,
                                ApplicationUserId = model.NewUser,
                                NewUserId = model.ApplicationUserId,
                                Desc = "",
                                Title = "لديك متابعة جديدة",
                                Type = 1
                            };
                            await notificationDb.Add(notification);
                            return (new { message = "Success" });
                        }
                        return (new { message = "Error : User  not added" });
                    }
                    catch (Exception e)
                    {
                        return (new { message = "Error : Execption" + e.Message });
                    }
                }
                else
                    return (new { message = "Error : You are following this user befor" });
            }
            return (new { message = "Error : Data not complete" });
        }
        [HttpPost("PostNotification")]
        public async Task<object> PostNotification(NotificationViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.NewUserId) && !string.IsNullOrEmpty(model.Title))
            {

                Notification notification = new Notification()
                {
                    Create_at = DateTime.Now,
                    ApplicationUserId = model.ApplicationUserId,
                    NewUserId = model.NewUserId,
                    Desc = model.Desc,
                    Title = model.Title,
                    Type = model.Type
                };
                try
                    {
                        if (await notificationDb.Add(notification))
                        {

                            return (new { message = "Success" });
                        }
                        return (new { message = "Error : User  not added" });
                    }
                    catch (Exception e)
                    {
                        return (new { message = "Error : Execption" + e.Message });
                    }
              
            }
            return (new { message = "Error : Data not complete" });
        }
        [HttpPost("AllNotification/{user}")]
        public List<NotificationViewModel> AllNotification([FromRoute]string user)
        {
            List<NotificationViewModel> notifications = new List<NotificationViewModel>();
            NotificationViewModel item = new NotificationViewModel();
            ApplicationUser userObj = new ApplicationUser();
            List<Notification> list = notificationDb.list(user).ToList();
            for(int i = 0; i < list.Count; i++)
            {
                item = new NotificationViewModel();
                item.Title = list[i].Title;
                item.Desc = list[i].Desc;
                item.ApplicationUserId = list[i].NewUserId;
                item.NewUserId = list[i].ApplicationUserId;
                item.Type = list[i].Type;
                item.Create_at = list[i].Create_at;
                userObj =  userManager.firstOrdefault(0, list[i].ApplicationUserId);
                item.NewUser = userObj;
                userObj = userManager.firstOrdefault(0, list[i].NewUserId);
                item.ApplicationUser = userObj;

                notifications.Add(item);
            }

            return notifications;
        }

        [HttpPost("UnFollow")]
        public async Task<object> UnFollow(FollowBlockViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.NewUser))
            {
                if (followDb.firstOrdefault(0, model.ApplicationUserId, model.NewUser) == null)
                {
                    return (new { message = "Success : User not found" });
                }
                else
                {
                    if (await followDb.Delete(0, null, new List<FollowUser>(), model.ApplicationUserId, model.NewUser))
                    {
                        return (new { message = "Success" });
                    }
                    return (new { message = "Error : User  not deleted" });
                }

            }
            return (new { message = "Error : Data not complete" });
        }
        [HttpPost("AllFollowing")] 
        public List<FollowUserViewModel> AllFollowing(UserInfoModel model)
        {
            return followDb.IList(model.ApplicationUserId, "AllFollowing").Select(a => new FollowUserViewModel { Id = a.Id, date = a.date, FollowedUserId = a.FollowedUserId, FulName = a.FollowedUser.FulName }).ToList();
        }
        [HttpPost("AllFollowers")] 
        public List<FollowUserViewModel> AllFollowers(UserInfoModel model)
        {
            return followDb.IList(model.ApplicationUserId).Select(a => new FollowUserViewModel { Id = a.Id, date = a.date, FollowedUserId = a.ApplicationUser.Id, FulName = a.ApplicationUser.FulName }).ToList();
        }
        #endregion
        #region 
        [HttpPost("Block")]
        public async Task<object> Block(FollowBlockViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.NewUser))
            {
                if (blockDb.firstOrdefault(0, model.ApplicationUserId, model.NewUser) != null)
                    return (new { message = "Success : Added befor" });
                BlockUser f = new BlockUser();
                f.ApplicationUserId = model.ApplicationUserId;
                f.date = DateTime.Now;
                f.blockedUserId = model.NewUser;
                if (await blockDb.Add(f))
                {
                    return (new { message = "Success" });
                }
                return (new { message = "Error : User  not added" });
            }
            return (new { message = "Error : Data not complete" });
        }
        [HttpPost("UnBlock")]
        public async Task<object> UnBlock(FollowBlockViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.NewUser))
            {
                if (await blockDb.Delete(0, null, new List<BlockUser>(), model.ApplicationUserId, model.NewUser))
                {
                    return (new { message = "Success" });
                }
                return (new { message = "Error : User  not deleted" });
            }
            return (new { message = "Error : Data not complete" });
        }
        [HttpPost("AllBlocking")] 
        public List<FollowUserViewModel> AllBlocking(UserInfoModel model)
        {
            return blockDb.IList(model.ApplicationUserId).Select(a => new FollowUserViewModel { Id = a.Id, date = a.date, FollowedUserId = a.blockedUserId, FulName = a.blockedUser.FulName }).ToList();
        }
        #endregion
        #region Search users
        [HttpPost("SearchUser")]  
        public async Task<UserDataInfoViewModel> SearchUser(FollowBlockViewModel model)  
        {
            List<ApplicationUser> _users = userManager.list().Where(a => a.UserId.ToString() == model.ApplicationUserId).ToList();
            ApplicationUser user = new ApplicationUser();
            if (_users.Count > 0)
            {
                if (_users.Count > 1)
                {
                    foreach (var item in _users)
                    {
                        List<string> userRoles = userManager1.GetRolesAsync(item).Result.ToList();
                        if (userRoles.Where(a => a.Contains("AddUsers") || a.Contains("ShowUsersChat") || a.Contains("AdminstrationChat") || a.Contains("Admin") || a.Contains("") || a.Contains("")).Count() == 0)
                        {
                            user = item;
                            break;
                        }
                    }
                }
                else
                    user = _users[0];
                //2- هشوف لو انا عملتله بلوك ولا              
                BlockUser blocked = blockDb.firstOrdefault(0, user.Id, model.NewUser);
                if (blocked == null)
                {
                    blocked = blockDb.firstOrdefault(0, model.NewUser, user.Id);
                    if (blocked == null)
                    {
                        return await getData(user.Id.ToString());
                    }
                    else
                        return new UserDataInfoViewModel();
                }
                else
                    return new UserDataInfoViewModel();
            }
            else
                return new UserDataInfoViewModel();
        }
        #endregion
        #region        
        [HttpPost("UserData")]
        public async Task<UserDataInfoViewModel> UserData(UserInfoModel model)
        {
            if (string.IsNullOrEmpty(model.ApplicationUserId))
                return null;
            return await getData(model.ApplicationUserId);
        }
        private async Task<UserDataInfoViewModel> getData(string ApplicationUserId)
        {
          
             
            ApplicationUser user = userManager.firstOrdefault(0, ApplicationUserId);
            if (user != null)
            {
                LevelModelView level = new LevelModelView()
                {
                    GiftSendCount = user.Level.GiftSendCount,
                    Id = user.Level.Id,
                    ImgUrl = user.Level.ImgUrl,
                    LevelName = user.Level.LevelName,
                    UserId = user.Id,
                    Color = user.Level.Color
                };
                OtherLevelViewModel otherLevel = new OtherLevelViewModel()
                {
                    GiftRecieverCount = user.OtherLevel.GiftRecieverCount,
                    Id = user.OtherLevel.Id,
                    ImgUrl = user.OtherLevel.ImgUrl,
                    LevelName = user.OtherLevel.LevelName,
                    UserId = user.Id,
                    Color = user.OtherLevel.Color
                };
                ChargingLevelViewModel chargingLevel = new ChargingLevelViewModel()
                {
                    Balance = user.ChargingLevel.BalanceCount,
                    Id = user.ChargingLevel.Id,
                    ImgUrl = user.ChargingLevel.ImgUrl,
                    LevelName = user.ChargingLevel.LevelName,
                    UserId = user.Id,
                    Color = user.ChargingLevel.Color
                };
                  
                IEnumerable<FollowUser> users = followDb.IList("All").Where(a => a.ApplicationUserId == ApplicationUserId || a.FollowedUserId == ApplicationUserId);
                List<FollowUserViewModel> _following = users.Where(a => a.ApplicationUserId == ApplicationUserId).Select(a => new FollowUserViewModel { Id = a.Id, date = a.date, FollowedUserId = a.FollowedUserId, FulName = a.FollowedUser.FulName, ImgUrl = a.FollowedUser.ImgUrl }).ToList();
                _following = _following.Where(a => a.FollowedUserId != ApplicationUserId).ToList(); ///عشان  مرجعش  فيهم الاكونت بتاعى  كباحث
                
                List<FollowUserViewModel> _followers = users.Where(a => a.FollowedUserId == ApplicationUserId).Select(a => new FollowUserViewModel { Id = a.Id, date = a.date, FollowedUserId = a.ApplicationUserId, FulName = a.ApplicationUser.FulName, ImgUrl = a.ApplicationUser.ImgUrl }).ToList();
                _followers = _followers.Where(a => a.FollowedUserId != ApplicationUserId).ToList();
                 
                List<FollowUserViewModel> visitors = VisitorDb.IList(ApplicationUserId).Select(a => new FollowUserViewModel { Id = a.Id, date = a.date, FollowedUserId = a.ApplicationUserId, FulName = a.ApplicationUser.FulName, ImgUrl = a.ApplicationUser.ImgUrl }).ToList();
                
                //List<UserProductViewModel> allUserProducts = db.IList(ApplicationUserId).Where(a=>a.IsDeleted==false).Select(a => new UserProductViewModel
				List<UserProductViewModel> allUserProducts = db.IList(ApplicationUserId).Select(a => new UserProductViewModel 
                { Id = a.Id, Used = a.Used, ApplicationUserId = a.ApplicationUserId, ProductId = a.ProductId, ImgUrl = a.Product.ImgUrl, Price = a.Product.Price, ProductName = a.Product.ProductName, DaysCount = a.DaysCount, date = a.date, CategoryId = a.Product.CategoryId }).ToList();
                List<UserProductViewModel> userProducts = new List<UserProductViewModel>();

                foreach (UserProductViewModel item in allUserProducts)
                {
                    if (productValied(item.date, item.DaysCount))
                    {
                        item.DaysCount = item.DaysCount - Convert.ToInt16((DateTime.Now - item.date).TotalDays);
                        userProducts.Add(item);
                    }
                }
                List<Category> cats = categorydb.list();
                List<CategoryViewModelProducts> x1 = new List<CategoryViewModelProducts>();
                CategoryViewModelProducts x2 = new CategoryViewModelProducts();
                foreach (var cat in cats)
                {
                    if (userProducts.Where(a => a.CategoryId == cat.Id).Count() > 0)
                    {
                        x2.ImgUrl = cat.ImgUrl;
                        x2.CatName = cat.CatName;
                        x2.Id = cat.Id;
                        x2.userProducts = userProducts.Where(a => a.CategoryId == cat.Id).ToList();
                        x1.Add(x2);
                        x2 = new CategoryViewModelProducts();
                    }
                }
            
                UserDataInfoViewModel r = new UserDataInfoViewModel();
                    r.ChargingLevel = chargingLevel;
                    Country c = countryDb.firstOrdefault(user.CountryId);
                    if (c != null)
                    {
                        r.Name = c.Name;
                    }

                List<ApplicationUser> ApplicationUsers = userManager.list();            
                List<ApplicationUser> otherUsers = new List<ApplicationUser>();
                foreach (var AppUser in ApplicationUsers)
                {
                 IList<string>  userRoles =await  userManager1.GetRolesAsync(AppUser);
                    userRoles = userRoles.ToList();  
                        if (_following.Where(a => a.FollowedUserId == AppUser.Id).Count() == 0 && _followers.Where(a => a.FollowedUserId == AppUser.Id).Count() == 0)
                        {
                            otherUsers.Add(AppUser);
                    }
                }
                r.otherUsers = otherUsers.Select(a => new FollowUserViewModel { Id = 0, date = a.RegisterDate, FollowedUserId = a.Id, FulName = a.FulName, ImgUrl = a.ImgUrl }).ToList();            
                r.FulName = user.FulName;
                r.Email = user.Email;
                r.DateBirth = user.DateBirth;
                r.ImgUrl = user.ImgUrl;
                r.UserName = user.UserName;
                r.Gender = user.Gender;
                r.CountryId = user.CountryId;
                if (user.userblocked && user.days > 0)
                {
                    if (DateTime.Now.Subtract(user.blockedStartDate).Days > user.days)
                    {
                        user.userblocked = false;
                        user.days = 0;
                        await userManager.update(user);
                    }
                }
                r.userblocked = user.userblocked;
                r.days = user.days;
                r.cat = x1;
                r.posts = user.posts.Select(p => new posts { Id = p.Id, content = p.content, ImgUrl = p.ImgUrl, publishDate = p.publishDate }).ToList();
                r.following = _following;
                r.followers = _followers;
                r.visitors = visitors;
                r.Id = user.Id;
                r.Level = level;
                r.OtherLevel = otherLevel;
                r.UserId = user.UserId;
                r.About = !string.IsNullOrEmpty(user.about) ? user.about : "";
                r.ReceiveMsg = user.ReceiveMsg;
                r.ReceiveInvitation = user.ReceiveInvitation;
                r.ReceiveChatRoomMsg = user.ReceiveChatRoomMsg;
                r.InterstedWith = user.InterstedWith;
                r.Tower = user.Tower;
             
                List<FollowUserViewModel> friends = new List<FollowUserViewModel>();                
                foreach (var item in _following)
                {
                    if (_followers.Where(a=>a.FollowedUserId == item.FollowedUserId).Count() > 0)
                    {
                        friends.Add(item);
                    }
                }
                r.Friends = friends.Where(a=>a.FollowedUserId != ApplicationUserId).ToList();  

				IEnumerable<UserGift> allgifts = dbGift.IList();
                //الهدايا اللى  ارسلتها
                List<UserGiftViewModel> send = allgifts.Where(a => a.ApplicationUserId == ApplicationUserId).ToList()
                      .Select(a => new UserGiftViewModel { ApplicationUserId = a.ApplicationUserId, GiftId = a.GiftId, NewUserId = a.NewUserId, DaysCount = a.Gift.DaysCount, Price = a.Gift.Price, GiftName = a.Gift.GiftName, Id = a.Id, ImgUrl = a.Gift.ImgUrl, Used = a.Used , ChatRoomID = a.ChatRoomID }).ToList();
                r.GiftSend = send;
                // الهدايا اللى استقبلتها
                List<UserGiftViewModel> Receive = allgifts.Where(a => a.NewUserId == ApplicationUserId).ToList()
                 .Select(a => new UserGiftViewModel { ApplicationUserId = a.ApplicationUserId, GiftId = a.GiftId, NewUserId = a.NewUserId, DaysCount = a.Gift.DaysCount, Price = a.Gift.Price, GiftName = a.Gift.GiftName, Id = a.Id, ImgUrl = a.Gift.ImgUrl, Used = a.Used }).ToList();
                r.GiftReceive = Receive;

                r.gallery= userImageDb.list(ApplicationUserId).Select((a) => new CustomUserImage { ImgUrl = a.ImgUrl }).ToList();
                r.galleryLikers = userImageLikeDb.IList(ApplicationUserId).Select(a => new CustomUserImageLike { LikerId=a.LikerId, LikerImg=a.Liker.ImgUrl, LikerName=a.Liker.FulName  }).ToList();
                return r;
            }
            else
                return new UserDataInfoViewModel();
        }

        public bool productValied(DateTime startDate, int count)
        {
            DateTime day = DateTime.Now;
            DateTime endDate = startDate.AddDays(count);
            int LargeEqual = DateTime.Compare(day, startDate);
            int LessEqual = DateTime.Compare(day, endDate);
            if (LargeEqual >= 0 && LessEqual <= 0)
            {
                return true;
            }
            return false;
        }
        #endregion
 
    }
    public class CategoryViewModelProducts
    {
        public int Id { get; set; }
        public string CatName { get; set; }
        public string ImgUrl { get; set; }
        public List<UserProductViewModel> userProducts { get; set; }
    }

}