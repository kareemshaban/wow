using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class CommentRepositry:IFitnessRepositry<Comment>
    {
        private readonly FitnessDbContext db;

        public CommentRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Comment entity)
        {
            await db.Comment.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Comment entity, List<Comment> entities, string name = "", string name2 = "")
        {
            var cat = firstOrdefault(id);
            if (cat != null)
            {
                db.Comment.Remove(cat);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Comment firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Comment cat = db.Comment.FirstOrDefault(a => a.Id == id);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }
            else
            {
                Comment cat = db.Comment.FirstOrDefault(a => a.ApplicationUserId == name);
                if (cat != null)
                {
                    return cat;
                }
                else
                    return null;
            }

        }
        public IEnumerable<Comment> IList(string var = "", string name = "")
        {
            return db.Comment.Include(a => a.Post);
        }
        public List<Comment> list(string var = "")
        {
            return db.Comment.ToList();
        }
        public async Task<bool> update(Comment entity)
        {
            Comment cat = firstOrdefault(entity.Id);
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
