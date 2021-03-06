using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using FolaChat.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Chat.Models.ViewModel;

namespace Fitness.Controllers
{
    [Route("api/Message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<ConnectionIdTbl> db;
        private readonly IFitnessRepositry<User2UserMsg> dbMsg;

        public MessageController(UserManager<ApplicationUser> userManger, IHostingEnvironment hostingEnvironment, IFitnessRepositry<ConnectionIdTbl> db, IFitnessRepositry<User2UserMsg> dbMsg, IFitnessRepositry<ApplicationUser> dbusers)
        {
            this.userManger = userManger;
            this.hostingEnvironment = hostingEnvironment;
            this.db = db;
            this.dbMsg = dbMsg;
            Dbusers = dbusers;
        }
        public IFitnessRepositry<ApplicationUser> Dbusers { get; }

        [HttpPost("AddConnectionId")]
        public object AddConnectionId(ConnectionIdTbl model)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Redirect("/Identity/Account/Login");
            //}
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.ConnectionId))
            {
                try
                {
                    db.update(model);
                    return (new { message = "Success" });
                }
                catch (Exception)
                {
                    return (new { message = "Error : ConnectionId not add" });
                }
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        [HttpPost("Messages")]
        public List<User2UserMsgViewModel> Messages(User2UserMsg model)
        {
            if (!string.IsNullOrEmpty(model.SenderId) && !string.IsNullOrEmpty(model.RecieverId))
            {
                try
                {
                    List<User2UserMsgViewModel> messages = dbMsg.IList(model.SenderId.ToString(), model.RecieverId.ToString()).Select(
                       a => new User2UserMsgViewModel { Id = a.Id, FileName = a.FileName, Msg = a.Msg, SendDate = a.SendDate, RecieverId = a.RecieverId, FulName = a.Reciever.FulName, ImgUrl = a.Reciever.ImgUrl, SenderId = a.SenderId, SenderFulName = a.Sender.FulName, SenderImgUrl = a.Sender.ImgUrl }).ToList();
                    return messages;
                }
                catch (Exception)
                {
                    return new List<User2UserMsgViewModel>();
                }
            }
            else if (!string.IsNullOrEmpty(model.SenderId) && string.IsNullOrEmpty(model.RecieverId))
            {  
                try
                {
                    List<ApplicationUser> adminstartionMsgUsers = userManger.GetUsersInRoleAsync("AdminstrationChat").Result.ToList();
                    List<User2UserMsgViewModel> messages = dbMsg.IList().Where(a => (a.SenderId == model.SenderId && a.RecieverId == model.SenderId) || (adminstartionMsgUsers.Where(x => x.Id == a.SenderId).Count() > 0 && a.RecieverId == model.SenderId))
                        .Select(a => new User2UserMsgViewModel { Id = a.Id, FileName = a.FileName, Msg = a.Msg, SendDate = a.SendDate, RecieverId = a.RecieverId, FulName = a.Reciever.FulName, ImgUrl = a.Reciever.ImgUrl, SenderId = a.SenderId, SenderFulName = a.Sender.FulName, SenderImgUrl = a.Sender.ImgUrl }).ToList();
                    return messages;
                }
                catch (Exception)
                {
                    return new List<User2UserMsgViewModel>();
                }
            }
            else
                return new List<User2UserMsgViewModel>();
        }
        [HttpPost("UserChat")]  
        public List<userChat> UserChat(UserInfoModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId))
            {
                try
                {
                    List<ApplicationUser> adminstartionMsgUsers = userManger.GetUsersInRoleAsync("AdminstrationChat").Result.ToList();
                    List<User2UserMsg> messages = dbMsg.list(model.ApplicationUserId);
                    List<userIds> otherUsersIds = new List<userIds>();
                    otherUsersIds.AddRange(messages.Where(a => a.SenderId != model.ApplicationUserId).Select((x) => new userIds { UserId = x.SenderId }).ToList());
                    otherUsersIds.AddRange(messages.Where(a => a.RecieverId != model.ApplicationUserId).Select((x) => new userIds { UserId = x.RecieverId }).ToList());
                    List<userIds> chatingUsers = new List<userIds>();
                    foreach (var item in otherUsersIds)
                    {
                        if (chatingUsers.Where(a => a.UserId == item.UserId).Count() == 0)
                        {
                            chatingUsers.Add(item);
                        }
                    }
                    List<ApplicationUser> users = Dbusers.list();
                    ApplicationUser applicationUser = users.FirstOrDefault(a => a.Id == model.ApplicationUserId);
                    if (applicationUser == null)
                    {
                        return new List<userChat>();
                    }
                    ApplicationUser otherApplicationUser = new ApplicationUser();
                    List<userChat> chating = new List<userChat>();
                    userChat chat = new userChat();
                    User2UserMsg lastMsg = new User2UserMsg();
                    foreach (var user in chatingUsers)
                    {
                        if (adminstartionMsgUsers.FirstOrDefault(a => a.Id == user.UserId) == null)
                        {
                            lastMsg = messages.LastOrDefault(a => a.SenderId == user.UserId || a.RecieverId == user.UserId);
                            otherApplicationUser = users.FirstOrDefault(a => a.Id == user.UserId);
                            chat.lastTextOfConversation = lastMsg.Msg;
                            chat.FileName = lastMsg.FileName;
                            chat.SendDate = lastMsg.SendDate;
                            chat.applicationUserId = user.UserId;
                            chat.Img = otherApplicationUser.ImgUrl;
                            chat.FullName = otherApplicationUser.FulName;
                            chat.FromAdminstration = false;
                            chat.msgs = messages.Where(z => z.SenderId == user.UserId || z.RecieverId == user.UserId)
                                .Select(a => new User2UserMsgViewModel { Id = a.Id, FileName = a.FileName, Msg = a.Msg, SendDate = a.SendDate, RecieverId = a.RecieverId, SenderId = a.SenderId }).ToList();
                            chating.Add(chat);
                            chat = new userChat();
                        }
                    }
                    messages = messages.Where(a => a.IsAdminstationMsg && (a.SenderId == model.ApplicationUserId || a.RecieverId == model.ApplicationUserId)).ToList();
                    if (messages.Count > 0)
                    {
                        chat = new userChat();
                        lastMsg = messages.LastOrDefault();
                        chat.lastTextOfConversation = lastMsg.Msg;
                        chat.FileName = lastMsg.FileName;
                        chat.SendDate = lastMsg.SendDate;
                        chat.Img = otherApplicationUser.ImgUrl;
                        chat.FullName = "الاداره";
                        chat.FromAdminstration = true;
                        chat.msgs = messages.Select(a => new User2UserMsgViewModel { Id = a.Id, FileName = a.FileName, Msg = a.Msg, SendDate = a.SendDate, RecieverId = a.RecieverId, SenderId = a.SenderId }).ToList();
                        chating.Add(chat);
                    }
                    return chating;
                }
                catch (Exception sx)
                {
                    return new List<userChat>();
                }
            }
            else
                return new List<userChat>();
        }

        [HttpPost("UserSeenMsg")]
        public async Task<object> UserSeenMsg(User2UserMsg model)
        {
            if (!string.IsNullOrEmpty(model.RecieverId) && model.Id > 0)
            {
                try
                {
                    User2UserMsg _msg = dbMsg.firstOrdefault(model.Id);
                    if (_msg != null)
                    {
                        _msg.IsSeen = true;
                        _msg.SeenDate = DateTime.Now;
                        await dbMsg.update(_msg);
                        return ("message : Success ");
                    }
                    return ("message : error : msg  not found");
                }
                catch (Exception sx)
                {
                    return ("message : error : Ex" + sx.Message);
                }
            }
            else
                return ("message : error : data not compete");
        }

        [HttpPost("SendMsg")]
        public object SendMsg([FromForm] UploadFileviewModel file)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            try
            {
                if (file.file != null && !string.IsNullOrEmpty(file.fileName))
                {
                    string path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "MsgFile");
                    path = System.IO.Path.Combine(path, file.fileName);
                    file.file.CopyTo(new FileStream(path, FileMode.Create));
                    return (new { message = "Success" });
                }
                else
                {
                    return (new { message = "Error :  data not complete" });
                }
            }
            catch (Exception)
            {
                return (new { message = "error" });
            }
        }
    }
    public class userChat
    {
        public string applicationUserId { get; set; }
        public string FullName { get; set; }
        public string Img { get; set; }
        public string lastTextOfConversation { get; set; }
        public DateTime SendDate { get; set; }
        public string FileName { get; set; }
        public bool FromAdminstration { get; set; }
        public List<User2UserMsgViewModel> msgs { get; set; }       
    }
    public class userIds
    {
        public string UserId { get; set; }
    }
}