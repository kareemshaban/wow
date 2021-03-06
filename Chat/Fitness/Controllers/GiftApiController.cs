using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftApiController : ControllerBase
    {
        private readonly IFitnessRepositry<UserGift> db;
        private readonly IFitnessRepositry<Level> leveldb;
        private readonly IFitnessRepositry<OtherLevel> otherdb;
        private readonly IFitnessRepositry<ApplicationUser> userdb;

        public GiftApiController(IFitnessRepositry<UserGift> db, IFitnessRepositry<ApplicationUser> Userdb, IFitnessRepositry<Level> Leveldb, IFitnessRepositry<OtherLevel> Otherdb)
        {
            this.db = db;
            leveldb = Leveldb;
            otherdb = Otherdb;
            userdb = Userdb;
        }
        [HttpPost("UserGifts")]
        public List<UserGiftViewModel> UserGifts(UserInfoModel user)
        {
            return db.IList(user.ApplicationUserId).Select(a => new UserGiftViewModel { Id = a.Id, ApplicationUserId = a.ApplicationUserId, GiftId = a.GiftId, ImgUrl = a.Gift.ImgUrl, Price = a.Gift.Price, GiftName = a.Gift.GiftName ,DaysCount=a.Gift.DaysCount,SoundUrl=a.Gift.SoundUrl}).ToList();
        }
        [HttpPost("UsingGift")] 
        public async Task<object> UsingProduct(UserGift p)
        {
            try
            {
                if (p.GiftId > 0 && !string.IsNullOrEmpty(p.NewUserId))
                {
                    p.Used = true;
                    if (await db.update(p))
                    {
                        return (new { message = "Success" });
                    }
                }
                return (new { message = "Error : Not added" });
            }
            catch (Exception)
            {
                return (new { message = "Error : Execption" });
            }
        }

    }
}