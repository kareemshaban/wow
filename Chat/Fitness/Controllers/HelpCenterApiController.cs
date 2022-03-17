using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpCenterApiController : ControllerBase
    {
        private readonly IFitnessRepositry<HelpCenter> db;
        public HelpCenterApiController(IFitnessRepositry<HelpCenter> db)
        {
            this.db = db;
        }
        [HttpPost("Add")]
        public async Task<object> Add(HelpCenter help)
        {
            if (!string.IsNullOrEmpty(help.ApplicationUserId))
            {
                help.create_at = DateTime.Now;
               await db.Add(help);
                return (new { message = "Success" });
            }
            return (new { message = "Error : User not  found" });

        }

    }
}