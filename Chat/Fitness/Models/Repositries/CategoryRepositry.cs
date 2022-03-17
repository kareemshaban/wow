using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class CategoryRepositry : IFitnessRepositry<Category>
    {
        private readonly FitnessDbContext db;

        public CategoryRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Category entity)
        {
           await db.Category.AddAsync(entity);
           await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool >Delete(int id, Category entity,List<Category> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.Category.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Category firstOrdefault(int id,string name="", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Category cat = db.Category.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Category cat = db.Category.FirstOrDefault(a => a.CatName == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
         
        }
        public IEnumerable<Category> IList(string var = "", string name = "")
        {
            if(var !="")
           return  db.Category.Include(a=>a.Products).Where(a => a.CatName != "EldahbDefault");
            else
                return db.Category.Include(a => a.gifts);
        }
        public List<Category> list(string var = "")
        {
            return db.Category.Where(a => a.CatName != "EldahbDefault").ToList();
        }
        public async Task<bool> update(Category entity)
        {
            Category cat = firstOrdefault(entity.Id);
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
