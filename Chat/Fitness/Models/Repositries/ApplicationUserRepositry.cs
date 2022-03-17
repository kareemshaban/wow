using Fitness.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ApplicationUserRepositry:IFitnessRepositry<ApplicationUser>
    {
        private readonly FitnessDbContext db;

        public ApplicationUserRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public ApplicationUser firstOrdefault(int id, string UserId = "", string name2 = "")
        {
            if (!string.IsNullOrEmpty(UserId) && id==0)
            {
                ApplicationUser _user = db.Users.Include(a => a.posts).Include(a => a.Level).Include(a => a.OtherLevel).Include(a => a.ChargingLevel).FirstOrDefault(a => a.Id == UserId);
                if (_user != null)
                {
                    return _user;
                }
                else
                    return null;
            }
            else
            {
                ApplicationUser _user = db.Users.Include(a => a.posts).Include(a => a.Level).Include(a => a.OtherLevel).FirstOrDefault(a => a.UserId == id);
                if (_user != null)
                {
                    return _user;
                }
                else
                    return null;
            }
               
        }
        public IEnumerable<ApplicationUser> IList(string var = "", string name = "")
        {
            return null;
        }
        public List<ApplicationUser> list(string var = "")
        {
            return db.Users.ToList();
        }
        public async Task<bool> update(ApplicationUser entity)
        {
             db.Users.Update(entity);
                return   true;
        }
        public async Task<bool> Delete(int id,ApplicationUser x,List<ApplicationUser> xx ,string name = "", string name2 = "")
        {
            return   true;
        }
        public async Task<bool> Add(ApplicationUser entity)
        {
            return true;
        }
    }
}
