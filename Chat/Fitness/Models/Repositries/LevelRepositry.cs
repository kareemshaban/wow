using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class LevelRepositry:IFitnessRepositry<Level>
    {
        private readonly FitnessDbContext db;

        public LevelRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Level entity) 
        {
            await db.Level.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Level entity,List<Level> entities, string name = "", string name2 = "")
        {
            var level = firstOrdefault(id);
            if (level != null)
            {
                db.Level.Remove(level);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Level firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                if (id > 0)
                {
                    Level level = db.Level.FirstOrDefault(a => a.Id == id);
                    if (level != null)
                    {
                        return level;
                    }
                    else
                        return null;
                }
                else
                {
                    Level level = db.Level.FirstOrDefault();
                    if (level != null)
                    {
                        return level;
                    }
                    else
                        return null;
                }
            }
            else
            {
                Level level = db.Level.FirstOrDefault(a => a.LevelName == name);
                if (level != null)
                {
                    return level;
                }
                else
                    return null;
            }

        }
        public IEnumerable<Level> IList(string var = "", string name = "")
        {
            return db.Level.Include(a => a.Users);
        }
        public List<Level> list(string var="")
        {
            return db.Level.ToList();
        }
        public async Task<bool> update(Level entity)
        {
            Level level = firstOrdefault(entity.Id);
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
