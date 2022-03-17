using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controllers
{
    [Route("api/ChatRoomUtlity")]
    [ApiController]
    public class ChatRoomUtilityController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<Music> dbmusic;
        private readonly IFitnessRepositry<ChatRoomMember> dbMember;
        private readonly IFitnessRepositry<Emosion> dbEmosion;
        private readonly IFitnessRepositry<Background> dbBackground;
        private readonly IFitnessRepositry<ChatRoom> dbRoom;
        private readonly IFitnessRepositry<ChatRoomMsg> dbMsg;
        private readonly IFitnessRepositry<SiteSetting> dbSetting;
        private readonly IFitnessRepositry<Wallet> dbWallet;
        public ChatRoomUtilityController(IHostingEnvironment hostingEnvironment, IFitnessRepositry<Music> dbmusic, IFitnessRepositry<ChatRoomMember> dbMember, IFitnessRepositry<Wallet> dbWallet, IFitnessRepositry<SiteSetting> dbSetting, IFitnessRepositry<Emosion> dbEmosion, IFitnessRepositry<Background> dbBackground, IFitnessRepositry<ChatRoom> dbRoom, IFitnessRepositry<ChatRoomMsg> dbMsg)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.dbmusic = dbmusic;
            this.dbMember = dbMember;
            this.dbEmosion = dbEmosion;
            this.dbBackground = dbBackground;
            this.dbRoom = dbRoom;
            this.dbMsg = dbMsg;
            this.dbSetting = dbSetting;
            this.dbWallet = dbWallet;
        }
        #region Get emosion - Get Background  - AddCustomBackground - Add Music
        [HttpGet("GetEmosion")]
        public object GetEmosion()
        {
            return dbEmosion.list().Select((a) => new { Id = a.Id, Price = a.Price, ImgUrl = "/EmosionImgs/" + a.ImgUrl, createDate = a.createDate.ToString() });
        }
        [HttpPost("GetBackground")]
        public object GetBackground()
        {
            return dbBackground.list().Select((a) => new { Id = a.Id, Price = a.Price, ImgUrl = "/BackgroundImgs/" + a.ImgUrl, createDate = a.createDate.ToString() });
        }
        [HttpPost("AddCustomBackground")]
        public async Task<object> AddCustomBackground([FromForm] ChatRoom model)
        {
            if (model.Id > 0)
            {
                ChatRoom room = dbRoom.firstOrdefault(model.Id);
                if (room != null)
                {
                    SiteSetting siteSetting = dbSetting.firstOrdefault(0);
                    Wallet w = dbWallet.firstOrdefault(0, model.ApplicationUserId);
                    if (room.ApplicationUserId == model.ApplicationUserId)
                    {
                        if (w.Balance < siteSetting.CustomBackgroundPrice)
                        {
                            return (new { message = "Error : Balance not en" });
                        }
                        string path = hostingEnvironment.WebRootPath;
                        path = System.IO.Path.Combine(path, "BackgroundImgs");
                        path = System.IO.Path.Combine(path, model.CustomBackground);
                        model.Image.CopyTo(new FileStream(path, FileMode.Create));
                        room.CustomBackground = model.CustomBackground;
                        room.CustomBackgroundAdd = DateTime.Now;
                        await dbRoom.update(room);
                        return (new { message = "Success" });
                    }
                    else
                        return (new { message = "Error :User Not admin" });
                }
                else
                    return (new { message = "Error : Room not found" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        [HttpPost("AddBackground")]
        public async Task<object> AddBackground(ChatRoom model)
        {
            if (model.Id > 0)
            {
                ChatRoom room = dbRoom.firstOrdefault(model.Id);
                if (room != null)
                {
                    if (room.ApplicationUserId == model.ApplicationUserId)
                    {
                        room.BackgroundId = model.BackgroundId;
                        await dbRoom.update(room);
                        return (new { message = "Success" });
                    }
                    else
                        return (new { message = "Error :User Not admin" });
                }
                else
                    return (new { message = "Error : Room not found" });
            }
            else
                return (new { message = "Error : Data not complete" });
        }
        #endregion
     }
}