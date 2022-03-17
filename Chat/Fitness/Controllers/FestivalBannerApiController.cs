using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models.ViewModel;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FestivalBannerApiController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<FestivalBanner> db;
        private readonly IFitnessRepositry<SiteSetting> settingDb;
        private readonly IFitnessRepositry<Wallet> walletDb;

        public FestivalBannerApiController(IHostingEnvironment hostingEnvironment, IFitnessRepositry<FestivalBanner> db, IFitnessRepositry<SiteSetting> settingDb, IFitnessRepositry<Wallet> walletDb)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.db = db;
            this.settingDb = settingDb;
            this.walletDb = walletDb;
        }
        [HttpPost("Add")]
        public async Task<object> Add([FromForm] FestivalBannerViewModel model)
        {
            FestivalBanner b = new FestivalBanner();
            try
            {
                if (!string.IsNullOrEmpty(model.ApplicationUserId) && !string.IsNullOrEmpty(model.MainImage) && !string.IsNullOrEmpty(model.Msg) && model.img != null)
                {
                    if (model.img != null)
                    {
                        string path = hostingEnvironment.WebRootPath;
                        path = System.IO.Path.Combine(path, "FestivalBanner");
                        path = System.IO.Path.Combine(path, model.MainImage);
                        model.img.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    int DaysCount = int.Parse(model.DaysCount);
                    DateTime StartDate = Convert.ToDateTime(model.StartDate);
                    b.ApplicationUserId = model.ApplicationUserId;
                    b.Approve = false;
                    b.DaysCount = DaysCount;
                    b.MainImage = model.MainImage;
                    b.Msg = model.Msg;
                    b.RoomId = int.Parse(model.RoomId);
                    b.StartDate = StartDate;
                    if (await db.Add(b))
                    {
                        SiteSetting setting = settingDb.firstOrdefault(0);
                        Wallet wallet = walletDb.firstOrdefault(0, model.ApplicationUserId);
                        if (setting != null && wallet != null)
                        {
                            if (wallet.Balance >= (setting.FestivalBannerPrice * DaysCount))
                            {
                                wallet.Balance = wallet.Balance - (setting.FestivalBannerPrice * DaysCount);
                                try
                                {
                                    if (await walletDb.update(wallet))
                                        return (new { message = "Success" });
                                    else
                                        return (new { message = "Error : Not add" });
                                }
                                catch (Exception)
                                {
                                    await db.Delete(b.Id, null, null);
                                    return (new { message = "Error :Exception wallet" });
                                }
                            }
                            await db.Delete(0, b, null);
                            return (new { message = "Error : Balance not  " });
                        }
                        await db.Delete(0, b, null);
                        return (new { message = "Error : Data not complete " });
                    }
                }
                return (new { message = "Error : Data not complete " });
            }
            catch (Exception Sd)
            {
                if (b.DaysCount > 0 && b.Msg != "")
                {
                    await db.Delete(0, b, null);
                }
                return (new { message = "Error : Exception" + Sd.InnerException.Message + "&& Msg : " + Sd.Message });
            }
        }

        [HttpPost("All")]
        public async Task<List<FestivalBannerViewModel>> All()
        {
            try
            {
                List<FestivalBannerViewModel> validBanners = new List<FestivalBannerViewModel>();
                FestivalBannerViewModel valid = new FestivalBannerViewModel();
                List<FestivalBanner> deletedBanners = new List<FestivalBanner>();
                var s = db.IList();
                int checkValid = -1;
                foreach (var item in db.IList().Where(a => a.Refused == false))
                {
                    checkValid = bannerValied(item.StartDate, item.DaysCount);
                    if (checkValid == 1)
                    {
                        valid.Id = item.Id;
                        valid.ApplicationUserId = item.ApplicationUserId;
                        valid.FulName = item.ApplicationUser.FulName;
                        valid.ImgUrl = item.ApplicationUser.ImgUrl;
                        valid.Msg = item.Msg;
                        valid.MainImage = item.MainImage;
                        valid.StartDate = item.StartDate.ToShortDateString();
                        valid.DaysCount = item.DaysCount.ToString();
                        valid.RoomId = item.RoomId.ToString();
                        validBanners.Add(valid);
                        valid = new FestivalBannerViewModel();
                    }
                    else if (checkValid == 3)
                        deletedBanners.Add(item);
                }
                foreach (var item in deletedBanners)
                {
                    await db.Delete(0, item, null);
                }
                return validBanners;
            }
            catch (Exception)
            {
                return new List<FestivalBannerViewModel>();
            }
        }
        public int bannerValied(DateTime startDate, double count)
        {
            DateTime day = DateTime.Now;
            DateTime endDate = startDate.AddDays(count);
            int LargeEqual = DateTime.Compare(day, startDate);
            int LessEqual = DateTime.Compare(day, endDate);
            if (LessEqual <= 0)
            {
                if (LargeEqual >= 0)
                {
                    return 1;// 
                }
                else
                    return 2; // 
            }
            return 3; // 
        }
    }
}
