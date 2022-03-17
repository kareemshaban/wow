using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class UserImageRepositry : IFitnessRepositry<UserImage>
    {
        private readonly FitnessDbContext db;

        public UserImageRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(UserImage entity)
        {
           await db.UserImage.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, UserImage entity,List<UserImage> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.UserImage.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public UserImage firstOrdefault(int id,string name="", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                UserImage cat = db.UserImage.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                UserImage cat = db.UserImage.FirstOrDefault(a => a.ApplicationUserId == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
         
        }
        public IEnumerable<UserImage> IList(string var = "", string name = "")
        {
           return  db.UserImage.Include(a=>a.ApplicationUser);
        }
        public List<UserImage> list(string var = "")
        {
            if (!string.IsNullOrEmpty(var))
            {
                return db.UserImage.Where(a => a.ApplicationUserId == var).ToList();
            }
            return db.UserImage.ToList();
        }
        public async Task<bool> update(UserImage entity)
        {
            UserImage cat = firstOrdefault(entity.Id);
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
