using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using FolaChat.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    public class MessengerController : Controller
    {
        private readonly IFitnessRepositry<ConnectionIdTbl> dbConnection;
        private readonly RoleManager<IdentityRole> roles;
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IFitnessRepositry<User2UserMsg> db;

        public MessengerController(IFitnessRepositry<ConnectionIdTbl> dbConnection, RoleManager<IdentityRole> roles, UserManager<ApplicationUser> userManger, IFitnessRepositry<User2UserMsg> db)
        {
            this.dbConnection = dbConnection;
            this.roles = roles;
            this.userManger = userManger;
            this.db = db;
        }
        [Authorize(Roles = "Admin,AdminstrationChat")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            List<ApplicationUser> adminstartionMsgUsers = userManger.GetUsersInRoleAsync("AdminstrationChat").Result.ToList();

            List<User2UserMsg> Msgs = db.IList().ToList();

            List<customMsg> userMsgsWithoutAdminstration = new List<customMsg>();
            List<User2UserMsg> lastMsg = new List<User2UserMsg>();
            foreach (User2UserMsg msg in Msgs)
            {
                if (msg.Msg != null)
                {
                    if (msg.RecieverId == msg.SenderId && userMsgsWithoutAdminstration.FirstOrDefault(a => a.UserId == msg.RecieverId) == null)
                    {
                        lastMsg = Msgs.Where(a => (a.SenderId == msg.RecieverId && (a.RecieverId == msg.RecieverId || adminstartionMsgUsers.Where(x => x.Id == a.RecieverId).Count() > 0)) || (a.RecieverId == msg.RecieverId && (a.SenderId == msg.RecieverId || adminstartionMsgUsers.Where(x => x.Id == a.SenderId).Count() > 0))).ToList();
                        userMsgsWithoutAdminstration.Add(new customMsg
                        {
                            UserId = msg.RecieverId,
                            FullName = msg.Reciever.FulName,
                            LastMsg = lastMsg[lastMsg.Count - 1].Msg,
                            Date = lastMsg[lastMsg.Count - 1].SendDate
                        });
                    }
                     else if (adminstartionMsgUsers.FirstOrDefault(a => a.Id == msg.SenderId) != null && userMsgsWithoutAdminstration.FirstOrDefault(a => a.UserId == msg.RecieverId) == null)
                    {
                        lastMsg = Msgs.Where(a => (a.SenderId == msg.RecieverId && (a.RecieverId == msg.RecieverId || adminstartionMsgUsers.Where(x => x.Id == a.RecieverId).Count() > 0)) || (a.RecieverId == msg.RecieverId && (a.SenderId == msg.RecieverId || adminstartionMsgUsers.Where(x => x.Id == a.SenderId).Count() > 0))).ToList();
                        userMsgsWithoutAdminstration.Add(new customMsg
                        {
                            UserId = msg.Reciever.Id,
                            FullName = msg.Reciever.FulName,
                            LastMsg = lastMsg[lastMsg.Count - 1].Msg,
                            Date = lastMsg[lastMsg.Count - 1].SendDate
                        });
                    }
                    lastMsg = new List<User2UserMsg>();
                }       
            }
            userMsgsWithoutAdminstration = userMsgsWithoutAdminstration.OrderBy(a => a.Date).ToList();
            return View(userMsgsWithoutAdminstration);
        }       
        [Authorize(Roles = "Admin,AdminstrationChat")]
        public ActionResult chating(string userId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (!string.IsNullOrEmpty(userId))
            {
                ViewBag.adminstrationChat = User.Identity.Name;
                ViewBag.userchatId = userId;
                List<ApplicationUser> adminstartionMsgUsers = userManger.GetUsersInRoleAsync("AdminstrationChat").Result.ToList();
                List<User2UserMsg> User2UserMsgs = db.IList().Where(a=>a.IsAdminstationMsg && (a.SenderId == userId || a.RecieverId == userId)).ToList();
            
                        ViewBag.name = userManger.Users.FirstOrDefault(a=>a.Id == userId  ).FulName;
                return View(User2UserMsgs);
            }
            return Redirect("/Messenger/Index");
        }
        [Authorize(Roles = "Admin,ShowUsersChat")]
        [HttpGet]
        public IActionResult ConflictResolution(int user1=0,int user2=0)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (user1 > 0 && user2 > 0)
            {
                List<ApplicationUser> _users = userManger.Users.Where(a => a.UserId == user1 || a.UserId == user2).ToList();
                string _user1 = _users.FirstOrDefault(x => x.UserId == user1).Id;
                string _user2 = _users.FirstOrDefault(x => x.UserId == user2).Id;
                ViewBag.rightBar = _user1;
                List<User2UserMsg> User2UserMsgs = db.IList().Where(a => (a.SenderId ==_user1 && a.RecieverId == _user2) || (a.RecieverId == _user1 && a.SenderId == _user2)).ToList();
                return View(User2UserMsgs);
            }
            else
            {
                return View(new List<User2UserMsg>());
            }
           
        }
      }
    public class customMsg
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string LastMsg { get; set; }
        public DateTime Date { get; set; }
    }
}
