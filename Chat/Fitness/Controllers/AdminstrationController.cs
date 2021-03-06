using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fitness.Controllers
{
    public class AdminstrationController : Controller 
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<User2UserMsg> dbMsg;

        private readonly IFitnessRepositry<Level> levelDb;
        private readonly IFitnessRepositry<ChargingLevel> chargingDb;
        private readonly IFitnessRepositry<Wallet> walletDb;
        private readonly IFitnessRepositry<OtherLevel> otherLevelDb;
        private readonly IFitnessRepositry<ChatRoom> ChatRoomDb;
        private readonly IFitnessRepositry<ChatRoomMember> ChatRoomMemberDb;
        private readonly IFitnessRepositry<ChatRoomMsg> ChatRoomMemberMsgDb;
        private readonly IFitnessRepositry<BlockUser> BlockUserDb;
        private readonly RoleManager<IdentityRole> roleManger;
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IFitnessRepositry<User2UserMsg> msgDb;
        private readonly IFitnessRepositry<FollowUser> followDb;
        public AdminstrationController(IHostingEnvironment hostingEnvironment, IFitnessRepositry<User2UserMsg> dbMsg, IFitnessRepositry<Level> levelDb, IFitnessRepositry<ChargingLevel> chargingDb, IFitnessRepositry<Wallet> walletDb, IFitnessRepositry<OtherLevel> otherLevelDb, RoleManager<IdentityRole> roleManger, UserManager<ApplicationUser> userManger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.dbMsg = dbMsg;
            this.followDb = followDb;
            this.levelDb = levelDb;
            this.chargingDb = chargingDb;
            this.walletDb = walletDb;
            this.otherLevelDb = otherLevelDb;
            this.roleManger = roleManger;
            this.userManger = userManger;
            this.ChatRoomDb = ChatRoomDb;
            this.ChatRoomMemberDb = ChatRoomMemberDb;
            this.ChatRoomMemberMsgDb = ChatRoomMemberMsgDb;
            this.BlockUserDb = BlockUserDb;
        }
         
        public async Task<IActionResult> createUser()
        {
            IdentityRole _role;
            if (!await roleManger.RoleExistsAsync("AddUsers"))
            {
                _role = new IdentityRole() { Name = "AddUsers" };
                await roleManger.CreateAsync(_role);
            }
            if (!await roleManger.RoleExistsAsync("ShowUsersChat"))
            {
                _role = new IdentityRole() { Name = "ShowUsersChat" };
                await roleManger.CreateAsync(_role);
            }
            if (!await roleManger.RoleExistsAsync("AdminstrationChat"))
            {
                _role = new IdentityRole() { Name = "AdminstrationChat" };
                await roleManger.CreateAsync(_role);
            }
            if (!await roleManger.RoleExistsAsync("Admin"))
            {
                _role = new IdentityRole() { Name = "Admin" };
                await roleManger.CreateAsync(_role);
            }
            return View(roleManger.Roles.ToList());
        }
        [HttpPost]
        public async Task<JsonResult> createUser(UserRegister model)
        {
            if (model.ConfirmPassword != model.Password || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.FulName) || string.IsNullOrEmpty(model.roleName) || string.IsNullOrEmpty(model.UserName) || model.Gender == 0)
            {
                return Json(new { message = "error", alertMsg = "البيانات غير  مكتمله" });
            }
            try
            {
                int startId = 90000;
                Level level = levelDb.firstOrdefault(0);
                OtherLevel other = otherLevelDb.firstOrdefault(0);
                ChargingLevel charging = chargingDb.firstOrdefault(0);
                int levelId = 0;
                int otherLevelId = 0;
                int chargingLevelId = 0;
                if (level != null)
                {
                    levelId = level.Id;
                }
                else
                {
                    level = new Level();
                    level.ImgUrl = "";
                    level.LevelName = "Zero level";
                    level.GiftSendCount = 0;
                    await levelDb.Add(level);
                    levelId = level.Id;
                }
                    if (charging != null)
                    {
                        chargingLevelId = charging.Id;
                    }
                    else
                    {
                        charging = new ChargingLevel();
                        charging.ImgUrl = "";
                        charging.LevelName = "Zero level";
                        charging.BalanceCount = 0;
                        await chargingDb.Add(charging);
                        chargingLevelId = charging.Id;
                    }
                    if (other != null)
                {
                    otherLevelId = other.Id;
                }
                else
                {
                    other = new OtherLevel();
                    other.GiftRecieverCount = 0;
                    other.ImgUrl = "";
                    other.LevelName = "Zero level";
                    await otherLevelDb.Add(other);
                    otherLevelId = other.Id;
                }
                var users = userManger.Users.ToList();
                while (users.Where(a => a.UserId == startId).Count() > 0)
                {
                    startId++;
                }
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    ImgUrl = model.ImgUrl,
                    FulName = model.FulName,
                    DateBirth = model.DateBirth,
                    Gender = model.Gender,
                    LevelId = levelId,
                    OtherLevelId = otherLevelId,
                    UserId = startId,
                    RegisterDate = DateTime.Now,
                    ChargingLevelId=chargingLevelId
                };
                IdentityResult result = await userManger.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    if (result.Errors.ToList()[0].Description.Contains("User name"))
                    {
                        return Json(new { message = "Error", alertMsg = "اسم المستخدم موجود من قبل" });
                    }
                    // return BadRequest(result.Errors.ToList()[0]);
                    return Json(new { message = "Error", alertMsg = result.Errors.ToList()[0].Description });
                }
                else
                {
                    Wallet w = new Wallet();
                    w.ApplicationUserId = user.Id;
                    w.Balance = 0;
                    w.LastUpdate = DateTime.Now;
                    w.TotalBalance = 0;
                    if (await walletDb.Add(w))
                    {
                        if (!string.IsNullOrEmpty(model.roleName))
                        {
                            if (await roleManger.RoleExistsAsync(model.roleName))
                            {
                                await userManger.AddToRoleAsync(user, model.roleName);
                            }
                        }
                        return Json(new { message = "success", alertMsg = "تمت اضافه المستخدم بنجاح" });
                    }
                    else
                        return Json(new { message = "error", alertMsg = "يرجى المحاوله مره  اخرى" });
                }
            }
            catch (Exception e)
            {
                return Json(new { message = "Error", alertMsg = "يرجى المحاوله مره اخرى" });
            }
        }
         
        [HttpPost]
        public async Task<JsonResult> AddRole(string roleName, string UserName)
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(roleName))
            {
                ApplicationUser _user = await userManger.FindByNameAsync(UserName);
                if (_user != null)
                {
                    IdentityResult res = await userManger.AddToRoleAsync(_user, roleName);
                    if (res.Succeeded)
                    {
                        return Json(new { msg = "Done" });
                    }
                    else
                    {
                        if (res.Errors.ToList()[0].Code.Contains("Already"))
                        {
                            return Json(new { msg = "Error", alertMsg = "لقد تم اضافه هذه الصلاحيه للمستخدم من قبل" });
                        }
                        return Json(new { msg = "Error", alertMsg = "يرجى المحاوله مره اخرى" });
                    }
                }
            }
            return Json(new { msg = "Error", alertMsg = "يرجى المحاوله مره اخرى" });
        }
        #region All Users and blocked users
         
        [HttpGet]
        public IActionResult users()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            usersCls _user;
            List<usersCls> _users = new List<usersCls>();
            foreach (var item in userManger.Users.Where(a => a.userblocked == false).ToList())
            {
                _user = new usersCls();
                _user.Email = item.Email;
                _user.FullName = item.FulName;
                _user.UserId = item.UserId;
                _user.Id = item.Id;
                _user.UserName = item.UserName;
                _user.UserRoles = userManger.GetRolesAsync(item).Result.ToList();
                _user.imgUrl = item.ImgUrl;
                if (_user.UserRoles.Count ==0 )
                {
                    _users.Add(_user);
                }
            }
            ViewBag.roles = roleManger.Roles.ToList();
            return View(_users);
        }
         
        [HttpGet]
        public IActionResult Adminstrationusers()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            usersCls _user;
            List<usersCls> _users = new List<usersCls>();
            foreach (var item in userManger.Users.Where(a => a.userblocked == false).ToList())
            {
                _user = new usersCls();
                _user.Email = item.Email;
                _user.FullName = item.FulName;
                _user.UserId = item.UserId;
                _user.Id = item.Id;
                _user.UserName = item.UserName;
                _user.UserRoles = userManger.GetRolesAsync(item).Result.ToList();
                if (_user.UserRoles.Count > 0)
                {
                    _users.Add(_user);
                }
            }
            ViewBag.roles = roleManger.Roles.ToList();
            return View(_users);
        }
         
        [HttpGet]
        public IActionResult blocked() => View(userManger.Users.Where(a => a.userblocked == true).ToList());
         
        [HttpPost]
        public async Task<JsonResult> block(string UserName,int days)
        {
            if (!string.IsNullOrEmpty(UserName) &&  days > 0)
            {
                ApplicationUser _user = await userManger.FindByNameAsync(UserName);
                if (_user != null)
                {
                    IEnumerable<string> userRoles = userManger.GetRolesAsync(_user).Result.ToList();
                    await userManger.RemoveFromRolesAsync(_user, userRoles);

                    _user.userblocked = true;
                    _user.days = days;
                    _user.blockedStartDate = DateTime.Now;
                    await userManger.UpdateAsync(_user);
                    return Json(new { msg = "Done" });
                }
            }
            return Json(new { msg = "Error" });
        }
         
        [HttpPost]
        public async Task<JsonResult> Unblock(string UserName)
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                ApplicationUser _user = await userManger.FindByNameAsync(UserName);
                if (_user != null)
                {
                    _user.userblocked = false;
                    await userManger.UpdateAsync(_user);
                    return Json(new { msg = "Done" });
                }
            }
            return Json(new { msg = "Error" });
        }
        

        [HttpGet]
        public ActionResult UpdateUser(string userId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if ( !string.IsNullOrEmpty(userId))
            {
                ApplicationUser c = userManger.Users.FirstOrDefault(a => a.Id == userId);
                if (c != null)
                {
                    Wallet _wallet = walletDb.firstOrdefault(0, userId);
                    ViewBag.walletBalance = _wallet.Balance;
                    ViewBag.levels = levelDb.list();
                    ViewBag.OtherLevel = otherLevelDb.list();
                    return View(c);
                }
            }
            return Redirect("/Adminstration/users");
        }

        [HttpPost]
        public async Task<object> UpdateUser(int level, int OtherLevel, string RoomName, string PhoneNumber, string RegisterDate, string Email, IFormFile imgUploader, string id)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Redirect("/Identity/Account/Login");
            //}
            if (!string.IsNullOrEmpty(id))
            {
                string fileName = string.Empty;
                if (imgUploader != null)
                {
                    fileName = DateTime.Now.Second + imgUploader.FileName;
                    string path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "UsersImage");
                    path = System.IO.Path.Combine(path, fileName);
                    imgUploader.CopyTo(new FileStream(path, FileMode.Create));
                }
                ApplicationUser r = userManger.Users.FirstOrDefault(a => a.Id == id);
                if (r != null)
                {
                    if (!string.IsNullOrEmpty(RoomName))
                        r.FulName = RoomName;

                    if (!string.IsNullOrEmpty(fileName))
                        r.ImgUrl = fileName;

                    r.LevelId = level > 0 ? level : r.LevelId;
                    r.OtherLevelId = OtherLevel > 0 ? OtherLevel : r.OtherLevelId;
                    r.PhoneNumber = !string.IsNullOrEmpty(PhoneNumber) ? PhoneNumber : r.PhoneNumber;
                    r.PhoneNumber = !string.IsNullOrEmpty(PhoneNumber) ? PhoneNumber : r.PhoneNumber;
                    r.Email = !string.IsNullOrEmpty(Email) ? Email : r.Email;
                    r.RegisterDate = !string.IsNullOrEmpty(RegisterDate) ? Convert.ToDateTime(RegisterDate) : r.RegisterDate;

                    var result = await userManger.UpdateAsync(r);
                    if (result.Succeeded)
                    {
                        return Redirect("/Adminstration/users");
                    }
                    else
                        return Redirect("/Adminstration/UpdateUser?userId=" + id);
                }
            }
            return Redirect("/Adminstration/users");
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(string Id,string NewPass)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Redirect("/Identity/Account/Login");
            //}
            if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(NewPass))
            {
                ApplicationUser r = userManger.Users.FirstOrDefault(a => a.Id == Id);
                if (r != null)
                {
                    string _token= await userManger.GeneratePasswordResetTokenAsync(r);
                 IdentityResult result=   await userManger.ResetPasswordAsync(r, _token, NewPass);
                    if (result.Succeeded)
                    {
                        return Redirect("/Adminstration/users");
                    }
                }
            }
            return Redirect("/Adminstration/UpdateUser?userId=" + Id);
        }

        [HttpPost]
        public async Task<object> UpdateBalance(decimal  Balance, string id)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Redirect("/Identity/Account/Login");
            //}
            if (!string.IsNullOrEmpty(id))
            {
                Wallet _wallet = walletDb.firstOrdefault(0, id);
                if (_wallet != null)
                {
                    if (Balance > 0)
                    {
                        _wallet.Balance = Balance;
                       await  walletDb.update(_wallet);
                        return Redirect("/Adminstration/UpdateUser?userId=" + id);
             
                    }
                }
            }
            return Redirect("/Adminstration/users");
        }
        #endregion

        [HttpPost]
        public async Task<JsonResult> deleteUser(string UserName)
        {
            if (!string.IsNullOrEmpty(UserName)  )
            {
                ApplicationUser _user = await userManger.FindByNameAsync(UserName);
                if (_user != null)
                {
                    //Get user roles 
                    IEnumerable<string> userRoles = userManger.GetRolesAsync(_user).Result.ToList();
                    await userManger.RemoveFromRolesAsync(_user, userRoles);
                    await followDb.Delete(0, null, new List<FollowUser>(), _user.Id);
                    await msgDb.Delete(0, null, new List<User2UserMsg>(), _user.Id);
                    await walletDb.Delete(0, null, new List<Wallet>(), _user.Id);
                    await ChatRoomDb.Delete(0, null, new List<ChatRoom>(), _user.Id);
                    await ChatRoomMemberDb.Delete(0, null, new List<ChatRoomMember>(), _user.Id);
                    await ChatRoomMemberMsgDb.Delete(0, null, new List<ChatRoomMsg>(), _user.Id);
                    await BlockUserDb.Delete(0, null, new List<BlockUser>(), _user.Id);
                    try
                    {
                        await userManger.DeleteAsync(_user);
                        return Json(new { msg = "Done" });
                    }
                    catch (Exception x)
                    {
                        return Json(new { msg = "Error" });
                    }
               
                }
            }
            return Json(new { msg = "Error" });
        }

    }
    public class usersCls
    {
        public string FullName { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public List<string> UserRoles { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }
        public string imgUrl { get; set; }
    }
}
