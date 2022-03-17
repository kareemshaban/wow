using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class MusicRepositry : IFitnessRepositry<Music>
    {
        private readonly FitnessDbContext db;

        public MusicRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Music entity)
        {
           await db.Music.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, Music entity,List<Music> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.Music.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Music firstOrdefault(int id,string name="", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Music cat = db.Music.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Music cat = db.Music.FirstOrDefault(a => a.ChatRoomId == int.Parse(name));
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
         
        }
        public IEnumerable<Music> IList(string var = "", string name = "")
        {
            if (var != "")
                return db.Music;
            else
                return db.Music;
        }
        public List<Music> list(string var = "")
        {
            return db.Music.ToList();
        }
        public async Task<bool> update(Music entity)
        {
            Music cat = firstOrdefault(entity.Id);
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
