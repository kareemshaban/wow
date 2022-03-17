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
    public class MainBannerApiController : ControllerBase
    {
        private readonly IFitnessRepositry<MainBanner> db;

        public MainBannerApiController(IFitnessRepositry<MainBanner> db)
        {
            this.db = db;
        }
        [HttpPost("All")]
        public List<MainBanner> All()
        {
            try
            {               
                return db.list();
            }
            catch (Exception)
            {
                return new List<MainBanner>();
            }
        }
    }
}
