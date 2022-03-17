using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class HelpCenterRepositry : IFitnessRepositry<HelpCenter>
    {
        private readonly FitnessDbContext db;

        public HelpCenterRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(HelpCenter entity)
        {
            await db.HelpCenter.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, HelpCenter entity, List<HelpCenter> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.HelpCenter.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public HelpCenter firstOrdefault(int id, string name = "", string name2 = "")
        {
            return null;
        }
        public IEnumerable<HelpCenter> IList(string var = "", string name = "")
        {
            return db.HelpCenter.Include(a => a.ApplicationUser);
        }
        public List<HelpCenter> list(string var = "")
        {
            if (!string.IsNullOrEmpty(var))
            {
                return db.HelpCenter.Where(a=>a.ApplicationUserId == var).ToList();
            }
            return db.HelpCenter.ToList();
        }
        public async Task<bool> update(HelpCenter entity)
        {
            HelpCenter cat = firstOrdefault(entity.Id);
            if (cat != null)
            {
                db.Update(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;

        }
    }
}
