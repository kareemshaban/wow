using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class PostRepositry : IFitnessRepositry<Post>
    {
        private readonly FitnessDbContext db;
        public PostRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(Post entity)
        {
            await db.Post.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, Post entity,List<Post> entities, string name = "", string name2 = "")
        {
            var post = firstOrdefault(id);
            if (post != null)
            {
                db.Post.Remove(post);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public Post firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                Post post = db.Post.FirstOrDefault(a => a.Id == id);
                if (post != null)
                {
                    return post;
                }
                else
                    return null;
            }
            else
            {
                Post post = db.Post.FirstOrDefault(a => a.content == name);
                if (post != null)
                {
                    return post;
                }
                else
                    return null;
            }

        }
        public IEnumerable<Post> IList(string var = "", string name = "")
        {
            if (var != null )
            {
                return db.Post.Include(a => a.ApplicationUser ) ;
            }
            return db.Post.Include(a => a.ApplicationUser);
        }
        public List<Post> list(string var = "")
        {
            return db.Post.ToList();
        }
        public async Task<bool> update(Post entity)
        {
            Post p = firstOrdefault(entity.Id);
            if (p != null)
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
