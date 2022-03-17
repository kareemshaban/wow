using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class RolletRepositry : IFitnessRepositry<Rollet>
    {
        private readonly FitnessDbContext db;

        public RolletRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Rollet entity)
        {
           await db.Rollet.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, Rollet entity,List<Rollet> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.Rollet.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Rollet firstOrdefault(int id,string name="", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Rollet cat = db.Rollet.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Rollet cat = db.Rollet.FirstOrDefault(a => a.ChatRoomId ==int.Parse( name));
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
         
        }
        public IEnumerable<Rollet> IList(string var = "", string name = "")
        {
           return  db.Rollet.Include(a=>a.ApplicationUser);
        }
        public List<Rollet> list(string var = "")
        {
            return db.Rollet.ToList();
        }
        public async Task<bool> update(Rollet entity)
        {
            Rollet cat = firstOrdefault(entity.Id);
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
