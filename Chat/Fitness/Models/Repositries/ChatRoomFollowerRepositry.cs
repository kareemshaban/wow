using Fitness.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ChatRoomFollowerRepositry:IFitnessRepositry<ChatRoomFollower>
    {
        private readonly FitnessDbContext db;

        public ChatRoomFollowerRepositry(FitnessDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Add(ChatRoomFollower entity)
        {
            try
            {
                await db.ChatRoomFollower.AddAsync(entity);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }         
        }
        public async Task<bool> Delete(int id, ChatRoomFollower entity,List<ChatRoomFollower> entities, string name = "", string name2 = "")
        {
            if (entities.Count > 0)
            {
                db.ChatRoomFollower.RemoveRange(entities);
                await db.SaveChangesAsync();
                return true;
            }else if (entity != null)
            {
                db.ChatRoomFollower.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                ChatRoomFollower member = firstOrdefault(id);
                if (member != null)
                {
                    db.ChatRoomFollower.Remove(member);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
        }
        public ChatRoomFollower firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name) && id  > 0)
            {
                ChatRoomFollower member = db.ChatRoomFollower.FirstOrDefault(a => a.Id == id);
                if (member != null)
                {
                    return member;
                }
                else
                    return null;
            }
            else
            {
                ChatRoomFollower member = db.ChatRoomFollower.FirstOrDefault(a => a.ApplicationUserId == name&& a.ChatRoomId==id);
                if (member != null)
                {
                    return member;
                }
                else
                    return null;
            }

        }
        public IEnumerable<ChatRoomFollower> IList(string var = "", string name = "")
        {
            if (var == "getrooms")
            {
                return db.ChatRoomFollower.Where(a => a.ChatRoomId == int.Parse(name));
            }
            if (var != "")
            {
                return db.ChatRoomFollower.Where(a=>a.ChatRoomId   == int.Parse(var)).Include(a => a.ApplicationUserId);
            }
            return db.ChatRoomFollower.Include(a => a.ChatRoom);
        }
        public List<ChatRoomFollower> list(string var = "")
        {
            int rooomId = 0;
            if (int.TryParse(var,out rooomId))
            {
                return db.ChatRoomFollower.Where(a => a.ChatRoomId == rooomId).ToList();
            }
            return db.ChatRoomFollower.Where(a => a.ApplicationUserId == var).ToList();

        }
        public async Task<bool> update(ChatRoomFollower entity)
        {
            ChatRoomFollower p = firstOrdefault(entity.Id);
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
