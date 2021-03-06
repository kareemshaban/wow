using Chat.Models.ViewModel;
using Fitness.Areas.Identity.Data;
using Fitness.Controllers;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using MassTransit.Monitoring.Performance;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FolaChat.Hubs
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public static ChatHub Instance { get; private set; }
        public const decimal rate = .9M;
        private readonly IFitnessRepositry<MicClosedState> dbMicClosed;
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IFitnessRepositry<Lucky> dbLucky;
        private readonly IFitnessRepositry<SiteSetting> dbSetting;
        private readonly IFitnessRepositry<Notification> notificationDb;
        private readonly IFitnessRepositry<Category> categorydb;
        private readonly IFitnessRepositry<Gift> dbGift;
        private readonly IFitnessRepositry<UserProduct> dbUserProduct;
        private readonly IFitnessRepositry<Level> leveldb;
        private readonly IFitnessRepositry<Comment> commentdb;
        private readonly IFitnessRepositry<Post> postdb;
        private readonly IFitnessRepositry<Like> likedb;
        private readonly IFitnessRepositry<ChatRoom> dbRoom;
        private readonly IFitnessRepositry<OtherLevel> otherdb;
        private readonly IFitnessRepositry<UserGift> db;
        private readonly IFitnessRepositry<ApplicationUser> userdb;
        private readonly IFitnessRepositry<ChatRoomMember> dbMember;
        private readonly IFitnessRepositry<Rollet> rolletDb;
        private readonly IFitnessRepositry<RolletMember> rolletMemberDb;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<Wallet> dbWallet;
        private readonly IFitnessRepositry<Emosion> dbEmosion;
        private readonly IFitnessRepositry<ChatRoomMember> dbRoomMemeber;
        private readonly IFitnessRepositry<ChatRoomMsg> dbRoomMsg;
        private readonly IFitnessRepositry<User2UserMsg> dbMsg;
        private readonly IFitnessRepositry<ConnectionIdTbl> dbConnection;

        public ChatHub(UserManager<ApplicationUser> userManger, IFitnessRepositry<Lucky> dbLucky, IFitnessRepositry<MicClosedState> dbMicClosed, IFitnessRepositry<SiteSetting> dbSetting, IFitnessRepositry<Notification> notificationDb, IFitnessRepositry<Category> categorydb, IFitnessRepositry<Gift> dbGift, IFitnessRepositry<UserProduct> dbUserProduct, IFitnessRepositry<Level> Leveldb, IFitnessRepositry<Comment> commentdb, IFitnessRepositry<Post> postdb, IFitnessRepositry<Like> likedb, IFitnessRepositry<ChatRoom> dbRoom, IFitnessRepositry<OtherLevel> Otherdb, IFitnessRepositry<UserGift> db, IFitnessRepositry<ApplicationUser> Userdb, IFitnessRepositry<ChatRoomMember> dbMember, IFitnessRepositry<Rollet> rolletDb, IFitnessRepositry<RolletMember> rolletMemberDb, IHostingEnvironment hostingEnvironment, IFitnessRepositry<Wallet> dbWallet, IFitnessRepositry<Emosion> dbEmosion, IFitnessRepositry<ChatRoomMember> dbRoomMemeber, IFitnessRepositry<ChatRoomMsg> dbRoomMsg, IFitnessRepositry<User2UserMsg> dbMsg, IFitnessRepositry<ConnectionIdTbl> dbConnection)
        {
            this.dbMicClosed = dbMicClosed;
            this.userManger = userManger;
            this.dbLucky = dbLucky;
            this.dbSetting = dbSetting;
            this.notificationDb = notificationDb;
            this.categorydb = categorydb;
            this.dbGift = dbGift;
            this.dbUserProduct = dbUserProduct;
            leveldb = Leveldb;
            this.commentdb = commentdb;
            this.postdb = postdb;
            this.likedb = likedb;
            this.dbRoom = dbRoom;
            otherdb = Otherdb;
            this.db = db;
            userdb = Userdb;
            this.dbMember = dbMember;
            this.rolletDb = rolletDb;
            this.rolletMemberDb = rolletMemberDb;
            this.hostingEnvironment = hostingEnvironment;
            this.dbWallet = dbWallet;
            this.dbEmosion = dbEmosion;
            this.dbRoomMemeber = dbRoomMemeber;
            this.dbRoomMsg = dbRoomMsg;
            this.dbMsg = dbMsg;
            this.dbConnection = dbConnection;
            Instance = this;
        }
        public async Task<object> GetConnectionId(string userName)
        {
            ApplicationUser _user = userManger.FindByNameAsync(userName).Result;
            ConnectionIdTbl alreadyConnections = new ConnectionIdTbl();
            try
            {
                alreadyConnections = dbConnection.firstOrdefault(0, _user.Id);
                if (alreadyConnections != null)
                {
                    alreadyConnections.ConnectionId = Context.ConnectionId;
                    await dbConnection.update(alreadyConnections);
                }
                else
                {
                    alreadyConnections = new ConnectionIdTbl();
                    alreadyConnections.ConnectionId = Context.ConnectionId;
                    alreadyConnections.ApplicationUserId = _user.Id;
                    await dbConnection.Add(alreadyConnections);
                }
                return Context.ConnectionId;
            }
            catch (Exception x)
            {
                return "ex : " + x.InnerException.Message;
            }

        }
        #region 
        public async Task<object> SendMsgByAdminstration(string message, string userId, string adminstationUserId)
        {
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(userId))
            {
                ApplicationUser sender = userManger.Users.Where(a => a.UserName == adminstationUserId).ToList()[0];
                User2UserMsg n = new User2UserMsg();
                n.SenderId = sender.Id;
                n.Msg = message;
                n.SendDate = DateTime.Now;
                n.IsSeen = false;
                n.RecieverId = userId;
                n.IsAdminstationMsg = true;
                if (await dbMsg.Add(n))
                {
                    await Clients.All.SendAsync("ReceiveAdminstrationChat", message, userId , adminstationUserId);
                    return (new { message = "Done", date = DateTime.Now.ToShortDateString() });
                }

            }
            return (new { msg = "Error" });
        }
        public async Task<object> SendMsgNewFollow(string message, string userId, string adminstationUserId , string followerId)
        {
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(userId))
            {
                ApplicationUser sender = userManger.Users.Where(a => a.UserName == adminstationUserId).ToList()[0];
                User2UserMsg n = new User2UserMsg();
                n.SenderId = sender.Id;
                n.Msg = message;
                n.SendDate = DateTime.Now;
                n.IsSeen = false;
                n.RecieverId = userId;
                n.IsAdminstationMsg = false;
                n.FileName = followerId;
                if (await dbMsg.Add(n))
                {
                    await Clients.All.SendAsync("ReceiveNewFollowerMsg", message, userId);
                    return (new { message = "Done", date = DateTime.Now.ToShortDateString() });
                }

            }
            return (new { msg = "Error" });
        }
        public async Task<object> SendMsgAdminstration(string SenderId, string Msg, string fileName, IFormFile file, string SenderName)
        {  // 
            if (file != null)
            {
                string path = hostingEnvironment.WebRootPath;
                path = System.IO.Path.Combine(path, "MsgFile");
                path = System.IO.Path.Combine(path, fileName);
                file.CopyTo(new FileStream(path, FileMode.Create));
            }
            User2UserMsg msg = new User2UserMsg();
            if (!string.IsNullOrEmpty(fileName))
            {
                msg.Msg = fileName;
            }
            else
                msg.Msg = Msg;
            msg.IsSeen = false;
            msg.RecieverId = SenderId;
            msg.SeenDate = null;
            msg.SendDate = DateTime.Now;
            msg.SenderId = SenderId;
            msg.IsAdminstationMsg = true;
            if (await dbMsg.Add(msg))
            {

                await Clients.All.SendAsync("MsgFromUser", SenderName, Msg, SenderId, DateTime.Now.ToShortDateString());
                return (new { message = "Success" });

            }
            else
                return (new { message = "NotAdd" });
        }
        #endregion
        public async Task<object> SendMsgNotification(string SenderName, string Msg, string SenderId, string RecieverId, string fileName, string SenderImg, string RecieverImg)
        {
            try
            {
                if (string.IsNullOrEmpty(RecieverId) || string.IsNullOrEmpty(SenderId))
                {
                    return (new { message = "Data not complete" });
                }
                User2UserMsg msg = new User2UserMsg();
                msg.FileName = fileName;
                msg.Msg = Msg;
                msg.IsSeen = false;
                msg.RecieverId = RecieverId;
                msg.SeenDate = null;
                msg.SendDate = DateTime.Now;
                msg.SenderId = SenderId;
                if (await dbMsg.Add(msg))
                {
                    await Clients.All.SendAsync("ReceiveMessage", SenderName, Msg, SenderId, RecieverId, fileName, SenderImg, RecieverImg);
                    return (new { message = "Success", SenderName, Msg, SenderId, RecieverId, fileName });
                }
                else
                    return (new { message = "NotAdd" });
            }
            catch (Exception)
            {
                return (new { message = "error" });
            }
        }

        #region ChatRoom
        public async Task<object> SendMsgChatRoom(int ChatRoomId, string SenderId, string Msg, IFormFile file, string fileName, string SenderName, int Type, string senderimg, string levelname, string levelimg)
        {
            if (file != null)
            {
                string path = hostingEnvironment.WebRootPath;
                path = System.IO.Path.Combine(path, "MsgFile");
                path = System.IO.Path.Combine(path, fileName);
                file.CopyTo(new FileStream(path, FileMode.Create));
            }
            ChatRoomMsg msg = new ChatRoomMsg();
            msg.FileName = fileName;
            if (!string.IsNullOrEmpty(fileName))
            {
                msg.Msg = fileName;
            }
            else
                msg.Msg = Msg;
            msg.SendDate = DateTime.Now;
            msg.SenderId = SenderId;
            msg.ChatRoomId = ChatRoomId;
            msg.Type = Type;
            if (await dbRoomMsg.Add(msg))
            {
                await Clients.All.SendAsync("ReceiveMsgChatRoom", SenderName, Msg, ChatRoomId, Type, senderimg, levelname, levelimg, SenderId);
                return (new { message = "Success" });
            }
            else
                return (new { message = "NotAdd" });
        }
        public async Task<object> luckyCse(int type , string ApplicationUserId , string userName , string userImg , decimal value , 
            string roomName , int roomId )
        {
            int id = 0;
            Wallet wallet = new Wallet();
            //create luckycase 
            wallet = dbWallet.firstOrdefault(0, ApplicationUserId);
            if(wallet.Balance >= value)
            {
                Lucky lucky = new Lucky()
                {
                    ChatRoomId = roomId,
                    ApplicationUserId = ApplicationUserId,
                    Create_at = DateTime.Now,
                    Type = type,
                    Value = value,
                    ActualValue = value
                };
                var res = await dbLucky.Add(lucky);
                if (res)
                {
                    wallet.Balance -= value;
                    await dbWallet.update(wallet);
                    await Clients.All.SendAsync("ReceiveluckyCse", id, ApplicationUserId, userImg, roomId, roomName, userName, roomId, value, type);
                    return (new { message = "Success" });
                }
                else
                {
                    return (new { message = "Error  : Exception" });
                }
            }
            else
            {
                return (new { message = "Error  : Exception" });
            }




        }

        public async Task<object> Deletelucky(int chatRoomId)
        {
            List<Lucky> roomLucky = new List<Lucky>();
            bool exc = false;
            roomLucky =  dbLucky.list().Where(c => c.ChatRoomId == chatRoomId).ToList();
            foreach(Lucky lucky in roomLucky)
            {
                bool res = await dbLucky.Delete(0, lucky, new List<Lucky>());
                if (!res)
                    exc = true;
            }
            if (!exc)
            {
                await Clients.All.SendAsync("ReceiveDeletelucky", chatRoomId);
                return (new { message = "Success" });
            } else
            {
                return (new { message = "error" });
            }

          
        }
        public async Task<object> Uselucky( string userId , string userName , string userImg , decimal value , string roomName , int roomId)
        {
            List<Lucky> roomLucky = new List<Lucky>();
            Lucky lucky = new Lucky();
            Wallet wallet = new Wallet();
            wallet = dbWallet.firstOrdefault(0, userId);
            if(wallet != null)
            {
              
                roomLucky = dbLucky.list().Where(c => c.ChatRoomId == roomId).ToList();
                if (roomLucky.Count > 0)
                {
                    lucky = roomLucky[0];
                    lucky.ActualValue += value;
                    await dbLucky.update(lucky);
                    wallet.Balance += value;
                   var res =  await dbWallet.update(wallet);
                    if (res)
                    {
                        await Clients.All.SendAsync("ReceiveUselucky", userId ,  roomId);
                        return (new { message = "Success" });
                    } else
                    {
                        return (new { message = "Error" });
                    }

                }
                else
                {
                    return (new { message = "Error" });
                }
            } 
            else
            {
                return (new { message = "Error" });
            }
         
             
        }



        public async Task<object> SendGiftNotification(string giftName, string roomName, int GiftId, int ChatRoomId, string ApplicationUserId, string NewUserId, string ApplicationUserFulName, string NewUserFulName, string SenderImg, string ReceiverImg, string itemDisplayName,string SoundUrl , 
            int count)
        {
            if (await SendGift(GiftId, ChatRoomId, ApplicationUserId, NewUserId , count) == true)
            {
                await Clients.All.SendAsync("ReceiveGiftNotification", ApplicationUserId, ApplicationUserFulName, NewUserId, NewUserFulName, giftName, ChatRoomId, SenderImg, ReceiverImg, 
                    itemDisplayName, SoundUrl);
                return (new { message = "Success" });
            }
            else
            {
                return (new { message = "Error  : Exception" });
            }
        }

        public async Task<object> SendGiftAllMembers(string giftName, string roomName, int GiftId, int ChatRoomId, string ApplicationUserId, string ApplicationUserFulName, string SenderImg,string itemDisplayName,string SoundUrl)
        {
            try
            {
                List<ChatRoomMember> chatroomMember = dbMember.list(ChatRoomId.ToString());
                foreach (var item in chatroomMember.Where(a => !a.ApplicationUserId.Equals(ApplicationUserId)).ToList())
                {
                    if (item.ApplicationUserId != ApplicationUserId)
                    {
                        if (await SendGift(GiftId, ChatRoomId, ApplicationUserId, item.ApplicationUserId) == false) //     
                        {
                            await SendGift(GiftId, ChatRoomId, ApplicationUserId, item.ApplicationUserId);
                        }
                    }
                }
                await Clients.All.SendAsync("ReceiveGiftAllMembers", ApplicationUserId, ApplicationUserFulName, giftName, ChatRoomId, roomName, SenderImg, itemDisplayName, SoundUrl);
                return (new { message = "Success" });
            }
            catch (Exception)
            {
                return (new { message = "Error" });
            }
        }
        public async Task<object> SendGiftMicUsers(string giftName, string roomName, int GiftId, int ChatRoomId, string ApplicationUserId, string ApplicationUserFulName, string SenderImg,string itemDisplayName,string SoundUrl)
        {
            try
            {
                List<ChatRoomMember> chatroomMember = dbMember.list(ChatRoomId.ToString());
                chatroomMember = chatroomMember.Where(a => a.MicId > 0).ToList();
                foreach (var item in chatroomMember)
                {
                    if (await SendGift(GiftId, ChatRoomId, ApplicationUserId, item.ApplicationUserId) == false) // هيرسل  لكل عضو الهديه
                    {
                        await SendGift(GiftId, ChatRoomId, ApplicationUserId, item.ApplicationUserId);
                    }
                }
                await Clients.All.SendAsync("ReceiveGiftMicUsers", ApplicationUserId, ApplicationUserFulName, giftName, ChatRoomId, roomName, SenderImg, itemDisplayName, SoundUrl);
                return (new { message = "Success" });
            }
            catch (Exception)
            {
                return (new { message = "Error" });
            }
        }

        public async Task<object> SendRadionNotification(int ChatRoomId, string SenderId, string Msg, string fileName, string SenderName)
        {
            ChatRoomMsg msg = new ChatRoomMsg();
            msg.FileName = fileName;
            msg.Msg = Msg;
            msg.SendDate = DateTime.Now;
            msg.SenderId = SenderId;
            msg.ChatRoomId = ChatRoomId;
            msg.IsRadio = true;
            if (await dbRoomMsg.Add(msg))
            {
                ApplicationUser user = userManger.Users.ToList().FirstOrDefault(a => a.Id == SenderId);
                Level _level = leveldb.firstOrdefault(Convert.ToInt32(user.LevelId));
                LevelModelView level = new LevelModelView()
                {
                    GiftSendCount = _level.GiftSendCount,
                    Id = _level.Id,
                    ImgUrl = _level.ImgUrl,
                    LevelName = _level.LevelName,
                    UserId = user.Id,
                    Color = _level.Color
                };
                OtherLevel _OtherLevel = otherdb.firstOrdefault(user.OtherLevelId);
                OtherLevelViewModel otherLevel = new OtherLevelViewModel()
                {
                    GiftRecieverCount = _OtherLevel.GiftRecieverCount,
                    Id = _OtherLevel.Id,
                    ImgUrl = _OtherLevel.ImgUrl,
                    LevelName = _OtherLevel.LevelName,
                    UserId = user.Id,
                    Color = _OtherLevel.Color
                };
                var radios = dbRoomMsg.IList(SenderId).Where(x => x.IsRadio == true).OrderBy(a=>a.Id).Select(a => new
                {
                    msg = a.Msg,
                    SenderName = a.Sender.FulName,
                    SenderId = a.SenderId,
                    Img = a.Sender.ImgUrl,
                    RoomId = a.ChatRoomId
                }).ToList();
                await Clients.All.SendAsync("ReceiveRadioNotification", radios, level, otherLevel);
                return (new { message = "Success" });
            }
            else
                return (new { message = "NotAdd" });
        }
        public async Task<object> RemoveRadioMsgs()
        {
            List<ChatRoomMsg> radios = dbRoomMsg.list().Where(x => x.IsRadio == true).ToList();
            if (await dbRoomMsg.Delete(0, null, radios))
            {
                await Clients.All.SendAsync("ReceiveRemoveRadioMsgs");
                return (new { message = "Success" });
            }
            else
                return (new { message = "Error " });
        }
        public async Task<object> SendUsingEmosionNotification(int ChatRoomId, string SenderId, string SenderName, string ImgUrl, int Id)
        {
            Emosion e = dbEmosion.firstOrdefault(Id);
            if (e != null)
            {
                if (e.Price > 0)
                {
                    Wallet w = dbWallet.firstOrdefault(0, SenderId);
                    if (w != null)
                    {
                        if (e.Price <= w.Balance)
                        {
                            w.Balance = w.Balance - e.Price;
                            try
                            {
                                if (await dbWallet.update(w))
                                {
                                    await Clients.All.SendAsync("ReceiveEmosionNotification", SenderName, ChatRoomId, SenderId, "/EmosionImgs/" + e.ImgUrl);
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
                    await Clients.All.SendAsync("ReceiveEmosionNotification", SenderName, ChatRoomId, SenderId, "/EmosionImgs/" + e.ImgUrl);
                    return (new { message = "Success" });
                }
            }
            return (new { message = "Error : Emosion not found" });
        }

        public async Task<object> DeleteChatRoom(int ChatRoomId, string ApplicationUserId)
        {
            if (ChatRoomId > 0 && !string.IsNullOrEmpty(ApplicationUserId))
            {
                ChatRoom _room = dbRoom.firstOrdefault(ChatRoomId);
                if (_room != null)
                {
                    List<ChatRoomMember> members = dbMember.list(ChatRoomId.ToString());
                    if (members.Count > 0 && _room.ApplicationUserId.Equals(ApplicationUserId))
                    {
                        await dbMember.Delete(0, null, members);
                        await dbRoom.Delete(0, _room, new List<ChatRoom>());
                        await Clients.All.SendAsync("ReceiveDeleteChatRoom", ChatRoomId);
                        return (new { message = "Success" });
                    }
                }
            }
            return (new { message = "NotAdd" });
        }

        #endregion

        #region
        public async Task<object> AddMember(string ApplicationUserId, int ChatRoomId, string FulName)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0)
            {
                List<CategoryViewModelProducts> cats = new List<CategoryViewModelProducts>();
                ChatRoom _room = dbRoom.firstOrdefault(ChatRoomId);
                if (_room == null)
                    return (new { message = "Error : Room  not  found" });

                Rollet _rollet = rolletDb.list().FirstOrDefault(a => a.IsFinished == false && a.ChatRoomId == ChatRoomId);
                RolletViewModel _rolletViewModel = new RolletViewModel();
                if (_rollet != null)
                {
                    _rolletViewModel = new RolletViewModel() { Id = _rollet.Id, MemberCount = _rollet.MemberCount, SubscribtionValue = _rollet.SubscribtionValue };
                    _rolletViewModel.members = rolletMemberDb.IList(_rollet.Id.ToString()).Select(a => new RolletMemberViewModel
                    {
                        ApplicationUserId = a.ApplicationUserId,
                        FulName = a.ApplicationUser.FulName,
                        ImgUrl = a.ApplicationUser.ImgUrl
                    }).ToList();
                    _rolletViewModel.ActualMemberCount = _rolletViewModel.members.Count;
                }
                //0- check  if user  is  exist
                ChatRoomMember member = dbMember.firstOrdefault(ChatRoomId, ApplicationUserId);

                if (member != null)
                {
                    member.IsExist = true;
                    await dbMember.update(member);
                    List<ChatRoomMember> members = dbMember.list(ChatRoomId.ToString()).Where(a => a.IsExist).ToList();
                    cats = await getUserCategory(ApplicationUserId);
                    await Clients.All.SendAsync("ReceiveAddMember", ChatRoomId, FulName, members.Count, _rolletViewModel, cats);
                    return (new { message = "Success", Id = member.Id });
                }
                else

                {
                    ChatRoomMember addUser = new ChatRoomMember() { IsExist = true, ChatRoomId = ChatRoomId, ApplicationUserId = ApplicationUserId, JoinDate = DateTime.Now, MemberType = 1 };
                    if (await dbMember.Add(addUser))
                    {
                        List<ChatRoomMember> members = dbMember.list(ChatRoomId.ToString()).Where(a => a.IsExist).ToList();
                        cats = await getUserCategory(ApplicationUserId);
                        await Clients.All.SendAsync("ReceiveAddMember", ChatRoomId, FulName, members.Count, _rolletViewModel, cats);
                        return (new { message = "Success", Id = addUser.Id });
                    }
                    else
                        return (new { message = "Error : Can' tadd  this  user" });
                }
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> DeleteMember(string ApplicationUserId, int ChatRoomId, string FulName)
        { // 
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0)
            {
                ChatRoom _room = dbRoom.firstOrdefault(ChatRoomId);
                ChatRoomMember x = dbMember.firstOrdefault(ChatRoomId, ApplicationUserId);
                if (x != null)
                {
                    if (x.MemberType == 0) // Admin
                    {
                        x.IsExist = false;
                        await dbMember.update(x);
                        await Clients.All.SendAsync("ReceiveDeleteMember", ChatRoomId, FulName, dbMember.list(ChatRoomId.ToString()).Where(a => a.IsExist).Count());
                        return (new { message = "Success" });
                    }
                    else //     
                    {
                        await dbMember.Delete(0, x, new List<ChatRoomMember>());
                        await Clients.All.SendAsync("ReceiveDeleteMember", ChatRoomId, FulName, dbMember.list(ChatRoomId.ToString()).Where(a => a.IsExist).Count());
                        return (new { message = "Success" });
                    }
                }
                else
                    return (new { message = "Error : This user  not  ound " });
            }
            else
                return (new { message = "Error : Data not complete" });
        }

        public async Task<object> DeleteUserFromMicRooms(string ApplicationUserId, string x, string x2, string x3)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId))
            {
                List<ChatRoomMember> m = dbMember.list(ApplicationUserId);

                if (m.Count > 0)
                {
                    foreach (var item in m)
                    {
                        if (item.MemberType == 0) // Admin
                        {
                            item.IsExist = false;
                            await dbMember.update(item);

                        }
                        else
                        {
                            await dbMember.Delete(0, item, new List<ChatRoomMember>());
                        }
                          

                    }
                }
                await Clients.All.SendAsync("Receivedata", ApplicationUserId, x, x2, x3);
                return (new { message = "Success" });
            }
            return (new { message = "Success" });
        }
        //----------------------------------------------------------------------------------------------
        public async Task<object> AddSupervisor(string ApplicationUserId, int ChatRoomId, string superName, string superImg) /// دا  هيضيف  المشرفين     فى  الغرفه
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0)
            {
                ChatRoomMember member = dbMember.firstOrdefault(ChatRoomId, ApplicationUserId);
                if (member != null)  
                {
                    member.MemberType = 0;
                    member.JoinDate = DateTime.Now;
                    await dbMember.update(member);
                    await Clients.All.SendAsync("ReceiveAddSupervisor", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success" });
                }
                else
                {
                    member = new ChatRoomMember();
                    member.ChatRoomId = ChatRoomId;
                    member.ApplicationUserId = ApplicationUserId;
                    member.MemberType = 0;
                    member.JoinDate = DateTime.Now;
                    await dbMember.Add(member);
                    await Clients.All.SendAsync("ReceiveAddSupervisor", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success" });
                }
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> DeleteSupervisor(string ApplicationUserId, int ChatRoomId, string superName, string superImg) // اشيل  مشرف  من  انه مشرف يرجع لمستخدم عادى
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0)
            {
                ChatRoomMember x = dbMember.firstOrdefault(ChatRoomId, ApplicationUserId);
                if (x != null)
                {
                    if (x.MemberType == 0)//User Found
                    {
                        x.MemberType = 1;
                        await dbMember.update(x);
                        await Clients.All.SendAsync("ReceiveDeleteSupervisor", ChatRoomId, ApplicationUserId, superName, superImg);
                        return (new { message = "Success" });
                    }
                    else
                        await dbMember.Delete(0, x, new List<ChatRoomMember>());
                    return (new { message = "Success" });
                }
                else
                    return (new { message = "Error :  This user  not  found" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }

        public async Task<object> BlockSupervisor(string ApplicationUserId, int ChatRoomId, int BlockedTime, string superName, string superImg)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0)
            {
                ChatRoomMember x = dbMember.firstOrdefault(ChatRoomId, ApplicationUserId);
                if (x != null && x.MemberType == 0)
                {
                    x.IsBloked = true;
                    x.BlockDate = DateTime.Now;
                    x.BlockedTime = BlockedTime;
                    x.MemberType = 1;//رجع  لمستخدم
                    await dbMember.update(x);
                    await Clients.All.SendAsync("ReceiveBlockSupervisor", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success", Id = x.Id });
                }
                else if (x != null && x.MemberType == 1)
                {
                    x.IsBloked = true;
                    x.BlockDate = DateTime.Now;
                    x.BlockedTime = BlockedTime;
                    await dbMember.update(x);
                    await Clients.All.SendAsync("ReceiveBlockSupervisor", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success", Id = x.Id });
                }
                else
                    return (new { message = "Error : Try again" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> UnBlock(string ApplicationUserId, int ChatRoomId, string superName, string superImg)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0)
            {
                ChatRoomMember x = dbMember.firstOrdefault(ChatRoomId, ApplicationUserId);
                if (x != null && x.MemberType == 1)
                {
                    x.IsBloked = false;
                    x.MemberType = 0;//  Admin super
                    await dbMember.update(x);
                    await Clients.All.SendAsync("ReceiveUnBlock", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success", Id = x.Id });
                }
                else if (x != null && x.MemberType == 1)
                {
                    x.IsBloked = false;
                    await dbMember.update(x);
                    await Clients.All.SendAsync("ReceiveUnBlock", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success", Id = x.Id });
                }
                else
                    return (new { message = "Error : Try again" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> Ban(string ApplicationUserId, int ChatRoomId, string superName, string superImg)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0)
            {
                ChatRoomMember x = dbMember.firstOrdefault(ChatRoomId, ApplicationUserId);
                if (x != null && x.MemberType == 0)
                {
                    x.IsBanned = true;
                    x.BannedDate = DateTime.Now;
                    await dbMember.update(x);
                    await Clients.All.SendAsync("ReceiveBan", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success", Id = x.Id });
                }
                else if (x != null && x.MemberType == 1)
                {
                    x.IsBanned = true;
                    x.BannedDate = DateTime.Now;
                    await dbMember.update(x);
                    await Clients.All.SendAsync("ReceiveBan", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success", Id = x.Id });
                }
                else
                    return (new { message = "Error : Try again" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> UnBan(string ApplicationUserId, int ChatRoomId, string superName, string superImg)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0)
            {
                ChatRoomMember x = dbMember.firstOrdefault(ChatRoomId, ApplicationUserId);
                if (x != null && x.MemberType == 0)
                {
                    x.IsBanned = false;
                    await dbMember.update(x);
                    await Clients.All.SendAsync("ReceiveUnBan", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success", Id = x.Id });
                }
                else if (x != null && x.MemberType == 1)
                {
                    x.IsBanned = false;
                    await dbMember.update(x);
                    await Clients.All.SendAsync("ReceiveUnBan", ChatRoomId, ApplicationUserId, superName, superImg);
                    return (new { message = "Success", Id = x.Id });
                }
                else
                    return (new { message = "Error : Try again" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        #endregion
        #region  
        public async Task<object> CreateRolletNotification(int ChatRoomId, string ApplicationUserId, int MemberCount, decimal SubscribtionValue, bool ApplicationUserSubscribe)// ارسال اشعار للكل بعدا انشاء  الرولت
        {
            if (ChatRoomId > 0 && !string.IsNullOrEmpty(ApplicationUserId) && MemberCount > 0 && SubscribtionValue > 0)
            {

                //create rollet
                Rollet rollet = new Rollet()
                {
                    ApplicationUserId = ApplicationUserId,
                    ChatRoomId = ChatRoomId,
                    Create_at = DateTime.Now,
                    SubscribtionValue = SubscribtionValue,
                    MemberCount = MemberCount
                };

                int RolletId = await createRollet(rollet);
                if (RolletId > 0)
                {
                    if (ApplicationUserSubscribe)
                    {
                        RolletMember member = new RolletMember()
                        {
                            ApplicationUserId = ApplicationUserId,
                            create_at = DateTime.Now,
                            RolletId = RolletId
                        };
                        if (await rolletMemberDb.Add(member))
                        {
                            Wallet w = dbWallet.firstOrdefault(0, ApplicationUserId);
                            if(w != null)
                            {
                                w.Balance -= SubscribtionValue;
                                await  dbWallet.update(w);
                            }

                            await Clients.All.SendAsync("ReceiveCreateRolletNotification", rollet.ChatRoomId, 0, rollet.Id, 0, ApplicationUserSubscribe);
                            return (new { message = "Success :Rolled created successfully and Admin Subscripped", Id = rollet.Id, });
                        }
                    } else
                    {
                        await Clients.All.SendAsync("ReceiveCreateRolletNotification", rollet.ChatRoomId, 0, rollet.Id, 0, ApplicationUserSubscribe);
                        return (new { message = "Success :Rolled created successfully", Id = rollet.Id, });
                    }
          
                }
                else
                    return (new { message = "Error : Can't creat rollet" });
            }
            return (new { message = "Error : Data not complete" });

        }


        //public async Task<object> CreateRolletNotification(int ChatRoomId, string ApplicationUserId, int MemberCount, decimal SubscribtionValue, bool ApplicationUserSubscribe)// ارسال اشعار للكل بعدا انشاء  الرولت
        //{
        //    if (ChatRoomId > 0 && !string.IsNullOrEmpty(ApplicationUserId) && MemberCount > 0 && SubscribtionValue > 0)
        //    {
        //        Wallet w = dbWallet.firstOrdefault(0, ApplicationUserId);

        //        if (await dbWallet.update(w))
        //        {
        //            Rollet rollet = new Rollet()
        //            {
        //                ApplicationUserId = ApplicationUserId,
        //                ChatRoomId = ChatRoomId,
        //                Create_at = DateTime.Now,
        //                SubscribtionValue = SubscribtionValue,
        //                MemberCount = MemberCount
        //            };
        //            int _Id = await createRollet(rollet);
        //            if (_Id > 0)
        //            {
        //                RolletMember member = new RolletMember()
        //                {
        //                    ApplicationUserId = rollet.ApplicationUserId,
        //                    create_at = DateTime.Now,
        //                    RolletId = _Id
        //                };
        //                if (await rolletMemberDb.Add(member))
        //                {
        //                    decimal rolletValue = (rollet.SubscribtionValue * rate);
        //                    await Clients.All.SendAsync("ReceiveCreateRolletNotification", ChatRoomId, rolletValue, rollet.Id, member.Id, true);//  
        //                    return (new { message = "Success : Rollet created and user has subscribed", rolletValue = rolletValue, Id = rollet.Id, UserId = member.Id });
        //                }
        //                else
        //                    return (new { message = "Error : Can't add  user to rollet" });
        //            }
        //            else
        //                return (new { message = "Error : Can't update user wallet" });
        //        }
        //        else
        //        {
        //            Rollet rollet = new Rollet()
        //            {
        //                ApplicationUserId = ApplicationUserId,
        //                ChatRoomId = ChatRoomId,
        //                Create_at = DateTime.Now,
        //                SubscribtionValue = SubscribtionValue,
        //                MemberCount = MemberCount
        //            };

        //            if (await createRollet(rollet) > 0)
        //            {
        //                await Clients.All.SendAsync("ReceiveCreateRolletNotification", rollet.ChatRoomId, 0, rollet.Id, 0, false);
        //                return (new { message = "Success :Rolled created successfully", Id = rollet.Id, });
        //            }
        //            else
        //                return (new { message = "Error : Can't creat rollet" });
        //        }
        //    }
        //    return (new { message = "Error : Data not complete" });

        //}
        public async Task<object> AddMemberTolRollet(int RolletId, string ApplicationUserId, string FulName)//       
        {
            if (RolletId > 0 && !string.IsNullOrEmpty(ApplicationUserId))
            {
                Rollet r = rolletDb.firstOrdefault(RolletId);
                if (r.ApplicationUserId == ApplicationUserId && r.ApplicationUserSubscribe)
                {
                    return (new { message = "Error : This user already exists" });
                }
                Wallet w = dbWallet.firstOrdefault(0, ApplicationUserId);
                if (w != null && r != null)
                {
                        if (await dbWallet.update(w))
                        {
                            RolletMember member = new RolletMember()
                            {
                                ApplicationUserId = ApplicationUserId,
                                create_at = DateTime.Now,
                                RolletId = RolletId
                            };
                            if (await rolletMemberDb.Add(member))
                            {
                                await Clients.All.SendAsync("ReceiveAddMemberTolRollet", r.ChatRoomId, RolletId, FulName, ApplicationUserId);
                                return (new { message = "Success : Rollet created and user has subscribed", rolletValue = (r.SubscribtionValue * rate) });
                            }
                            else
                                return (new { message = "Error : Can't add  user to rollet" });
                    }
                    else
                        return (new { message = "Error : User don't have balance" });
                }
                else
                    return (new { message = "Error : User don't have wallet Or Rollet id not correct" });

            }
            return (new { message = "Error : Data not complete" });
        }
        public async Task<object> RolletLoser(int RolletId, string ApplicationUserId, string FulName)//      
        {
            if (RolletId > 0 && !string.IsNullOrEmpty(ApplicationUserId))
            {
                Rollet r = rolletDb.firstOrdefault(RolletId);
                if (r != null)
                {
                    List<RolletMember> member = rolletMemberDb.list(ApplicationUserId);
                        if (await rolletMemberDb.update(member[0]))
                        {
                            await Clients.All.SendAsync("ReceiveRolletLoser", r.ChatRoomId, RolletId, FulName);
                            return (new { message = "Success " });
                        }
                        else

                            return (new { message = "Error : Can't Delete this  user from rollet" });
                }
                else
                    return (new { message = "Error : Rollet not found" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> RolletWinner(int RolletId, string ApplicationUserId, string FulName)//    
        {
            if (RolletId > 0 && !string.IsNullOrEmpty(ApplicationUserId))
            {
                Rollet r = rolletDb.firstOrdefault(RolletId);
                if (r != null)
                {
                    List<RolletMember> members = rolletMemberDb.list();
                    RolletMember winner = members.FirstOrDefault(a => a.ApplicationUserId == ApplicationUserId);
                    if (members.Count > 0 && winner != null)
                    {
                        winner.IsWinner = true;
                        if (await rolletMemberDb.update(winner))
                        {
                            Wallet w = dbWallet.firstOrdefault(0, winner.ApplicationUserId);
                            if (w != null)
                            {
                                if (await dbWallet.update(w))
                                {
                                    await rolletDb.update(r);
                                    await Clients.All.SendAsync("ReceiveRolletWinner", r.ChatRoomId, RolletId, FulName);
                                    return (new { message = "Success " });
                                }
                                else
                                    return (new { message = "Error : Can't update  user wallet" });
                            }
                            else
                                return (new { message = "Error : Can't found user wallet" });
                        }
                        else
                            return (new { message = "Error : Can't update this  user " });
                    }
                    else
                        return (new { message = "Error : This user not found" });
                }
                else
                    return (new { message = "Error : Rollet not found" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> RolletBegin(int RolletId)
        {
                Rollet r = rolletDb.firstOrdefault(RolletId);
                    r.IsBegin = true;
                    await rolletDb.update(r);
                    await Clients.All.SendAsync("ReceiveRolletBegin", r.ChatRoomId, RolletId);
                    return (new { message = "Success " });
        }
        public async Task<object> DeleteRollet(int RolletId)
        {
            if (RolletId > 0)
            {
                Rollet r = rolletDb.firstOrdefault(RolletId);
                if (r != null)
                {
                    r.IsFinished = true;
                    if (await rolletDb.update(r))
                    {
                        List<RolletMember> _members = rolletMemberDb.list().Where(a => a.RolletId == RolletId).ToList();
                        List<Wallet> _wallets = dbWallet.list();
                        Wallet userWallet = new Wallet();


                        if (r.ApplicationUserSubscribe)
                        {
                            userWallet = _wallets.FirstOrDefault(a => a.ApplicationUserId == r.ApplicationUserId);
                                await dbWallet.update(userWallet);
                        }
                        foreach (RolletMember item in _members)
                        {
                                await dbWallet.update(userWallet);
                        }
                        await Clients.All.SendAsync("ReceiveDeleteRollet", r.ChatRoomId, RolletId);
                        return (new { message = "Success " });
                    }
                    else
                        return (new { message = "Error :can't delete rollet" });

                }
                else
                    return (new { message = "Error : Rollet not found" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> GetRolletById(int RolletId, int chatroomId)
        {
            try
            {
                List<RolletViewModel> _rollets = new List<RolletViewModel>();
                if (RolletId > 0)
                {
                    if (chatroomId > 0)
                    {
                        List<Rollet> rollets = rolletDb.list().Where(a => a.ChatRoomId == chatroomId).ToList();
                        if (rollets.Count > 0)
                        {

                            RolletViewModel _rollet = new RolletViewModel();
                            foreach (Rollet r in rollets)
                            {

                                List<RolletMemberViewModel> _members = rolletMemberDb.list().Where(a => a.RolletId == r.Id).Select(x => new RolletMemberViewModel
                                {
                                    ApplicationUserId = x.ApplicationUserId,
                                    FulName = x.ApplicationUser.FulName,
                                    ImgUrl = x.ApplicationUser.ImgUrl
                                }).ToList();
                                _rollet = new RolletViewModel()
                                {
                                    Id = r.Id,
                                    IsBegin = r.IsBegin,
                                    IsFinished = r.IsFinished,
                                    MemberCount = r.MemberCount,
                                    ActualMemberCount = _members.Count,
                                    ChatRoomId = r.ChatRoomId,
                                    members = _members
                                };
                                _rollets.Add(_rollet);
                                _rollet = new RolletViewModel();
                            }
                            await Clients.All.SendAsync("ReceiveGetRolletById", _rollets);
                            return (new { message = "Success " });
                        }
                        else
                            return (new { message = "Error : Chatroom dosn't contains any rollet" });
                    }
                    else
                    {
                        Rollet r = rolletDb.firstOrdefault(RolletId);
                        if (r != null)
                        {
                            List<RolletMember> _members1 = rolletMemberDb.list(RolletId.ToString());
                            List<RolletMemberViewModel> _members = new List<RolletMemberViewModel>();
                            ApplicationUser _user2202 = new ApplicationUser();
                            List<ApplicationUser> _allUsers = userManger.Users.ToList();
                            foreach (var item in _members1)
                            {
                                _user2202 = _allUsers.FirstOrDefault(a => a.Id == item.ApplicationUserId);
                                _members.Add(new RolletMemberViewModel
                                {
                                    ApplicationUserId = item.ApplicationUserId,
                                    FulName = _user2202.FulName,
                                    ImgUrl = _user2202.ImgUrl
                                });
                            }
                            RolletViewModel _rollet = new RolletViewModel()
                            {
                                Id = r.Id,
                                IsBegin = r.IsBegin,
                                IsFinished = r.IsFinished,
                                MemberCount = r.MemberCount,
                                SubscribtionValue = r.SubscribtionValue,
                                ActualMemberCount = _members.Count,
                                ChatRoomId = r.ChatRoomId,
                                members = _members
                            };
                            _rollets.Add(_rollet);
                            await Clients.All.SendAsync("ReceiveGetRolletById", _rollets);
                            return (new { message = "Success " });
                        }
                        else
                            return (new { message = "Error : Rollet not found" });
                    }
                }
                else
                    return (new { message = "Error : Data not complete" });
            }
            catch (Exception x)
            {
                return (new { message = "Error :Ex" + x.InnerException.Message });
            }
        }
        #endregion
        #region    
        public async Task<object> AddUser2Mic(string userImge, int ChatRoomId, string ApplicationUserId, int MicId , bool isInvite = false)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0 && MicId <= 10 && MicId >= 1)
            {
                ChatRoom room = dbRoom.firstOrdefault(ChatRoomId);
                if (room != null)
                {
                    List<ChatRoomMember> members = dbMember.list(ChatRoomId.ToString());
                    if (members.Where(a => a.MicId >= 1 && a.MicId <= 10).Count() < 9) 
                    {
                        if (members.Where(a => a.MicId == MicId).Count() == 0)  //  
                        {
                            ChatRoomMember _member = members.FirstOrDefault(a => a.ApplicationUserId == ApplicationUserId);
                            if (_member != null)
                            {
                                _member.MicId = MicId;
                                await dbMember.update(_member);
                                await Clients.All.SendAsync("ReceiveAddUser2Mic", userImge, ApplicationUserId, MicId, ChatRoomId , isInvite);
                                return (new { message = "Success" });
                            }
                            else
                                return (new { message = "Error :User Not found" });
                        }
                        else
                            return (new { message = "Error : Mic  is  used" });
                    }
                    else
                        return (new { message = "Error : All mics are used" });
                }
                else
                    return (new { message = "Error : Room not found" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        public async Task<object> DeleteFromMic(string fulName, int ChatRoomId, string ApplicationUserId, int MicId, string SuperAdminFulName, string userImge, string SuperAdminId)
        {
            //
            if (!string.IsNullOrEmpty(ApplicationUserId) && ChatRoomId > 0 && MicId <= 10 && MicId >= 1)
            {
                ChatRoom room = dbRoom.firstOrdefault(ChatRoomId);
                if (room != null)
                {
                    List<ChatRoomMember> members = dbMember.list(ChatRoomId.ToString());
                    ChatRoomMember _member = members.FirstOrDefault(a => a.ApplicationUserId == ApplicationUserId);
                    if (members.Count > 0 && _member != null)
                    {
                        if (!string.IsNullOrEmpty(SuperAdminId))
                        {
                            if (room.ApplicationUserId == SuperAdminId)
                            {
                                _member.MicId = 0;
                                await dbMember.update(_member);
                                await Clients.All.SendAsync("ReceiveDeleteFromMic", fulName, userImge, ApplicationUserId, MicId, ChatRoomId, SuperAdminFulName);
                                return (new { message = "Success" });
                            }
                            else
                            {
                                //
                                ChatRoomMember _super = members.FirstOrDefault(a => a.ApplicationUserId == SuperAdminId);
                                if (_super != null)
                                {
                                    if (_super.MemberType == 0 && _member.MemberType == 1)
                                    {
                                        _member.MicId = 0;
                                        await dbMember.update(_member);
                                        await Clients.All.SendAsync("ReceiveDeleteFromMic", fulName, userImge, ApplicationUserId, MicId, ChatRoomId, SuperAdminFulName);
                                        return (new { message = "Success" });
                                    }
                                    else
                                        return (new { message = "Error : Supervisor can delete ordinary user  onley" });
                                }
                                else
                                    return (new { message = "Error : There aren't supervisors  in this  chat room" });
                            }
                        }
                        else
                        {
                            _member.MicId = 0;
                            await dbMember.update(_member);
                            await Clients.All.SendAsync("ReceiveDeleteFromMic", fulName, userImge, ApplicationUserId, MicId, ChatRoomId, "");
                            return (new { message = "Success" });
                        }
                    }
                    else
                        return (new { message = "Error : Room dosn't have  any members" });
                }
                else
                    return (new { message = "Error : Room not found" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }

        public async Task<object> ChangeMicState(int ChatRoomId, int MicId, bool state)
        {
            //هغير حاله المايك
            if (ChatRoomId > 0 && MicId <= 10 && MicId >= 1)
            {
                MicClosedState _MicClosed = dbMicClosed.firstOrdefault(MicId, ChatRoomId.ToString());
                if (_MicClosed != null)
                {
                    if (state == false)
                    {
                        await dbMicClosed.Delete(0, _MicClosed, new List<MicClosedState>());
                        await Clients.All.SendAsync("ReceiveChangeMicState", ChatRoomId, MicId, state);
                        return (new { message = "Success" });
                    }
                    else
                        return (new { message = "Success" });
                }
                else
                {
                    if (state)
                    {
                        MicClosedState _micClosed = new MicClosedState()
                        {
                            ChatRoomId = ChatRoomId,
                            MicId = MicId
                        };
                        await dbMicClosed.Add(_micClosed);
                        await Clients.All.SendAsync("ReceiveChangeMicState", ChatRoomId, MicId, state);
                        return (new { message = "Success" });
                    }
                    else
                        return (new { message = "Error : 2" });
                }
            }
            else
                return (new { message = "Error : Data not complete" });
        }

        #endregion
        #region      
        public async Task playMusic(string fulName, int ChatRoomId, string ApplicationUserId, int audioId, string audioUrl)
        {
            await Clients.All.SendAsync("ReceivePlayMusic", fulName, ChatRoomId, ApplicationUserId, audioId, audioUrl);
        }
        public async Task stopMusic(string fulName, int ChatRoomId, string ApplicationUserId, int audioId, string audioUrl)
        {
            await Clients.All.SendAsync("ReceiveStopMusic", fulName, ChatRoomId, ApplicationUserId, audioId, audioUrl);
        }
        #endregion
        #region 

        public async Task<object> AddComment(string ApplicationUserId, int PostId, string Body, string CommenterName)
        {
            Post post = postdb.firstOrdefault(PostId);
            if (post != null)
            {
                Comment comment = new Comment();
                comment.PostId = PostId;
                comment.ApplicationUserId = ApplicationUserId;
                comment.CommentBody = Body;
                comment.date = DateTime.Now;

                if (await commentdb.Add(comment))
                {
                    await Clients.All.SendAsync("ReceiveAddComment", ApplicationUserId, CommenterName, post.ApplicationUserId, PostId);
                    return (new { message = "Success" });
                }
                else
                    return (new { message = "Error :adding " });
            }
            else
                return (new { message = "Post not found" });
        }

        public async Task<object> AddLike(string ApplicationUserId, int PostId, string LikerName)
        {
            if (likedb.list().Where(a => a.PostId == PostId && a.ApplicationUserId == ApplicationUserId).Count() > 0)
            {
                return (new { message = "Error : User  like  this post befor" });
            }
            Post post = postdb.firstOrdefault(PostId);
            if (post != null)
            {
                Like comment = new Like();
                comment.PostId = PostId;
                comment.ApplicationUserId = ApplicationUserId;
                comment.date = DateTime.Now;
                if (await likedb.Add(comment))
                {
                    await Clients.All.SendAsync("ReceiveAddLike", ApplicationUserId, LikerName, post.ApplicationUserId, PostId);
                    return (new { message = "Success" });
                }
                else
                    return (new { message = "Error :adding " });
            }
            else
                return (new { message = "Post not found" });
        }

        public async Task<object> DisLike(string ApplicationUserId, int PostId, string LikerName)
        {
            Like like = likedb.firstOrdefault(0, ApplicationUserId, PostId.ToString());
            if (like != null)
            {
                if (await likedb.Delete(0, like, null))
                {
                    await Clients.All.SendAsync("ReceiveDisLike", ApplicationUserId, LikerName, PostId);
                    return (new { message = "Success" });
                }
                else
                    return (new { message = "Error : not delete " });
            }
            else
                return (new { message = "Like  not found" });
        }

        #endregion
        #region 
        public async Task<object> AddNotification(string ApplicationUserId, string NewUserId, string Title, string Desc, int type, string username, string userImage, string newusername, string newuserimg)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId) && !string.IsNullOrEmpty(NewUserId) && !string.IsNullOrEmpty(Title))
            {
                Notification notification = new Notification()
                {
                    Create_at = DateTime.Now,
                    ApplicationUserId = ApplicationUserId,
                    NewUserId = NewUserId,
                    Desc = Desc,
                    Title = Title,
                    Type = type
                };
                await notificationDb.Add(notification);
                await Clients.All.SendAsync("ReceiveAddNotification", ApplicationUserId, ApplicationUserId, NewUserId, Title, Desc, type, username, userImage, newusername, newuserimg);
                
                return (new { message = "Success" });
            }
            return (new { message = "Error : Data  not complete" });

        }
        public async Task<object> GetUserNotification(string ApplicationUserId)
        {
            if (!string.IsNullOrEmpty(ApplicationUserId))
            {
                List<NotificationModelView> notifications = notificationDb.list(ApplicationUserId).Select(a => new NotificationModelView { ApplicationUserId = a.ApplicationUserId, Create_at = a.Create_at.ToShortDateString(), Desc = a.Desc, Id = a.Id, NewUserId = a.NewUserId, Title = a.Title, Type = a.Type }).ToList();
                if (notifications.Count > 0)
                {
                    await Clients.All.SendAsync("ReceiveUserNotification", notifications);
                    return (new { message = "Success" });
                }
                else
                    return (new { message = "Success : But  there isn't any notifications", notifications });
            }
            return (new { message = "Error : Data  not complete" });

        }
        #endregion
        #region  
        private async Task<int> createRollet(Rollet rollet)
        {
            await rolletDb.Add(rollet);
            return rollet.Id;
        }


        private async Task<List<CategoryViewModelProducts>> getUserCategory(string ApplicationUserId)
        {
            List<UserProductViewModel> allUserProducts = dbUserProduct.IList(ApplicationUserId).Select(a => new UserProductViewModel
            { Id = a.Id, Used = a.Used, ApplicationUserId = a.ApplicationUserId, ProductId = a.ProductId, ImgUrl = a.Product.ImgUrl, Price = a.Product.Price, ProductName = a.Product.ProductName, DaysCount = a.DaysCount, date = a.date, CategoryId = a.Product.CategoryId }).ToList();
            if (allUserProducts.Count > 0)
            {
                List<UserProductViewModel> userProducts = new List<UserProductViewModel>();
                foreach (UserProductViewModel item in allUserProducts)
                {
                    if (productValied(item.date, item.DaysCount))
                    {
                        item.DaysCount = item.DaysCount - Convert.ToInt16((DateTime.Now - item.date).TotalDays);
                        userProducts.Add(item);
                    }
                    else
                    {
                        await dbUserProduct.Delete(item.ProductId, null, null, "FromUserDataOrSearch", item.ApplicationUserId);
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
                return x1;
            }
            else return new List<CategoryViewModelProducts>();
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

        public async Task<bool> SendGift(int GiftId, int ChatRoomId, string ApplicationUserId, string NewUserId , int count = 1)
        {
            int senderLevelId = 0, senderOtherLevelId = 0, RecieverLevelId = 0, RecievierOtherLevelId = 0;
            ApplicationUser sender = null;
            ApplicationUser reciever = null;
            ApplicationUser roomOwner = null;
            try
            {
                if (GiftId > 0 && !string.IsNullOrEmpty(ApplicationUserId) && !string.IsNullOrEmpty(NewUserId) && ChatRoomId > 0)
                {
                    Wallet _wallet = dbWallet.firstOrdefault(0, ApplicationUserId);
                    SiteSetting _setting = dbSetting.firstOrdefault(0);
                    Gift g = dbGift.firstOrdefault(GiftId);
                    if (_wallet != null && _setting != null)
                    {
                        if (_wallet.Balance >= count * g.Price)
                        {
                            //                        
                            List<UserGift> Gifts = db.IList().ToList();
                            UserGift _checkGiftExist = Gifts.FirstOrDefault(a => a.ApplicationUserId == ApplicationUserId && a.GiftId == GiftId && a.NewUserId == NewUserId);
                            UserGift p = null;
                   
                           
                                for(int i = 0; i < count; i++)
                                {
                                    p = new UserGift()
                                    {
                                        ApplicationUserId = ApplicationUserId,
                                        date = DateTime.Now,
                                        GiftId = GiftId,
                                        NewUserId = NewUserId,
                                        DaysCount = g.DaysCount,
                                        ConvertGift2Diamond = (_setting.ConvertGift2Diamond / 100),
                                        ChatRoomID = ChatRoomId
                                    };
                                    await db.Add(p);

                                }
                          
                            
                         
                            ChatRoom _chatRoom = dbRoom.firstOrdefault(ChatRoomId);
                            _chatRoom.Balancea += (count * g.Price);
                            _chatRoom.LastDate = DateTime.Now;
                            await dbRoom.update(_chatRoom);

                            // room owner walet increase 
                            roomOwner = userdb.firstOrdefault(0, _chatRoom.ApplicationUserId);
                            if(roomOwner != null)
                            {
                                Wallet ownerWallet = dbWallet.firstOrdefault(0, _chatRoom.ApplicationUserId);
                                if(ownerWallet != null)
                                {
                                    ownerWallet.DiamonadBalance += (count * g.Price) * (_setting.ConvertGift2Diamond / 100);
                                    await dbWallet.update(ownerWallet);
                                }
                            }


                                Gifts = db.IList().ToList();
                            List<Level> levels = leveldb.list();
                            List<OtherLevel> otherLevels = otherdb.list();
                            sender = userdb.firstOrdefault(0, p.ApplicationUserId);

                            senderLevelId = int.Parse(sender.LevelId + "");
                            senderOtherLevelId = sender.OtherLevelId;

                            List<UserGift> x = Gifts.Where(a => a.ApplicationUserId == sender.Id).ToList();
                            List<UserGift> y = Gifts.Where(a => a.NewUserId == sender.Id).ToList();
                            decimal giftWhichSendItSender = x.Sum(a => a.Gift.Price); //الهدايا المستقبلة
                            decimal giftWhichRecieveItSender = y.Sum(a => a.Gift.Price);  

                            List<Level> _levels = levels.Where(a => a.GiftSendCount <= giftWhichSendItSender).OrderBy(c=> c.GiftSendCount).ToList();
                            sender.LevelId = _levels.Count > 0 ? (_levels[_levels.Count - 1]).Id : sender.LevelId;

                            List<OtherLevel> _other = otherLevels.Where(a => a.GiftRecieverCount <= giftWhichRecieveItSender).OrderBy(c=> c.GiftRecieverCount).ToList();
                            sender.OtherLevelId = _other.Count > 0 ? (_other[_other.Count - 1]).Id : sender.OtherLevelId;
                            await userdb.update(sender);

                            reciever = userdb.firstOrdefault(0, p.NewUserId);
                            Wallet recieverWallet = dbWallet.firstOrdefault(0, p.NewUserId);
                            if (recieverWallet != null)
                            {
                                recieverWallet.DiamonadBalance += (count* g.Price )* (_setting.ConvertGift2Diamond / 100);
                                if (await dbWallet.update(recieverWallet))
                                {
                                    Gifts = db.IList().ToList();
                                    List<UserGift> xx = Gifts.Where(a => a.ApplicationUserId == reciever.Id).ToList();
                                    List<UserGift> yy = Gifts.Where(a => a.NewUserId == reciever.Id).ToList();
                                    decimal giftWhichSendItRecievier = xx.Sum(a => a.Gift.Price);      
                                    decimal giftWhichRecieveItRecievier = yy.Sum(a => a.Gift.Price); 
                                    List<Level> _levels2 = levels.Where(a => a.GiftSendCount <= giftWhichSendItRecievier).OrderBy(c=> c.GiftSendCount).ToList();
                                    reciever.LevelId = _levels2.Count > 0 ? (_levels2[_levels2.Count - 1]).Id : reciever.LevelId;

                                    List<OtherLevel> _other2 = otherLevels.Where(a => a.GiftRecieverCount <= giftWhichRecieveItRecievier).OrderBy(c => c.GiftRecieverCount).ToList();
                                    reciever.OtherLevelId = _other2.Count > 0 ? (_other2[_other2.Count - 1]).Id : reciever.OtherLevelId;
                                    await userdb.update(reciever);
                                    return true;
                                }
                            }
                            else
                                return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
        public async Task<object> lucky(int lucky,string id,string username,int roomid)
        {
            await Clients.All.SendAsync("Receivelucky", lucky, id, username, roomid);
            return (new { message = "Success" });
        }
        public async Task<object> nurd(int ran, string userid, string username, int roomid)
        {
            await Clients.All.SendAsync("Receivenurd", ran, userid, username, roomid);
            return (new { message = "Success" });
        }

    }
}