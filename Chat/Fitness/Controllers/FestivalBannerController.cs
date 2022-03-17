using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
     
    public class FestivalBannerController : Controller
    {
        private readonly Models.Repositries.IFitnessRepositry<FestivalBanner> db;
        private readonly IHostingEnvironment hostingEnvironment;

        public FestivalBannerController(IFitnessRepositry<FestivalBanner> _db, IHostingEnvironment hostingEnvironment)
        {
            this.db = _db;
            this.hostingEnvironment = hostingEnvironment;
        }
        //[Authorize]
        public async Task<ActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var x = User.Identity.Name;
            List<FestivalBannerViewModel> validBanners = new List<FestivalBannerViewModel>();
            FestivalBannerViewModel valid = new FestivalBannerViewModel();
            List<FestivalBanner> deletedBanners = new List<FestivalBanner>();
            foreach (var item in db.IList("notApprove").Where(a => a.Refused == false))
            {
                if (bannerValied(item.StartDate, item.DaysCount))
                {
                    valid.Id = item.Id;
                    valid.UserId = item.ApplicationUser.UserId.ToString();
                    valid.FulName = item.ApplicationUser.FulName;
                    valid.Msg = item.Msg;
                    valid.MainImage = item.MainImage;
                    valid.StartDate = item.StartDate.ToString();
                    valid.DaysCount = item.DaysCount.ToString();
                    valid.Approve = item.Approve;
                    validBanners.Add(valid);
                    valid = new FestivalBannerViewModel();
                }
                else
                    deletedBanners.Add(item);
            }
            foreach (var item in deletedBanners)
            {
                await db.Delete(0, item, null);
            }
            return View(validBanners);
        }
        [HttpGet]
        public ActionResult Show(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (Id > 0)
            {
                FestivalBanner fes = db.firstOrdefault(Id);
                if (fes.Refused== false)
                {
                    return View(fes);
                }
            }
                return Redirect("/FestivalBanner/Index");
        }
        [HttpPost]
        public async Task<JsonResult> Refuse(int id)
        {
            try
            {
                FestivalBanner f = db.firstOrdefault(id);
                if (f != null)
                {
                    f.Refused = true;
                    await db.update(f);
                    return Json(new { msg = "Done",userId=f.ApplicationUserId });
                }
                return Json(new { msg = "Error" });
            }
            catch
            {
                return Json(new { msg = "Error" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> Approve(int id)
        {
            try
            {
                FestivalBanner f = db.firstOrdefault(id);
                if (f != null)
                {
                    f.Approve = true;
                    await db.update(f);
                    return Json(new { msg = "Done" });
                }
                return Json(new { msg = "Error" });
            }
            catch
            {
                return Json(new { msg = "Error" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> deletBanner(int id)
        {
            try
            {
                if (id > 0)
                {
                    await db.Delete(id, null, null);
                    return Json(new { msg = "Done" });
                }
                return Json(new { msg = "Error" });
            }
            catch
            {
                return Json(new { msg = "Error" });
            }
        }
        public bool bannerValied(DateTime startDate, double count)
        {
            DateTime day = DateTime.Now;
            DateTime endDate = startDate.AddDays(count);
       
            int LessEqual = DateTime.Compare(day, endDate);
            if (LessEqual <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
