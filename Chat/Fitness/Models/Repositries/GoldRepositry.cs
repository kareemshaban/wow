using Fitness.Models;
using Fitness.Models.Repositries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class GoldRepositry : IFitnessRepositry<Gold>
    {
        private readonly FitnessDbContext db;
        public GoldRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Gold entity)
        {
            Product p = new Product() { CategoryId = entity.CategoryId, Id = entity.Id, Price = entity.Price, ProductName = entity.ProductName };
            await db.Product.AddAsync(p);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Gold entity,List<Gold> entities, string name = "", string name2 = "")
        {
            var product = firstOrdefault(id);
            if (product != null)
            {
                Product p = new Product() { CategoryId=product.CategoryId, Id=product.Id, ImgUrl="", Price=product.Price, ProductName=product.ProductName };
                db.Product.Remove(p);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Gold firstOrdefault(int id,  string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
               List<Gold>cat = db.Product.Where(a => a.Id == id&& a.Category.CatName== "EldahbDefault").Select(a => new Gold { ProductName = a.ProductName, Price = a.Price, CategoryId = a.CategoryId, Id = a.Id }).ToList(); ;
                if (cat.Count > 0 )
                {
                    return cat[0];
                }
                else
                    return null;
            }
            else
            {
                List<Gold> cat = db.Product.Where(a => a.ProductName == name && a.Category.CatName == "EldahbDefault").Select(a => new Gold { ProductName = a.ProductName, Price = a.Price, CategoryId = a.CategoryId, Id = a.Id }).ToList();
                if (cat.Count > 0)
                {
                    return cat[0];
                }
                else
                    return null;
            }
        }

        public List<Gold> list(string var = "")
        {

            return db.Product.Where(a=>a.Category.CatName== "EldahbDefault").Select(a=>new Gold { ProductName=a.ProductName, Price=a.Price, CategoryId=a.CategoryId, Id=a.Id }).ToList();
        }
        public IEnumerable<Gold> IList(string var = "", string name = "")
        {
            return null;
        }
        public async Task<bool> update(Gold entity)
        {
            Gold cat = firstOrdefault(entity.Id);
            if (cat != null)
            {
                Product p = new Product() { CategoryId=entity.CategoryId, Id=entity.Id, Price=entity.Price, ProductName=entity.ProductName };
                db.Update(p);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;

        }
    }
}
