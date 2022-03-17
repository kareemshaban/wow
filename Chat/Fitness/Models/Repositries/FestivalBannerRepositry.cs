using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class FestivalBannerRepositry:IFitnessRepositry<FestivalBanner>
    {
        private readonly FitnessDbContext db;

        public FestivalBannerRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(FestivalBanner entity)
        {
            await db.FestivalBanner.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, FestivalBanner entity, List<FestivalBanner> entities, string name = "", string name2 = "")
        {
            if (entity != null)
            {
                db.FestivalBanner.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                var FestivalBanner = firstOrdefault(id);
                if (FestivalBanner != null)
                {
                    db.FestivalBanner.Remove(FestivalBanner);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
        }
        public FestivalBanner firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                if (id > 0)
                {
                    FestivalBanner FestivalBanner = db.FestivalBanner.FirstOrDefault(a => a.Id == id);
                    if (FestivalBanner != null)
                    {
                        return FestivalBanner;
                    }
                    else
                        return null;
                }
                else
                {
                    FestivalBanner FestivalBanner = db.FestivalBanner.FirstOrDefault();
                    if (FestivalBanner != null)
                    {
                        return FestivalBanner;
                    }
                    else
                        return null;
                }
            }
            else
            {
                //FestivalBanner FestivalBanner = db.FestivalBanner.FirstOrDefault(a => a.FestivalBannerName == name);
                //if (FestivalBanner != null)
                //{
                //    return FestivalBanner;
                //}
                //else
                    return null;
            }

        }
        public IEnumerable<FestivalBanner> IList(string var = "", string name = "")
        {
            if (!string.IsNullOrEmpty(var))
            {
                return db.FestivalBanner.Include(a => a.ApplicationUser);
            }else
                return db.FestivalBanner.Where(a => a.Approve == true).Include(a => a.ApplicationUser);
        }
        public List<FestivalBanner> list(string var = "")
        {
            return db.FestivalBanner.ToList();
        }
        public async Task<bool> update(FestivalBanner entity)
        {
            FestivalBanner FestivalBanner = firstOrdefault(entity.Id);
            if (FestivalBanner != null)
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
