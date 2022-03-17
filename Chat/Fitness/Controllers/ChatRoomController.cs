using Chat.Controllers;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controllers
{
    [Route("api/ChatRoom")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFitnessRepositry<Wallet> dbWallet;
        private readonly IFitnessRepositry<MicClosedState> dbMicClosed;
        private readonly IFitnessRepositry<Emosion> dbEmosion;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<UserProduct> dbUserProduct;
        private readonly IFitnessRepositry<Music> dbMusic;
        private readonly IFitnessRepositry<ChatRoomFollower> dbFollow;
        private readonly IFitnessRepositry<ChatRoom> dbRoom;
        private readonly IFitnessRepositry<ChatRoomMember> dbMember;
        private readonly IFitnessRepositry<ChatRoomMsg> dbMsg;
        private readonly IFitnessRepositry<UserGift> dbGift;
        public ChatRoomController(UserManager<ApplicationUser> userManager, IFitnessRepositry<Wallet> dbWallet, IFitnessRepositry<MicClosedState> dbMicClosed, IFitnessRepositry<Emosion> dbEmosion, IHostingEnvironment hostingEnvironment, IFitnessRepositry<UserProduct> dbUserProduct, IFitnessRepositry<Music> dbMusic, IFitnessRepositry<ChatRoomFollower> dbFollow, IFitnessRepositry<ChatRoom> dbRoom, IFitnessRepositry<ChatRoomMember> dbMember, IFitnessRepositry<ChatRoomMsg> dbMsg ,
            IFitnessRepositry<UserGift> dbGift)
        {
            this.userManager = userManager;
            this.dbWallet = dbWallet;
            this.dbMicClosed = dbMicClosed;
            this.dbEmosion = dbEmosion;
            this.hostingEnvironment = hostingEnvironment;
            this.dbUserProduct = dbUserProduct;
            this.dbMusic = dbMusic;
            this.dbFollow = dbFollow;
            this.dbRoom = dbRoom;
            this.dbMember = dbMember;
            this.dbMsg = dbMsg;
            this.dbGift = dbGift;
        }
        #region Chat room  >> Add room - DeleteRoom   - Rename Room
        [HttpPost("SendUsingEmosionNotification")]
        public async Task<object> SendUsingEmosionNotification(int ChatRoomId, string SenderId, string SenderName, string ImgUrl, int Id)
        {
            Emosion e = dbEmosion.firstOrdefault(25);
            if (e != null)
            {
                if (e.Price > 0)
                {
                    Wallet w = dbWallet.firstOrdefault(0, "f49e9185-72b0-486e-9945-0848c66d569b");
                    if (w != null)
                    {
                        if (e.Price <= w.Balance)
                        {
                            w.Balance = w.Balance - e.Price;
                            try
                            {
                                if (await dbWallet.update(w))
                                {
                                    var ssss = e.ImgUrl;
                                    return (new { message = "Success" });
                                }
                            }
                            catch (Exception)
                            {
                                return (new { message = "Error" });
                            }

                        }
                    }
                }
                else
                {
                    return (new { message = "Success" });
                }
            }
            return (new { message = "Error : Emosion not found" });
        }

        [HttpPost("AddRoom")]
        public async Task<object> AddRoom(customChatRoomModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.Name))
            {

                string fileName = string.Empty;
               
                List<ChatRoom> checkRooms = dbRoom.list();
                ChatRoom checkRoom = checkRooms.FirstOrDefault(a => a.ApplicationUserId == model.ApplicationUserId);
                if (checkRoom != null)
                {
                    return (new { message = "Error : This user has another room " + checkRoom.Name });
                }
                if (checkRooms.FirstOrDefault(a => a.Name.Equals(model.Name)) != null)
                {
                    return (new { message = "Error : This " + checkRoom.Name + " is exist " });
                }
                ChatRoom room = new ChatRoom();
                room.ApplicationUserId = model.ApplicationUserId;
                room.CreateDate = DateTime.Now;
                room.Desc = model.Desc;
                room.ImgUrl = fileName;
                room.Name = model.Name;
                room.OptionalPassword = model.OptionalPassword;
                room.ChatRoomCategoryId = model.ChatRoomCategoryId;
                try
                {
                    if (await dbRoom.Add(room))
                    {
                        return (new { message = "Success", Id = room.Id });
                    }
                    else
                        return (new { message = "Error : Can't add room" });
                }
                catch (Exception ex)
                {
                    return (new { message = "Error : Exception" });
                }
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        [HttpPost("GetAllRooms")]
        public async Task<List<chatRoomObj>> getAll()
        {
            getAllClass model = new getAllClass()
            {
                rooms = dbRoom.IList(),
                members = dbMember.list()
            };
            List<ChatRoom> roomlist = model.rooms.ToList();

            IEnumerable<ChatRoomMember> _members = dbMember.IList();
            IEnumerable<ChatRoomMember> _roomMembers;
            chatRoomObj _room = new chatRoomObj();
            List<chatRoomObj> _rooms = new List<chatRoomObj>();
            List<ApplicationUser> _users = userManager.Users.ToList();

            foreach (ChatRoom room in roomlist)
            {
                _roomMembers = _members.Where(a => a.ChatRoomId == room.Id);
                var _followers = dbFollow.IList("getrooms", room.Id.ToString());
                List<customRoomFollower> followers = new List<customRoomFollower>();
                customRoomFollower x2021;
                ApplicationUser _user;
                foreach (var item in _followers)
                {
                    if (item.ApplicationUser == null)
                    {
                        x2021 = new customRoomFollower()
                        {
                            ApplicationUserId = item.ApplicationUserId,
                            fulName = item.ApplicationUser.FulName,
                            Img = item.ApplicationUser.ImgUrl,
                            ChatRoomId = item.ChatRoomId,
                            FollowingDate = item.FollowingDate.ToShortDateString()
                        };
                        followers.Add(x2021);
                    }
                    else
                    {
                        _user = _users.FirstOrDefault(a => a.Id == item.ApplicationUserId);
                        x2021 = new customRoomFollower()
                        {
                            ApplicationUserId = item.ApplicationUserId,
                            fulName = _user.FulName,
                            Img = _user.ImgUrl,

                            ChatRoomId = item.ChatRoomId,
                            FollowingDate = item.FollowingDate.ToShortDateString()
                        };
                        followers.Add(x2021);
                    }
                }
                _room = new chatRoomObj()
                {

                    Balancea = room.Balancea,
                    Id = room.Id,
                    Name = room.Name,
                    ImgUrl = room.ImgUrl,
                    ApplicationUserId = room.ApplicationUserId,
                    AdminName = room.ApplicationUser.FulName,
                    AdminImg = room.ApplicationUser.ImgUrl,
                    AdminProducts = new List<ProductViewModel>(),
                    UserId =  room.ApplicationUser.UserId,
                    OptionalPassword = room.OptionalPassword,
                    BackgroundId = room.BackgroundId,
                    ChatRoomCategoryId = room.ChatRoomCategoryId,
                    CreateDate = room.CreateDate,
                    CustomBackground = room.CustomBackground,
                    Members = _roomMembers.Where(a => a.IsExist)
                        .Select((a) => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            fulName = a.ApplicationUser.FulName,
                            Img = a.ApplicationUser.ImgUrl,
                            ChatRoomId = room.Id,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            IsBanned = a.IsBanned,
                            IsBloked = a.IsBloked,
                            BlockDate = a.BlockDate.ToShortDateString(),
                            BlockedTime = a.BlockedTime,
                            BannedDate = a.BannedDate.ToShortDateString()
                        }).ToList(),
                    Supervisors = new List<customRoomMember>() ,
                    UsersBlocked = new List<customRoomMember>() ,
                    UsersBanned = new List<customRoomMember>(),
                    followers = followers,
                    Music =new List<customMusic>() 
                };
                _rooms.Add(_room);

            }
            return _rooms;
        }

        //kareem edit
        [HttpPost]
        [Route("/api/ChatRoom/GetRoom/{id}")]
        public async Task<chatRoomObj> GetRoom([FromRoute] int id)
        {
            int _id = id;
            ChatRoom room = new ChatRoom();
            chatRoomObj _room = new chatRoomObj();
            if (dbRoom.list().Where(c => c.Id == id).ToList().Count > 0)
            {
                room = dbRoom.list().Where(c => c.Id == id).ToList()[0];
               
                //List<ChatRoom> rooms = dbRoom.list();
                List<Music> music = dbMusic.list();
                IEnumerable<ChatRoomMember> _members = dbMember.IList();
                IEnumerable<ChatRoomMember> _roomMembers;
                IEnumerable<UserProduct> _products = dbUserProduct.IList();
                List<MicModel> micsForRom = new List<MicModel>() {
             new MicModel{ MicId=01 },new MicModel{ MicId=02},new MicModel{ MicId=03},new MicModel{ MicId=04},
             new MicModel{ MicId=05 },new MicModel{ MicId=06 },new MicModel{ MicId=07 },
             new MicModel{ MicId=08},new MicModel{ MicId=09} ,new MicModel{ MicId=10}
            };
                List<MicModel> _mics = mics();
                List<MicClosedState> _micClosed = dbMicClosed.list();
                List<ApplicationUser> _users = userManager.Users.ToList();
                List<ProductViewModel> userProducts;
                List<ProductViewModel> exists = new List<ProductViewModel>();
         
                try
                {
                    _roomMembers = _members.Where(a => a.ChatRoomId == room.Id);
                    //followers
                    var _followers = dbFollow.IList("getrooms", room.Id.ToString());
                    List<customRoomFollower> followers = new List<customRoomFollower>();
                    customRoomFollower x2021;
                    ApplicationUser _user;
                    foreach (var item in _followers)
                    {
                        if (item.ApplicationUser == null)
                        {
                            x2021 = new customRoomFollower()
                            {
                                ApplicationUserId = item.ApplicationUserId,
                                fulName = item.ApplicationUser.FulName,
                                Img = item.ApplicationUser.ImgUrl,
                                ChatRoomId = item.ChatRoomId,
                                FollowingDate = item.FollowingDate.ToShortDateString()
                            };
                            followers.Add(x2021);
                        }
                        else
                        {
                            _user = _users.FirstOrDefault(a => a.Id == item.ApplicationUserId);
                            x2021 = new customRoomFollower()
                            {
                                ApplicationUserId = item.ApplicationUserId,
                                fulName = _user.FulName,
                                Img = _user.ImgUrl,

                                ChatRoomId = item.ChatRoomId,
                                FollowingDate = item.FollowingDate.ToShortDateString()
                            };
                            followers.Add(x2021);
                        }
                    }
                    //Followers end-----------------------------------

               
                    userProducts = _products.Where(a => a.ApplicationUserId == room.ApplicationUserId ).Select(x => new ProductViewModel
                    {
                        Id = x.ProductId,
                        CategoryId = x.Product.CategoryId,
                        ImgUrl = x.Product.ImgUrl,
                        ProductName = x.Product.ProductName,
                        date = x.date,
                        DaysCount = x.DaysCount,
                        Price = x.Product.Price,
                        Used = x.Used
                    }).ToList();

                    foreach (var item in userProducts)
                    {
                        if (productValied(item.date, item.DaysCount))
                        {
                            exists.Add(item);
                        }
                        else
                        {
                            await dbUserProduct.Delete(item.Id, null, null, "FromUserDataOrSearch", room.ApplicationUserId);
                        }
                    }


                    _room = new chatRoomObj()
                    {
                        Balancea = room.Balancea,
                        Id = room.Id,
                        Name = room.Name,
                        ImgUrl = room.ImgUrl,
                        Desc = room.Desc ,
                        ApplicationUserId = room.ApplicationUserId,
                        AdminName = _users.FirstOrDefault(a => a.Id == room.ApplicationUserId).FulName,
                        AdminImg = _users.FirstOrDefault(a => a.Id == room.ApplicationUserId).ImgUrl,
                        AdminProducts = exists,
                        UserId = _users.FirstOrDefault(a => a.Id == room.ApplicationUserId).UserId,
                        OptionalPassword = room.OptionalPassword,
                        BackgroundId = room.BackgroundId,
                        ChatRoomCategoryId = room.ChatRoomCategoryId,
                        CreateDate = room.CreateDate,
                        CustomBackground = room.CustomBackground,
                        Members = _roomMembers.Where(a => a.IsExist)
                        .Select((a) => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            fulName = a.ApplicationUser.FulName,
                            Img = a.ApplicationUser.ImgUrl,
                            ChatRoomId = room.Id,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            IsBanned = a.IsBanned,
                            IsBloked = a.IsBloked,
                            BlockDate = a.BlockDate.ToShortDateString(),
                            BlockedTime = a.BlockedTime,
                            BannedDate = a.BannedDate.ToShortDateString()
                        }).ToList(),
                        Supervisors = _roomMembers.Where(a => a.MemberType == 0).Select(a => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            Img = a.ApplicationUser.ImgUrl,
                            fulName = a.ApplicationUser.FulName,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            IsBanned = a.IsBanned,
                            BannedDate = a.BannedDate.ToShortDateString(),
                            IsInvited = a.IsInvited,
                            InviteDate = a.InviteDate.ToShortDateString()
                        }).ToList(),
                        UsersBlocked = _roomMembers.Where(a => a.IsBloked == true).Select(a => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            fulName = a.ApplicationUser.FulName,
                            Img = a.ApplicationUser.ImgUrl,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            BlockDate = a.BlockDate.ToShortDateString(),
                            BlockedTime = a.BlockedTime,
                            IsInvited = a.IsInvited,
                            InviteDate = a.InviteDate.ToShortDateString()
                        }).ToList(),
                        UsersBanned = _roomMembers.Where(a => a.IsBanned == true).Select(a => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            fulName = a.ApplicationUser.FulName,
                            Img = a.ApplicationUser.ImgUrl,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            BannedDate = a.BlockDate.ToShortDateString(),
                            IsInvited = a.IsInvited,
                            InviteDate = a.InviteDate.ToShortDateString()
                        }).ToList(),
                      
                        Music = music.Where(a => a.ChatRoomId == room.Id).Select(x => new customMusic { Id = x.Id, ChatRoomId = x.ChatRoomId, MusicName = x.MusicName }).ToList()
                    };
                    ChatRoomMember x;
                    customRoomMember memberObj;
                    List<ChatRoomMember> chatRoomMembers = dbMember.list();
                    foreach (var mic in _mics)
                    {

                        if (_roomMembers.Where(a => a.MicId == mic.MicId).ToList().Count > 0)
                        {
                            x = _roomMembers.Where(a => a.MicId == mic.MicId).ToList()[0];
                            if (_room.Members.Where(c => c.ApplicationUserId == x.ApplicationUserId).ToList().Count > 0)
                            {
                                memberObj = _room.Members.Where(c => c.ApplicationUserId == x.ApplicationUserId).ToList()[0];
                                mic.ApplicationUserId = x.ApplicationUserId;
                                mic.fulName = memberObj.fulName;
                                mic.Img = memberObj.Img;
                                mic.IsClosed = (_micClosed.Where(c => c.MicId == mic.MicId && c.ChatRoomId == room.Id).ToList().Count > 0);

                                List<UserGift> allgifts = dbGift.IList().Where(c => c.ChatRoomID == id && c.NewUserId == x.ApplicationUserId).ToList();
                                if (allgifts.Count > 0)
                                    mic.balance = allgifts.Sum(c => c.Gift.Price);
                                else
                                    mic.balance = 0;
                            }
              
                            
                                
                            
                        }
                        else
                        {
                            x = new ChatRoomMember();
                            mic.ApplicationUserId = null;
                            mic.fulName = "";
                            mic.Img = "";
                            mic.balance = 0;
                            mic.IsClosed = (_micClosed.Where(c => c.MicId == mic.MicId && c.ChatRoomId == room.Id).ToList().Count > 0);

                        }


                        exists = new List<ProductViewModel>();
                        userProducts = _products.Where(a => a.ApplicationUserId == x.ApplicationUserId).Select(x => new ProductViewModel
                        {
                            Id = x.ProductId,
                            CategoryId = x.Product.CategoryId,
                            ImgUrl = x.Product.ImgUrl,
                            ProductName = x.Product.ProductName,
                            date = x.date,
                            DaysCount = x.DaysCount,
                            Used = x.Used
                        }).ToList();
                        foreach (var item in userProducts)
                        {

                            if (productValied(item.date, item.DaysCount))
                            {
                                item.DaysCount = item.DaysCount - Convert.ToInt16((DateTime.Now - item.date).TotalDays);
                                exists.Add(item);
                            }
                            else
                            {
                                await dbUserProduct.Delete(item.Id, null, null, "FromUserDataOrSearch", x.ApplicationUserId);
                            }
                        }
                        mic.Products = exists;
                      //  mic.IsClosed = true;
                    }
                    _room.Mics = _mics;
                    _room.followers = followers;
                    _mics = mics();
                    exists = new List<ProductViewModel>();
                }
                catch (Exception ex) { return new chatRoomObj(); }
            }
            else
                room = new ChatRoom();

            return _room;
        }

        [HttpPost]
        [Route("/api/ChatRoom/GetRoomGifts/{id}")]
        public async Task<List<UserGift>> GetRoomGifts([FromRoute] int id)
        {
            int _id = id;
            List<UserGift> roomGifts = new List<UserGift>();
            ApplicationUser sender = new ApplicationUser();
            List<ApplicationUser> _users = userManager.Users.ToList();
            List<UserGift> allgifts = dbGift.IList().Where(c=>c.ChatRoomID == id).ToList();
            for(int i = 0; i < allgifts.Count; i++)
            {
                
;                if (_users.Where(c=> c.Id == allgifts[i].ApplicationUserId).ToList().Count > 0)
                {
                    allgifts[i].ApplicationUser = _users.Where(c => c.Id == allgifts[i].ApplicationUserId).ToList()[0];
                }

                    roomGifts.Add(allgifts[i]);

            }

            return roomGifts;
        }

        [Route("api/ChatRoom")]
        [HttpPost("GetRooms")]
        public async Task<List<chatRoomObj>> GetRooms()
        {
            List<chatRoomObj> _rooms = new List<chatRoomObj>();
            chatRoomObj _room;
            List<ChatRoom> rooms = dbRoom.list();
            List<Music> music = dbMusic.list();
            IEnumerable<ChatRoomMember> _members = dbMember.IList();
            IEnumerable<ChatRoomMember> _roomMembers;
            IEnumerable<UserProduct> _products = dbUserProduct.IList();
            List<MicModel> micsForRom = new List<MicModel>() {
             new MicModel{ MicId=01 },new MicModel{ MicId=02},new MicModel{ MicId=03},new MicModel{ MicId=04},
             new MicModel{ MicId=05 },new MicModel{ MicId=06 },new MicModel{ MicId=07 },
             new MicModel{ MicId=08},new MicModel{ MicId=09}
            };
            List<MicModel> _mics = mics();
            List<MicClosedState> _micClosed = dbMicClosed.list();
            List<ApplicationUser> _users = userManager.Users.ToList();
            List<ProductViewModel> userProducts;
            List<ProductViewModel> exists = new List<ProductViewModel>();
            foreach (ChatRoom room in rooms)
            {
                try
                {
                    _roomMembers = _members.Where(a => a.ChatRoomId == room.Id);
                    //
                    var _followers = dbFollow.IList("getrooms", room.Id.ToString());
                    List<customRoomFollower> followers = new List<customRoomFollower>();
                    customRoomFollower x2021;
                    ApplicationUser _user;
                    foreach (var item in _followers)
                    {
                        if (item.ApplicationUser == null)
                        {
                            x2021 = new customRoomFollower()
                            {
                                ApplicationUserId = item.ApplicationUserId,
                                fulName = item.ApplicationUser.FulName,
                                Img = item.ApplicationUser.ImgUrl,
                                ChatRoomId = item.ChatRoomId,
                                FollowingDate = item.FollowingDate.ToShortDateString()
                            };
                            followers.Add(x2021);
                        }
                        else
                        {
                            _user = _users.FirstOrDefault(a => a.Id == item.ApplicationUserId);
                            x2021 = new customRoomFollower()
                            {
                                ApplicationUserId = item.ApplicationUserId,
                                fulName = _user.FulName,
                                Img = _user.ImgUrl,

                                ChatRoomId = item.ChatRoomId,
                                FollowingDate = item.FollowingDate.ToShortDateString()
                            };
                            followers.Add(x2021);
                        }
                    }
                    //
                    userProducts = _products.Where(a => a.ApplicationUserId == room.ApplicationUserId && a.IsDeleted == false).Select(x => new ProductViewModel
                    {
                        Id = x.ProductId,
                        CategoryId = x.Product.CategoryId,
                        ImgUrl = x.Product.ImgUrl,
                        ProductName = x.Product.ProductName,
                        date = x.date,
                        DaysCount = x.DaysCount,
                        Price = x.Product.Price,
                        Used = x.Used
                    }).ToList();

                    foreach (var item in userProducts)
                    {
                        if (productValied(item.date, item.DaysCount))
                        {
                            exists.Add(item);
                        }
                        else
                        {
                            await dbUserProduct.Delete(item.Id, null, null, "FromUserDataOrSearch", room.ApplicationUserId);
                        }
                    }

                    _room = new chatRoomObj()
                    {
                        Balancea = room.Balancea,
                        Id = room.Id,
                        Name = room.Name,
                        ImgUrl = room.ImgUrl,
                        ApplicationUserId = room.ApplicationUserId,
                        AdminName = _users.FirstOrDefault(a => a.Id == room.ApplicationUserId).FulName,
                        AdminImg = _users.FirstOrDefault(a => a.Id == room.ApplicationUserId).ImgUrl,
                        AdminProducts = exists,
                        AdminCountry = _users.FirstOrDefault(a => a.Id == room.ApplicationUserId).CountryId,
                        UserId = _users.FirstOrDefault(a => a.Id == room.ApplicationUserId).UserId,
                        OptionalPassword = room.OptionalPassword,
                        BackgroundId = room.BackgroundId,
                        ChatRoomCategoryId = room.ChatRoomCategoryId,
                        CreateDate = room.CreateDate,
                        CustomBackground = room.CustomBackground,
                        followers = followers,
                        Members = _roomMembers.Where(a => a.IsExist)
                        .Select((a) => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            fulName = a.ApplicationUser.FulName,
                            Img = a.ApplicationUser.ImgUrl,
                            ChatRoomId = room.Id,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            IsBanned = a.IsBanned,
                            IsBloked = a.IsBloked,
                            BlockDate = a.BlockDate.ToShortDateString(),
                            BlockedTime = a.BlockedTime,
                            BannedDate = a.BannedDate.ToShortDateString()
                        }).ToList(),
                        Supervisors = _roomMembers.Where(a => a.MemberType == 0).Select(a => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            Img = a.ApplicationUser.ImgUrl,
                            fulName = a.ApplicationUser.FulName,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            IsBanned = a.IsBanned,
                            BannedDate = a.BannedDate.ToShortDateString(),
                            IsInvited = a.IsInvited,
                            InviteDate = a.InviteDate.ToShortDateString()
                        }).ToList(),
                        UsersBlocked = _roomMembers.Where(a => a.IsBloked == true).Select(a => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            fulName = a.ApplicationUser.FulName,
                            Img = a.ApplicationUser.ImgUrl,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            BlockDate = a.BlockDate.ToShortDateString(),
                            BlockedTime = a.BlockedTime,
                            IsInvited = a.IsInvited,
                            InviteDate = a.InviteDate.ToShortDateString()
                        }).ToList(),
                        UsersBanned = _roomMembers.Where(a => a.IsBanned == true).Select(a => new customRoomMember
                        {
                            ApplicationUserId = a.ApplicationUserId,
                            fulName = a.ApplicationUser.FulName,
                            Img = a.ApplicationUser.ImgUrl,
                            JoinDate = a.JoinDate.ToShortDateString(),
                            BannedDate = a.BlockDate.ToShortDateString(),
                            IsInvited = a.IsInvited,
                            InviteDate = a.InviteDate.ToShortDateString()
                        }).ToList(),
                        Music = music.Where(a => a.ChatRoomId == room.Id).Select(x => new customMusic { Id = x.Id, ApplicationUserId = x.ApplicationUserId, ChatRoomId = x.ChatRoomId, MusicName = x.MusicName }).ToList()
                    };
                    customRoomMember x;
                    foreach (var mic in _mics)
                    {
                        if (_room.Members.Where(a => a.MicId == mic.MicId).Count() > 0)
                        {
                            x = _room.Members.Where(a => a.MicId == mic.MicId).ToList()[0];
                            mic.ApplicationUserId = x.ApplicationUserId;
                            mic.fulName = x.fulName;
                            mic.Img = x.Img;
                            
                            exists = new List<ProductViewModel>();
                            userProducts = _products.Where(a => a.ApplicationUserId == x.ApplicationUserId).Select(x => new ProductViewModel
                            {
                                Id = x.ProductId,
                                CategoryId = x.Product.CategoryId,
                                ImgUrl = x.Product.ImgUrl,
                                ProductName = x.Product.ProductName,
                                date = x.date,
                                DaysCount = x.DaysCount,
                                Used = x.Used
                            }).ToList();
                            foreach (var item in userProducts)
                            {
                                    item.DaysCount = item.DaysCount - Convert.ToInt16((DateTime.Now - item.date).TotalDays);
                                    exists.Add(item);
                            }
                            mic.Products = exists;
                        }
                            mic.IsClosed = false;
                    }
                    _room.Mics = _mics;
                    _rooms.Add(_room);
                    _mics = mics();
                    exists = new List<ProductViewModel>();
                }
                catch (Exception ex) { return new List<chatRoomObj>(); }
            }
            return _rooms;
        }

        public List<MicModel> mics()
        {
            return new List<MicModel>() {
             new MicModel{ MicId=01 },new MicModel{ MicId=02},new MicModel{ MicId=03},new MicModel{ MicId=04},
             new MicModel{ MicId=05 },new MicModel{ MicId=06 },new MicModel{ MicId=07 },
             new MicModel{ MicId=08},new MicModel{ MicId=09} ,new MicModel{ MicId=10}
            };
        }
        [HttpPost("UpdateRoom")]
        public async Task<object> UpdateRoom([FromForm] chatRoomFormType model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.id))
            {
                string fileName = string.Empty;
                try
                {
                    if (model.Image != null)
                    {
                        string path = hostingEnvironment.WebRootPath;
                        path = System.IO.Path.Combine(path, "RoomImgs");
                        path = System.IO.Path.Combine(path, model.ImgUrl);
                        model.Image.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    ChatRoom r = dbRoom.firstOrdefault(int.Parse(model.id));
                    if (r != null && r.ApplicationUserId == model.ApplicationUserId)
                    {
                        r.ImgUrl = model.ImgUrl;
                        r.Name = model.Name;
                        r.OptionalPassword = model.OptionalPassword;
                        r.State = model.State;
                        if (!string.IsNullOrEmpty(model.BackgroundId))
                        {
                            r.BackgroundId = int.Parse(model.BackgroundId);
                        }
                        r.ChatRoomCategoryId = int.Parse(model.ChatRoomCategoryId);
                        r.Desc = model.Desc;
                        if (await dbRoom.update(r))
                        {
                            return (new { message = "Success" });
                        }
                        else
                            return (new { message = "Error : Can't update room" });
                    }
                    else
                        return (new { message = "Error : Room not found" });
                }
                catch (Exception ex)
                {
                    return (new { message = "Error : Please try again" });
                }

            }
            else
                return (new { message = "Error : Data not complete" });
        }
        [HttpPost("ChatRoomState")]
        public async Task<object> ChatRoomState(ChatRoom model)
        {
            if (model.Id > 0 && !string.IsNullOrEmpty(model.State))
            {
                if (await dbRoom.update(model))
                {
                    return (new { message = "Success" });
                }
                else
                    return (new { message = "Error : Can't update room" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        [HttpPost("DeleteRoom")]
        public async Task<object> DeleteRoom(ChatRoom model)
        {
            if (model.Id > 0)
            {
                ChatRoom room = dbRoom.firstOrdefault(model.Id);
                if (room != null)
                {
                    if (model.ApplicationUserId != room.ApplicationUserId)
                    {
                        return (new { message = "Error : This user don't have privilages" });
                    }
                    //
                    List<ChatRoomMember> members = dbMember.list(model.Id.ToString());
                    //
                    List<ChatRoomMsg> msgs = dbMsg.list(model.Id.ToString());
                    if (msgs.Count > 0)
                    {
                        await dbMsg.Delete(0, null, msgs);
                    }
                    if (members.Count > 0)
                    {
                        await dbMember.Delete(0, null, members);
                    }
                    await dbRoom.Delete(0, room, new List<ChatRoom>());
                    return (new { message = "Success" });
                }
                else
                    return (new { message = "Error : Can't delete room" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        #endregion
        #region Chat room member >> Add member and supervisor  -  Delete member and supervisor


        #endregion
        #region 
        [HttpPost("SearchMsg")]
        public ChatRoomMsgViewModel SearchMsg(ChatRoomMsg model)
        {
            if (!string.IsNullOrEmpty(model.Msg))
            {
                ChatRoomMsg msg = dbMsg.firstOrdefault(0, model.Msg);
                if (msg != null)
                {
                    ChatRoomMsgViewModel m = new ChatRoomMsgViewModel()
                    {
                        Msg = msg.Msg,
                        FileName = msg.FileName,
                        FulName = msg.Sender.FulName,
                        ApplicationUserId = msg.SenderId,
                        ChatRoomId = msg.ChatRoomId,
                        SendDate = msg.SendDate,
                        Id = msg.Id,
                        Type = msg.Type
                    };
                    return m;
                }
                else
                    return new ChatRoomMsgViewModel();
            }
            else
                return new ChatRoomMsgViewModel();
        }
        [HttpPost("Messages")]
        public List<ChatRoomMsgViewModel> Messages(ChatRoomMsgViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && model.ChatRoomId > 0)
            {
                ChatRoomMember member = dbMember.firstOrdefault(model.ChatRoomId, model.ApplicationUserId);

                List<ChatRoomMsgViewModel> msgs = dbMsg.IList(model.ApplicationUserId, model.ChatRoomId.ToString()).Where(a => a.SendDate >= member.JoinDate && a.IsRadio == false).Select(a => new ChatRoomMsgViewModel
                {
                    Id = a.Id,
                    ApplicationUserId = a.SenderId,
                    ChatRoomId = a.ChatRoomId,
                    FulName = a.Sender.FulName,
                    ImgUrl = a.Sender.ImgUrl,
                    SendDate = a.SendDate,
                    FileName = a.FileName,
                    Msg = a.Msg,
                    Type = a.Type
                }).ToList();
                return msgs;
            }
            else
                return new List<ChatRoomMsgViewModel>();
        }
        [HttpPost("RadioMessages")]
        public List<ChatRoomMsgViewModel> RadioMessages(ChatRoomMsgViewModel model)
        {
            List<ChatRoomMsgViewModel> msgs = dbMsg.IList().Where(a => a.IsRadio).Select(a => new ChatRoomMsgViewModel
            {
                Id = a.Id,
                ApplicationUserId = a.SenderId,
                ChatRoomId = a.ChatRoomId,
                FulName = a.Sender.FulName,
                ImgUrl = a.Sender.ImgUrl,
                SendDate = a.SendDate,
                FileName = a.FileName,
                Msg = a.Msg
            }).ToList();
            return msgs;
        }

        [HttpPost("UserChatRooms")] // 
        public List<userChatRoom> UserChatRooms(ChatRoomMsgViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId))
            {
                try
                {
                    List<userChatRoom> chating = new List<userChatRoom>();
                    userChatRoom chat = new userChatRoom();
                    ChatRoomMsg lastMsg = new ChatRoomMsg();
                    IEnumerable<ChatRoomMsg> messages = dbMsg.IList();
                    List<ChatRoomMember> UserRooms = dbMember.list(model.ApplicationUserId);
                    List<ChatRoom> rooms = dbRoom.list();
                    ChatRoom room = new ChatRoom();
                    foreach (var userRoom in UserRooms)
                    {
                        lastMsg = messages.LastOrDefault(a => a.ChatRoomId == userRoom.ChatRoomId);
                        room = rooms.FirstOrDefault(a => a.Id == userRoom.ChatRoomId);
                        chat.lastTextOfConversation = lastMsg.Msg;
                        chat.FileName = lastMsg.FileName;
                        chat.SendDate = lastMsg.SendDate;
                        chat.ChatRoomId = userRoom.ChatRoomId;
                        chat.ImgUrl = room.ImgUrl;
                        chat.FulName = room.Name;
                        chat.msgs = messages.Where(x => x.ChatRoomId == userRoom.ChatRoomId)
                            .Select(a => new ChatRoomMsgViewModel
                            {
                                Id = a.Id,
                                ApplicationUserId = a.SenderId,
                                ChatRoomId = a.ChatRoomId,
                                FulName = a.Sender.FulName,
                                ImgUrl = a.Sender.ImgUrl,
                                SendDate = a.SendDate,
                                FileName = a.FileName,
                                Msg = a.Msg
                            }).ToList();
                        chating.Add(chat);
                        chat = new userChatRoom();

                    }
                    return chating;
                }
                catch (Exception)
                {
                    return new List<userChatRoom>();
                }
            }
            else
                return new List<userChatRoom>();
        }

        #endregion
        #region Chat room followers
        [HttpPost("Follow")]
        public async Task<object> Follow(ChatRoomFollower model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && model.ChatRoomId > 0)
            {
                try
                {
                    ChatRoom room = dbRoom.firstOrdefault(model.ChatRoomId);
                    if (room != null)
                    {
                        ChatRoomFollower follow;
                        follow = dbFollow.firstOrdefault(model.ChatRoomId, model.ApplicationUserId);
                        if (follow != null)
                        {
                            return (new { message = "Error: User has followed this room " });
                        }
                        else
                        {
                            follow = new ChatRoomFollower()
                            {
                                FollowingDate = DateTime.Now,
                                ApplicationUserId = model.ApplicationUserId,
                                ChatRoomId = model.ChatRoomId
                            };
                            if (await dbFollow.Add(follow))
                            {
                                return (new { message = "Success" });
                            }
                            else
                            {
                                return (new { message = "Error : can't add " });
                            }
                        }
                    }
                    else
                        return (new { message = "Error: Room not found" });

                }
                catch (Exception c)
                {
                    return (new { message = "Error :Exception" });
                }
            }
            else
                return (new { message = "Error  : Data not complete" });
        }

        [HttpPost("UnFollow")]
        public async Task<object> UnFollow(ChatRoomFollower model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && model.ChatRoomId > 0)
            {
                try
                {
                    ChatRoom room = dbRoom.firstOrdefault(model.ChatRoomId);
                    if (room != null)
                    {
                        ChatRoomFollower follow;
                        follow = dbFollow.firstOrdefault(model.ChatRoomId, model.ApplicationUserId);
                        if (follow != null)
                        {
                          
                            if (await dbFollow.Delete(follow.Id , follow , new List<ChatRoomFollower>()))
                            {
                                return (new { message = "Success" });
                            }
                            else
                            {
                                return (new { message = "Error : can't add " });
                            }
                            //return (new { message = "Error: User has followed this room " });
                        }
                        else
                        {
                            return (new { message = "Error: User has not followed this room " });
                        }
                    }
                    else
                        return (new { message = "Error: Room not found" });

                }
                catch (Exception c)
                {
                    return (new { message = "Error :Exception" + c.Message });
                }
            }
            else
                return (new { message = "Error  : Data not complete" });
        }
        [HttpPost("GetFollowers")]
        public List<ChatRoomsFollowing> GetChatRoomsFollowing(ChatRoomFollower model)
        {
            return dbFollow.IList(model.ApplicationUserId).Select(a => new ChatRoomsFollowing { Id = a.ChatRoomId, Name = a.ChatRoom.Name, ImgUrl = a.ChatRoom.ImgUrl }).ToList();
        }
        #endregion
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
    }
    public class userChatRoom
    {
        public int ChatRoomId { get; set; }
        public string FulName { get; set; }
        public string ImgUrl { get; set; }
        public string lastTextOfConversation { get; set; }
        public DateTime SendDate { get; set; }
        public string FileName { get; set; }
        public List<ChatRoomMsgViewModel> msgs { get; set; }

    }
    public class ChatRoomsFollowing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
    public class customChatRoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string ApplicationUserId { get; set; }//
        public string Desc { get; set; }
        public int ChatRoomCategoryId { get; set; }
        public string OptionalPassword { get; set; }
    }
    public class chatRoomObj
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public string ApplicationUserId { get; set; }//
        public string AdminName { get; set; }//
        public string AdminImg { get; set; }//
        public int AdminCountry { get; set; }//
        public int UserId { get; set; } // 
        public string Desc { get; set; }
        public int ChatRoomCategoryId { get; set; }
        public string OptionalPassword { get; set; }
        public List<customRoomMember> Members { get; set; }
        public List<customRoomMember> Supervisors { get; set; }
        public int BackgroundId { get; set; }
        public string CustomBackground { get; set; }//
        public List<customRoomFollower> followers { get; set; }
        public List<customRoomMember> UsersBanned { get; set; }
        public List<customRoomMember> UsersBlocked { get; set; }
        public List<MicModel> Mics { get; set; }
        public string MusicName { get; set; }
        public List<customMusic> Music { get; set; }
        public List<ProductViewModel> AdminProducts { get; set; }
        public decimal Balancea { get; set; }//
    }
    public class customRoomMember
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string ApplicationUserId { get; set; }
        public string fulName { get; set; }
        public string Img { get; set; }
        public string JoinDate { get; set; }
        public bool IsBloked { get; set; }
        public string BlockDate { get; set; }
        public int BlockedTime { get; set; }// 
        public bool IsBanned { get; set; }// 
        public string BannedDate { get; set; }
        public bool IsInvited { get; set; }
        public string InviteDate { get; set; }
        public int MicId { get; set; }
    }
    public class customRoomFollower
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string ApplicationUserId { get; set; }
        public string fulName { get; set; }
        public string Img { get; set; }
        public string FollowingDate { get; set; }
    }
    public class chatRoomFormType
    {
        public string Name { get; set; }
        public string ApplicationUserId { get; set; }
        public string Desc { get; set; }
        public string OptionalPassword { get; set; }
        public string id { get; set; }
        public IFormFile Image { get; set; }
        public string ImgUrl { get; set; }
        public string ChatRoomCategoryId { get; set; }
        public string State { get; set; }
        public string BackgroundId { get; set; }
    }
    public class customMusic
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string MusicName { get; set; }
        public string ApplicationUserId { get; set; }
    }
    public class MicModel
    {
        public int ChatRoomId { get; set; }
        public string ApplicationUserId { get; set; }
        public int MicId { get; set; }
        public string fulName { get; set; }
        public string Img { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public bool IsClosed { get; set; }
        public decimal balance { get; set; }
    }
}