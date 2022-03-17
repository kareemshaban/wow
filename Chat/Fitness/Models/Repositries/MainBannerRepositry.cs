using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class MainBannerRepositry:IFitnessRepositry<MainBanner>
    {
        private readonly FitnessDbContext db;

        public MainBannerRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(MainBanner entity)
        {
            await db.MainBanner.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, MainBanner entity, List<MainBanner> entities, string name = "", string name2 = "")
        {
            var MainBanner = firstOrdefault(id);
            if (MainBanner != null)
            {
                db.MainBanner.Remove(MainBanner);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public MainBanner firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                if (id > 0)
                {
                    MainBanner MainBanner = db.MainBanner.FirstOrDefault(a => a.Id == id);
                    if (MainBanner != null)
                    {
                        return MainBanner;
                    }
                    else
                        return null;
                }
                else
                {
                    MainBanner MainBanner = db.MainBanner.FirstOrDefault();
                    if (MainBanner != null)
                    {
                        return MainBanner;
                    }
                    else
                        return null;
                }
            }
            else
            {
                //MainBanner MainBanner = db.MainBanner.FirstOrDefault(a => a.MainBannerName == name);
                //if (MainBanner != null)
                //{
                //    return MainBanner;
                //}
                //else
                    return null;
            }

        }
        public IEnumerable<MainBanner> IList(string var = "", string name = "")
        {
            return db.MainBanner;
        }
        public List<MainBanner> list(string var = "")
        {
            return db.MainBanner.ToList();
        }
        public async Task<bool> update(MainBanner entity)
        {
            MainBanner MainBanner = firstOrdefault(entity.Id);
            if (MainBanner != null)
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
