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
    public class ChargingLevelApiController : ControllerBase
    {
        private readonly IFitnessRepositry<ChargingLevel> db;

        public ChargingLevelApiController(IFitnessRepositry<ChargingLevel> db)
        {
            this.db = db;
        }
        [HttpGet("AllChargingLevels")]
        public List<ChargingLevelViewModel> AllChargingLevels()
        {
            return db.list().Select(a => new ChargingLevelViewModel { Id=a.Id, Balance=a.BalanceCount, ImgUrl=a.ImgUrl, LevelName=a.LevelName,Color=a.Color}).ToList();
        }
        [HttpGet("ChargingLevelsWithUsers")]
        public List<ChargingLevelViewModel> ChargingLevelsWithUsers()
        {
            return db.IList().Select(a => new ChargingLevelViewModel { Id = a.Id, Balance = a.BalanceCount, ImgUrl = a.ImgUrl, LevelName = a.LevelName,Color=a.Color, ApplicationUserId = a.Users.Select(x => x.Id).ToList() }).ToList();
        }
        //
    }
}