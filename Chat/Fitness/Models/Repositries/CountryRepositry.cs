using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class CountryRepositry : IFitnessRepositry<Country>
    {
        private readonly FitnessDbContext db;

        public CountryRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Country entity)
        {
           await db.Country.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, Country entity,List<Country> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.Country.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Country firstOrdefault(int id,string name="", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Country cat = db.Country.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Country cat = db.Country.FirstOrDefault(a => a.Name == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
         
        }
        public IEnumerable<Country> IList(string var = "", string name = "")
        {
            if(var !="")
           return  db.Country;
            else
                return db.Country;
        }
        public List<Country> list(string var = "")
        {
            return db.Country.ToList();
        }
        public async Task<bool> update(Country entity)
        {
            Country cat = firstOrdefault(entity.Id);
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
