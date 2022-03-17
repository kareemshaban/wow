using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
namespace Fitness.Controllers
{

    public class SettingController : Controller
    {
        private readonly IFitnessRepositry<SiteSetting> db;
        private readonly IFitnessRepositry<ChatRoom> roomDb;
        private readonly IFitnessRepositry<SiteSetting> settingDb;
        private readonly UserManager<ApplicationUser> manager;
        private readonly IFitnessRepositry<Wallet> walletDb;
        private readonly IFitnessRepositry<UserGift> dbGift;
        public SettingController(IFitnessRepositry<SiteSetting> db, IFitnessRepositry<ChatRoom> roomDb, IFitnessRepositry<SiteSetting> settingDb, UserManager<ApplicationUser> _manager, IFitnessRepositry<Wallet> walletDb ,
            IFitnessRepositry<UserGift> dbGift)
        {
            this.db = db;
            this.roomDb = roomDb;
            this.settingDb = settingDb;
            manager = _manager;
            this.walletDb = walletDb;
            this.dbGift = dbGift;
        }
        public IActionResult Index()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            List<SiteSetting> setting = db.list();
            if (setting.Count > 0)
            {
                return View(setting[0]);
            }
            else
                return View(new SiteSetting());
        }
        [HttpPost]
       public async  Task< IActionResult> ResetChatRoomBalance()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            try
            {
                List<ChatRoom> rooms = roomDb.list().Where(c=> c.Balancea > 0).ToList();
                foreach (ChatRoom item in rooms)
                {

                    item.Balancea = 0;

                    var res = await roomDb.update(item);
                    List<UserGift> roomGifts = dbGift.IList().Where(c => c.ChatRoomID == item.Id).ToList();
                    foreach (UserGift gift in roomGifts)
                    {
                     gift.ChatRoomID = null;
                     await dbGift.update(gift);
                        
                    }
                    if (!res)
                    {
                        ViewBag.Error = "error updating the room";
                        var ErrorVM = new ErrorViewModel();

                        return View("Error", ErrorVM);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var ErrorVM = new ErrorViewModel();

                return View("Error", ErrorVM);
            }
        }

        [HttpPost]
        public IActionResult ConvertGift2Diamond(string ConvertGift2Diamond, int id)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            decimal convertGift2Diamond = 0;
            if (decimal.TryParse(ConvertGift2Diamond, out convertGift2Diamond) && id > 0)
            {
                SiteSetting setting = db.firstOrdefault(id);
                if (setting != null)
                {
                    setting.ConvertGift2Diamond = convertGift2Diamond;
                    db.update(setting);
                    return RedirectToAction("Index");
                }
                ViewBag.data = "من فضلك يرجى المحاوله مره اخرى";
                return RedirectToAction("Index");
            }
            else if (decimal.TryParse(ConvertGift2Diamond, out convertGift2Diamond))

            {
                SiteSetting setting = new SiteSetting();
                setting.ConvertGift2Diamond = convertGift2Diamond;
                db.Add(setting);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.data = "من فضلك يرجى ادخال نسبه تحويل الهديه الى ماس";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Buy100Diamond(string Buy100Diamond, int id)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            decimal buy100Diamond = 0;
            if (decimal.TryParse(Buy100Diamond, out buy100Diamond) && id > 0)
            {
                SiteSetting setting = db.firstOrdefault(id);
                if (setting != null)
                {
                    setting.Buy100Diamond = buy100Diamond;
                    db.update(setting);
                    return RedirectToAction("Index");
                }
                ViewBag.data = "من فضلك يرجى المحاوله مره اخرى";
                return RedirectToAction("Index");
            }
            else if (decimal.TryParse(Buy100Diamond, out buy100Diamond))

            {
                SiteSetting setting = new SiteSetting();
                setting.Buy100Diamond = buy100Diamond;
                db.Add(setting);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.data = "من فضلك يرجى تحديد سعر الشراء لكل 100 ماسه";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult SavePostPrice(string PostPrice, int id)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            decimal PostPriceD = 0;
            if (decimal.TryParse(PostPrice, out PostPriceD) && id > 0)
            {
                SiteSetting setting = db.firstOrdefault(id);
                if (setting != null)
                {
                    setting.PostPrice = PostPriceD;
                    db.update(setting);
                    return RedirectToAction("Index");
                }
                ViewBag.data = "من فضلك يرجى المحاوله مره اخرى";
                return RedirectToAction("Index");
            }
            else if (decimal.TryParse(PostPrice, out PostPriceD))

            {
                SiteSetting setting = new SiteSetting();
                setting.PostPrice = PostPriceD;
                db.Add(setting);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveUserIdPrice(decimal UserIdPrice, int id)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            if (UserIdPrice > 0 && id > 0)
            {
                SiteSetting setting = db.firstOrdefault(id);
                if (setting != null)
                {
                    setting.UserIdPrice = UserIdPrice;
                    db.update(setting);
                    return RedirectToAction("Index");
                }
                ViewBag.data = "من فضلك يرجى المحاوله مره اخرى";
                return RedirectToAction("Index");
            }
            else if (UserIdPrice > 0)

            {
                SiteSetting setting = new SiteSetting();
                setting.UserIdPrice = UserIdPrice;
                db.Add(setting);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SaveFestivalPrice(decimal FestivalBannerPrice, int id)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            if (FestivalBannerPrice > 0 && id > 0)
            {
                SiteSetting setting = db.firstOrdefault(id);
                if (setting != null)
                {
                    setting.FestivalBannerPrice = FestivalBannerPrice;
                    db.update(setting);
                    return Redirect("/Setting/Index");
                }
                ViewBag.data = "من فضلك يرجى المحاوله مره اخرى";
                return Redirect("/Setting/Index");
            }
            else if (FestivalBannerPrice > 0)

            {
                SiteSetting setting = new SiteSetting();
                setting.FestivalBannerPrice = FestivalBannerPrice;
                db.Add(setting);
                return Redirect("/Setting/Index");
            }
            return Redirect("/Setting/Index");
        }
        public IActionResult SaveCustomeBakgroundPrice(decimal CustomeBakgroundPrice, int id)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            if (CustomeBakgroundPrice > 0 && id > 0)
            {
                SiteSetting setting = db.firstOrdefault(id);
                if (setting != null)
                {
                    setting.CustomBackgroundPrice = CustomeBakgroundPrice;
                    db.update(setting);
                    return Redirect("/Setting/Index");
                }
                ViewBag.data = "من فضلك يرجى المحاوله مره اخرى";
                return Redirect("/Setting/Index");
            }
            else if (CustomeBakgroundPrice > 0)

            {
                SiteSetting setting = new SiteSetting();
                setting.CustomBackgroundPrice = CustomeBakgroundPrice;
                db.Add(setting);
                return Redirect("/Setting/Index");
            }
            return Redirect("/Setting/Index");
        }

        #region  شراء المعرف مجانا 
        private ApplicationUser checkUserIdAvailable(int userId)
        {
            List<ApplicationUser> _users = manager.Users.Where(a => a.UserId == userId).ToList();
            if (_users.Count > 0)
            {
                return _users[0];
            }
            else
                return null;
        }
        [HttpPost]
        public IActionResult SearchUserIdFree(int UserId)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            try
            {
                if (UserId > 0)
                {
                    if (checkUserIdAvailable(UserId) == null)
                    {
                        ViewBag.searchId = UserId;
                        return View("PayIdFree");
                    }
                    else
                    {
                        ViewBag.data = "هذا المعرف  " + UserId + " مستخدم بالفعل ";
                        ViewBag.searchId = null;
                        return View("PayIdFree");
                    }
                }
                ViewBag.searchId = null;
                return View("PayIdFree");
            }
            catch (Exception)
            {
                ViewBag.searchId = null;
                return View("PayIdFree");
            }
        }

        [HttpGet]
        public IActionResult PayIdFree()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            ViewBag.searchId = null;
            ViewBag.data = null;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PayIdFree(int IdNow, int UserId)
        {
            try
            {
                if (IdNow > 0 && UserId > 0)
                {
                    ApplicationUser _user = checkUserIdAvailable(UserId);
                    if (_user == null)
                    {
                        List<ApplicationUser> _oldUserId = manager.Users.Where(a => a.UserId == IdNow).ToList();
                        if (_oldUserId.Count > 0)
                        {
                            _oldUserId[0].UserId = UserId;
                            await manager.UpdateAsync(_oldUserId[0]);
                            ViewBag.data = " تم حجز المعرف " + UserId + " بنجاح للمعرف " + IdNow;
                            ViewBag.searchId = UserId;
                            return View();
                        }
                        else
                        {
                            ViewBag.searchId = UserId;
                            ViewBag.data = " هذا المعرف " + IdNow + "  غير موجود بقاعده البيانات    ";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.searchId = UserId;
                        ViewBag.data = "هذا المعرف  " + UserId + "  مستخدم من قبل  ";
                        return View();
                    }
                }
                ViewBag.searchId = UserId;
                ViewBag.data = "تم الغاء عمليه حجز  للمعرف  " + IdNow;
                return View();
            }
            catch (Exception)
            {
                ViewBag.searchId = UserId;
                ViewBag.data = "تم الغاء عمليه حجز  للمعرف  " + IdNow;
                return View();
            }
        }
        #endregion
        #region  شراء المعرف        
        [HttpPost]
        public IActionResult SearchUserId(int UserId)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            try
            {
                if (UserId > 0)
                {
                    if (checkUserIdAvailable(UserId) == null)
                    {
                        ViewBag.searchId = UserId;
                        return View("PayId");
                    }
                    else
                    {
                        ViewBag.data = "هذا المعرف  " + UserId + " مستخدم بالفعل ";
                        ViewBag.searchId = null;
                        return View("PayId");
                    }
                }
                ViewBag.searchId = null;
                return View("PayId");
            }
            catch (Exception)
            {
                ViewBag.searchId = null;
                return View("PayId");
            }
        }
        [HttpPost]
        public async Task<IActionResult> PayId(int IdNow, int UserId)
        {
            try
            {
                if (IdNow > 0 && UserId > 0)
                {
                    ApplicationUser _user = checkUserIdAvailable(UserId);
                    if (_user == null)
                    {
                        List<ApplicationUser> _oldUserId = manager.Users.Where(a => a.UserId == IdNow).ToList();
                        if (_oldUserId.Count > 0)
                        {
                            Wallet _wallet = walletDb.firstOrdefault(0, _oldUserId[0].Id);
                            if (_wallet != null)
                            {
                                SiteSetting _setting = settingDb.firstOrdefault(0);
                                if (_setting != null)
                                {
                                    if (_wallet.Balance > _setting.UserIdPrice)
                                    {
                                        _wallet.Balance = _wallet.Balance - _setting.UserIdPrice;
                                        if (await walletDb.update(_wallet))
                                        {
                                            try
                                            {
                                                _oldUserId[0].UserId = UserId;
                                                await manager.UpdateAsync(_oldUserId[0]);
                                                ViewBag.searchId = UserId;
                                                ViewBag.data = " تم حجز المعرف " + UserId + " بنجاح للمعرف " + IdNow;
                                                return View();
                                            }
                                            catch (Exception)
                                            {
                                                _wallet.Balance = _wallet.Balance + _setting.UserIdPrice;
                                                await walletDb.update(_wallet);
                                                ViewBag.searchId = UserId;
                                                ViewBag.data = "تم الغاء عمليه حجز  للمعرف  " + IdNow;
                                                return View();
                                            }

                                        }
                                        else
                                        {
                                            ViewBag.searchId = UserId;
                                            ViewBag.data = "لم يتم تحديث المحفظه . تم الغاء العمليه";
                                            return View();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ViewBag.searchId = UserId;
                                ViewBag.data = " تم الغاء العمليه";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.searchId = UserId;
                            ViewBag.data = " هذا المعرف " + IdNow + "  غير موجود بقاعده البيانات    ";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.searchId = UserId;
                        ViewBag.data = "هذا المعرف  " + UserId + "  مستخدم من قبل  ";
                        return View();
                    }
                }
                ViewBag.searchId = UserId;
                ViewBag.data = "تم الغاء عمليه حجز  للمعرف  " + IdNow;
                return View();
            }
            catch (Exception)
            {
                ViewBag.searchId = UserId;
                ViewBag.data = "تم الغاء عمليه حجز  للمعرف  " + IdNow;
                return View();
            }
        }

        [HttpGet]
        public IActionResult PayId()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }
            }
            ViewBag.searchId = null;
            ViewBag.data = null;
            return View();
        }
        #endregion
    }
}