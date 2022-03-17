using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class NotificationRepositry : IFitnessRepositry<Notification>
    {
        private readonly FitnessDbContext db;

        public NotificationRepositry(FitnessDbContext _db)
        {
            db = _db;
        }

        public async Task<bool> Add(Notification entity)
        {
            await db.Notification.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Notification entity, List<Notification> entities, string name = "", string name2 = "")
        {
            var Notification = firstOrdefault(id);
            if (Notification != null)
            {
                db.Notification.Remove(Notification);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public Notification firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Notification cat = db.Notification.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Notification cat = db.Notification.FirstOrDefault(a => a.ApplicationUserId == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
        }

        public List<Notification> list(string var )
        {
            return db.Notification.Where(a=>a.ApplicationUserId == var).ToList();
        }
        public IEnumerable<Notification> IList(string var = "EldahbDefault", string name = "")
        {
            return db.Notification.Include(a => a.ApplicationUser);
        }
        public async Task<bool> update(Notification entity)
        {
            Notification cat = firstOrdefault(entity.Id);
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
