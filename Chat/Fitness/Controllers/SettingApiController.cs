using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingApiController : ControllerBase
    {
        private readonly IFitnessRepositry<SiteSetting> db;

        public SettingApiController(IFitnessRepositry<SiteSetting> db)
        {
            this.db = db;
        }
        [HttpGet("SiteSetting")]
        public List<SiteSetting> SiteSetting()
        {
            return db.list();
        }
    }
}