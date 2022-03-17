using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelApiController : ControllerBase
    {
        private readonly IFitnessRepositry<Level> db;

        public LevelApiController(IFitnessRepositry<Level> db)
        {
            this.db = db;
        }
        [HttpGet("AllLevels")]
        public List<LevelModelView> AllLevels()
        {
            return db.list().Select(a => new LevelModelView { Id=a.Id, GiftSendCount=a.GiftSendCount, ImgUrl=a.ImgUrl, LevelName=a.LevelName,Color=a.Color}).ToList();
        }
        [HttpGet("LevelsWithUsers")]
        public List<LevelModelView> levelsWithUsers()
        {
            return db.IList().Select(a => new LevelModelView { Id = a.Id, GiftSendCount = a.GiftSendCount, ImgUrl = a.ImgUrl, LevelName = a.LevelName,Color=a.Color, ApplicationUserId = a.Users.Select(x => x.Id).ToList() }).ToList();
        }
    }
}