using Chat.Models.ViewModel;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roles;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<UserImage> userImgDb;
        private readonly IFitnessRepositry<UserImageLike> userImgLikeDb;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> manager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IFitnessRepositry<Level> levelDb;
        private readonly IFitnessRepositry<Wallet> walletDb;
        private readonly IFitnessRepositry<OtherLevel> otherLevelDb;
        private readonly IFitnessRepositry<Country> country;
        private readonly IFitnessRepositry<ChargingLevel> chargingDb;
        private readonly IFitnessRepositry<SiteSetting> settingDb;

        public AccountController(RoleManager<IdentityRole> roles, IHostingEnvironment hostingEnvironment, IFitnessRepositry<UserImage> userImgDb, IFitnessRepositry<UserImageLike> userImgLikeDb, UserManager<ApplicationUser> _manager, SignInManager<ApplicationUser> _signInManager, IFitnessRepositry<Level> levelDb, IFitnessRepositry<Wallet> walletDb, IFitnessRepositry<OtherLevel> otherLevelDb, IFitnessRepositry<Country> _country, IFitnessRepositry<ChargingLevel> ChargingDb)
        {
            this.roles = roles;
            this.hostingEnvironment = hostingEnvironment;
            this.userImgDb = userImgDb;
            this.userImgLikeDb = userImgLikeDb;
            manager = _manager;
            signInManager = _signInManager;
            this.levelDb = levelDb;
            this.walletDb = walletDb;
            this.otherLevelDb = otherLevelDb;
            country = _country;
            chargingDb = ChargingDb;
        }

        [HttpGet("countries")]
        public object countries()
        {
            try
            {
                return country.list();
            }
            catch (Exception e)
            {
                return new { message = "Error", alertMsg = e.Message };
            }
        }



        [HttpPost("Register")]
        public async Task<object> register(UserRegister model)
        {
            try
            {
                var users = manager.Users.ToList();
                //if (users.FirstOrDefault(a => a.FulName.Equals(model.FulName)) != null)
                //{
                //    return new { message = "Error : Name is exist" };
                //}
                int startId = 100000;
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
                if (charging != null)
                {
                    chargingLevelId = charging.Id;
                }
                else
                {
                    charging = new ChargingLevel();
                    charging.BalanceCount = 0;
                    charging.ImgUrl = "";
                    charging.LevelName = "Zero level";
                    await chargingDb.Add(charging);
                    chargingLevelId = charging.Id;
                }

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
                    ChargingLevelId = chargingLevelId,
                    UserId = startId,
                    RegisterDate = DateTime.Now,
                    blockedStartDate = Convert.ToDateTime("10-10-1990"),
                    CountryId = model.CountryId,
                	EmailConfirmed=true,
                };
                IdentityResult result = await manager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    if (result.Errors.ToList()[0].Description.Contains("User name"))
                    {
                        return new { message = "Error", alertMsg = "This username is used" };
                    }
                    // return BadRequest(result.Errors.ToList()[0]);
                    return new { message = "Error", alertMsg = result.Errors.ToList()[0].Description };
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
                            IdentityRole _role = new IdentityRole()
                            {
                                Name = model.roleName
                            };
                            IdentityResult _res = await roles.CreateAsync(_role);
                            if (_res.Succeeded)
                            {
                                await manager.AddToRoleAsync(user, model.roleName);

                            }
                        }
                        return new { message = "success", alertMsg = user.Id };
                    }
                    else
                        return new { message = "error", alertMsg = user.Id + "  Wallet not created" };
                }
            }
            catch (Exception e)
            {
                return new { message = "Error", alertMsg = e.Message };
            }
        }

        [HttpPost("createUser")]
        public async Task<object> createUser(UserRegister model)
        {
            try
            {
                int startId = 1000;
                Level level = levelDb.firstOrdefault(0);
                OtherLevel other = otherLevelDb.firstOrdefault(0);
                int levelId = 0;
                int otherLevelId = 0;
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
                var users = manager.Users.ToList();
            
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
                    UserId = startId
                };
                IdentityResult result = await manager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    if (result.Errors.ToList()[0].Description.Contains("User name"))
                    {
                        return new { message = "Error", alertMsg = "This username is used" };
                    }
                    // return BadRequest(result.Errors.ToList()[0]);
                    return new { message = "Error", alertMsg = result.Errors.ToList()[0].Description };
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
                        return new { message = "success", alertMsg = user.Id };
                    }
                    else
                        return new { message = "success", alertMsg = user.Id + "  Wallet not created" };
                }
            }
            catch (Exception e)
            {
                return new { message = "Error", alertMsg = e.Message, alertMsg2 = e.InnerException.InnerException };
            }
        }
        [HttpPost("Login")]
        public async Task<object> Login(LoginViewModel model)
        {
            ApplicationUser anonymousUser = await manager.FindByEmailAsync(model.Email);
            if (anonymousUser == null)
                return BadRequest("Error : Invalid login attempt");
            var result = await signInManager.PasswordSignInAsync(anonymousUser, model.Password, true, false);
            if (result.Succeeded)
            {
                return new { message = "Success", Id = anonymousUser.Id };
            }
            return new { message = "Error", alertMsg = result.ToString() };
        }
        [HttpPost("UploadImage")]
        public object UploadImage([FromForm] UploadFileviewModel file)
        {
            try
            {
                if (file.file != null && !string.IsNullOrEmpty(file.fileName))
                {
                    string path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "UsersImage");
                    path = System.IO.Path.Combine(path, file.fileName);
                    file.file.CopyTo(new FileStream(path, FileMode.Create));
                    return (new { message = "Success" });
                }
                return (new { message = "Error : file not found" });
            }
            catch (Exception x)
            {
                return (new { message = "Error : upload not complete. : Inner Exeception - " + x.InnerException.Message + "Message " + x.Message });
            }
        }
        [HttpPost("UserInfo")]
        public async Task<UserRegister> UserInfo(UserInfoModel model)
        {
            ApplicationUser anonymousUser = await manager.FindByEmailAsync(model.Email);
            if (anonymousUser != null)
            {
                UserRegister user = new UserRegister();
                user.FulName = anonymousUser.FulName;
                user.DateBirth = anonymousUser.DateBirth;
                user.Email = anonymousUser.Email;
                user.Gender = anonymousUser.Gender;
                user.ImgUrl = anonymousUser.ImgUrl;
                user.UserName = anonymousUser.UserName;
                user.Id = anonymousUser.Id;
                user.UserId = anonymousUser.UserId;
                return user;
            }
            else
            {
                return new UserRegister();
            }
        }
        [HttpPost("Users")]
        public List<UserRegister> GetUsers()
        {
            try
            {
                List<ApplicationUser> _users = manager.Users.ToList();
                if (_users.Count > 0)
                {
                    return _users.Select((x) => new UserRegister { Id = x.Id, DateBirth = x.DateBirth, Email = x.Email, FulName = x.FulName, Gender = x.Gender, ImgUrl = x.ImgUrl, UserName = x.UserName, UserId = x.UserId }).ToList();
                }
                else
                {
                    return new List<UserRegister>();
                }

            }
            catch (Exception)
            {
                return new List<UserRegister>();
            }
        }
        [HttpPost("UserSetting")]
        public async Task<object> UserSetting(UserSettings model)
        {
            try
            {
                ApplicationUser _user = await manager.FindByIdAsync(model.ApplicationUserId);
                if (_user == null)
                {
                    return new { message = "Sorry : User not found " };
                }
                if (model.InterstedWith >= 0)
                {
                    _user.InterstedWith = model.InterstedWith;
                }
                else
                {
                    _user.ReceiveChatRoomMsg = model.ReceiveChatRoomMsg;
                    _user.ReceiveInvitation = model.ReceiveInvitation;
                    _user.ReceiveMsg = model.ReceiveMsg;
                }
                IdentityResult result = await manager.UpdateAsync(_user);
                if (!result.Succeeded)
                {
                    return new { message = "Error", alertMsg = result.Errors.ToList()[0].Description };
                }
                else
                {
                    return new { message = "success" };
                }
            }
            catch (Exception e)
            {
                return new { message = "Error", alertMsg = e.Message, alertMsg2 = e.InnerException.InnerException };
            }
        }

        [HttpPost("EditUserData")]
        public async Task<object> EditUserData([FromForm] EditUserData model)
        {
            try
            {
                ApplicationUser _user = await manager.FindByIdAsync(model.ApplicationUserId);
                if (_user == null)
                {
                    return new { message = "Sorry : User not found " };
                }
                _user.FulName = model.FulName == "" ? _user.FulName : model.FulName;
                if (!string.IsNullOrEmpty(model.DateBirth))
                {
                    _user.DateBirth = Convert.ToDateTime(model.DateBirth);
                }
                _user.Gender = string.IsNullOrEmpty(model.Gender) ? _user.Gender : int.Parse(model.Gender);
                if (model.file != null)
                {
                    if (!EditUserImg(model.file, model.fileName))
                        return new { message = "Error : Image not added" };
                    else
                    {
                        _user.ImgUrl = model.fileName;
                    }
                }
                if (!string.IsNullOrEmpty(model.Tower))
                {
                    _user.Tower = model.Tower;
                }
                int _countryId = 0;
                if (int.TryParse(model.CountryId.ToString(),out _countryId))
                    _user.CountryId = _countryId;

                IdentityResult result = await manager.UpdateAsync(_user);
                if (!result.Succeeded)
                {
                    return new { message = "Error", alertMsg = result.Errors.ToList()[0].Description };
                }
                else
                {
                    return new { message = "success" };
                }
            }
            catch (Exception e)
            {
                return new { message = "Error", alertMsg = e.Message, alertMsg2 = e.InnerException.InnerException };
            }
        }
        [HttpPost("Upload")]
        public async Task<object> Upload([FromForm] EditUserData model)  
        {
            try
            {
                ApplicationUser _user = await manager.FindByIdAsync(model.ApplicationUserId);
                if (_user == null)
                {
                    return new { message = "Error : User not found " };
                }
                if (!EditUserImg(model.file, model.fileName))
                    return new { message = "Error : Can't upload image" };
                else
                {
                    UserImage userImage = new UserImage()
                    {
                        ApplicationUserId = model.ApplicationUserId,
                        create_at = DateTime.Now,
                        ImgUrl = model.fileName
                    };

                    await userImgDb.Add(userImage);
                    return new { message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new { message = "Error", alertMsg = e.Message, alertMsg2 = e.InnerException.InnerException };
            }
        }

        [HttpPost("GalleryLike")]
        public async Task<object> GalleryLike(UserImageLike model) 
        {
            try
            {
                ApplicationUser _user = await manager.FindByIdAsync(model.ApplicationUserId);
                if (_user == null)
                {
                    return new { message = "Error : User not found " };
                }
                if ((userImgLikeDb.list(model.ApplicationUserId)).Where(a => a.LikerId == model.LikerId).Count() > 0)
                    return new { message = "Error : User add  like already" };
                else
                {
                    UserImageLike userImageLike = new UserImageLike()
                    {
                        ApplicationUserId = model.ApplicationUserId,
                        date = DateTime.Now,
                        LikerId = model.LikerId
                    };
                    await userImgLikeDb.Add(userImageLike);
                    return new { message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new { message = "Error", alertMsg = e.Message, alertMsg2 = e.InnerException.InnerException };
            }
        }

        [HttpPost("GalleryUnLike")]
        public async Task<object> GalleryUnLike(UserImageLike model)
        {
            try
            {
                List<UserImageLike> like = userImgLikeDb.list(model.ApplicationUserId);
                foreach (var item in like.Where(a => a.LikerId == model.LikerId).ToList())
                {
                    await userImgLikeDb.Delete(0, item, new List<UserImageLike>());
                }
                return new { message = "Success" };
            }
            catch (Exception e)
            {
                return new { message = "Error", alertMsg = e.Message, alertMsg2 = e.InnerException.InnerException };
            }
        }

        public bool EditUserImg(IFormFile img, string fileName)
        {
            try
            {
                if (img != null && !string.IsNullOrEmpty(fileName))
                {
                    string path = hostingEnvironment.WebRootPath;
                    path = System.IO.Path.Combine(path, "UsersImage");
                    path = System.IO.Path.Combine(path, fileName);
                    img.CopyTo(new FileStream(path, FileMode.Create));
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost("AddEditState")]
        public async Task<object> AddEditState(StateViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.about))
            {
                ApplicationUser user = await manager.FindByIdAsync(model.ApplicationUserId);
                if (user != null)
                {
                    user.about = model.about;
                    await manager.UpdateAsync(user);
                    return (new { message = "Success" });
                }
                return (new { message = "User not found" });
            }
            return (new { message = "Data not complete" });
        }
        public class UserSettings
        {
            public int InterstedWith { get; set; } 
            public bool ReceiveMsg { get; set; } 
            public bool ReceiveInvitation { get; set; }
            public bool ReceiveChatRoomMsg { get; set; }
            public string ApplicationUserId { get; set; }
        }
        public class MessagePasswordValidator
        {
            public bool RequireDigit { get; set; }
            public int RequiredLength { get; set; }
            public bool RequireLowercase { get; set; }
            public bool RequireNonLetterOrDigit { get; set; }
            public bool RequireUppercase { get; set; }
        }
    }
}