using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class EmosionRepositry : IFitnessRepositry<Emosion>
    {
        private readonly FitnessDbContext db;

        public EmosionRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Emosion entity)
        {
           await db.Emosion.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, Emosion entity,List<Emosion> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.Emosion.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Emosion firstOrdefault(int id,string name="", string name2 = "")
        {
                Emosion cat = db.Emosion.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
        }
        public IEnumerable<Emosion> IList(string var = "", string name = "")
        {
                return db.Emosion;
        }
        public List<Emosion> list(string var = "")
        {
            return db.Emosion.ToList();
        }
        public async Task<bool> update(Emosion entity)
        {
            Emosion cat = firstOrdefault(entity.Id);
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
