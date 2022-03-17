using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class OtherOtherLevelRepositry:IFitnessRepositry<OtherLevel>
    {
        private readonly FitnessDbContext db;

        public OtherOtherLevelRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(OtherLevel entity)
        {
            await db.OtherLevel.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, OtherLevel entity, List<OtherLevel> entities, string name = "", string name2 = "")
        {
            var OtherLevel = firstOrdefault(id);
            if (OtherLevel != null)
            {
                db.OtherLevel.Remove(OtherLevel);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public OtherLevel firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                if(id  > 0)
                {
                    OtherLevel OtherLevel = db.OtherLevel.FirstOrDefault(a => a.Id == id);
                    if (OtherLevel != null)
                    {
                        return OtherLevel;
                    }
                    else
                        return null;
                }
                else
                {
                    OtherLevel OtherLevel = db.OtherLevel.FirstOrDefault();
                    if (OtherLevel != null)
                    {
                        return OtherLevel;
                    }
                    else
                        return null;
                }
              
            }
            else
            {
                OtherLevel OtherLevel = db.OtherLevel.FirstOrDefault(a => a.LevelName == name);
                if (OtherLevel != null)
                {
                    return OtherLevel;
                }
                else
                    return null;
            }

        }
        public IEnumerable<OtherLevel> IList(string var = "", string name = "")
        {
            return db.OtherLevel.Include(a => a.Users);
        }
        public List<OtherLevel> list(string var = "")
        {
            return db.OtherLevel.ToList();
        }
        public async Task<bool> update(OtherLevel entity)
        {
            OtherLevel OtherLevel = firstOrdefault(entity.Id);
            if (OtherLevel != null)
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
