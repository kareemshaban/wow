using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ChargingLevelRepositry:IFitnessRepositry<ChargingLevel>
    {

        private readonly FitnessDbContext db;

        public ChargingLevelRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(ChargingLevel entity)
        {
            await db.ChargingLevel.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, ChargingLevel entity, List<ChargingLevel> entities, string name = "", string name2 = "")
        {
            var ChargingLevel = firstOrdefault(id);
            if (ChargingLevel != null)
            {
                db.ChargingLevel.Remove(ChargingLevel);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public ChargingLevel firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                if (id > 0)
                {
                    ChargingLevel ChargingLevel = db.ChargingLevel.FirstOrDefault(a => a.Id == id);
                    if (ChargingLevel != null)
                    {
                        return ChargingLevel;
                    }
                    else
                        return null;
                }
                else
                {
                    ChargingLevel ChargingLevel = db.ChargingLevel.FirstOrDefault();
                    if (ChargingLevel != null)
                    {
                        return ChargingLevel;
                    }
                    else
                        return null;
                }
            }
            else
            {
                ChargingLevel ChargingLevel = db.ChargingLevel.FirstOrDefault(a => a.LevelName == name);
                if (ChargingLevel != null)
                {
                    return ChargingLevel;
                }
                else
                    return null;
            }

        }
        public IEnumerable<ChargingLevel> IList(string var = "", string name = "")
        {
            return db.ChargingLevel.Include(a => a.Users);
        }
        public List<ChargingLevel> list(string var = "")
        {
            return db.ChargingLevel.ToList();
        }
        public async Task<bool> update(ChargingLevel entity)
        {
            ChargingLevel ChargingLevel = firstOrdefault(entity.Id);
            if (ChargingLevel != null)
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
