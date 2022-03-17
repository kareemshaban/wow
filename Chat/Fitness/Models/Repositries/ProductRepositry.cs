using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ProductRepositry : IFitnessRepositry<Product>
    {
        private readonly FitnessDbContext db;

        public ProductRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Product entity)
        {
            await db.Product.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Product entity,List<Product> entities ,string name = "", string name2 = "")
        {
            var product = firstOrdefault(id);
            if (product != null)
            {
                db.Product.Remove(product);
               await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Product firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Product cat = db.Product.FirstOrDefault(a => a.Id== id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Product cat = db.Product.FirstOrDefault(a => a.ProductName == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }           
        }

        public List<Product> list(string var = "")
        {
            return db.Product.ToList();
        }
        public IEnumerable<Product> IList(string var = "EldahbDefault", string name = "")
        {
            var sdsdsds = db.Product.ToList();
            return db.Product.Include(a=>a.Category).Where(a=>a.Category.CatName!= var);
        }
        public async Task<bool> update(Product entity)
        {
            Product cat = firstOrdefault(entity.Id);
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
