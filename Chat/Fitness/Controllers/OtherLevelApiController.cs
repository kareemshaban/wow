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
    public class OtherLevelApiController : ControllerBase
    {
        private readonly IFitnessRepositry<OtherLevel> db;

        public OtherLevelApiController(IFitnessRepositry<OtherLevel> db)
        {
            this.db = db;
        }
        [HttpGet("Levels")]
        public List<OtherLevelViewModel> Levels()
        {
            return db.list().Select(a => new OtherLevelViewModel { Id = a.Id, GiftRecieverCount = a.GiftRecieverCount, LevelName = a.LevelName,ImgUrl=a.ImgUrl,Color=a.Color}).ToList();
        }


    }
}