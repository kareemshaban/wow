using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class SiteSettingRepositry : IFitnessRepositry<SiteSetting>
    {
        private readonly FitnessDbContext db;

        public SiteSettingRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(SiteSetting entity)
        {
            await db.SiteSetting.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, SiteSetting entity, List<SiteSetting> entities, string name = "", string name2 = "")
        {
            var level = firstOrdefault(id);
            if (level != null)
            {
                db.SiteSetting.Remove(level);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public SiteSetting firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (id > 0)
            {
                SiteSetting level = db.SiteSetting.FirstOrDefault(a => a.Id == id);
                if (level != null)
                    return level;
                else
                    return new SiteSetting();
            }
            else
            {
                return db.SiteSetting.FirstOrDefault();
            }
        }
        public IEnumerable<SiteSetting> IList(string var = "", string name = "")
        {
            return db.SiteSetting;
        }
        public List<SiteSetting> list(string var = "")
        {
            return db.SiteSetting.ToList();
        }
        public async Task<bool> update(SiteSetting entity)
        {
            SiteSetting level = firstOrdefault(entity.Id);
            if (level != null)
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
