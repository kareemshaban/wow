using Fitness.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class ChatRoomMemberRepositry : IFitnessRepositry<ChatRoomMember>
    {
        private readonly FitnessDbContext db;

        public ChatRoomMemberRepositry(FitnessDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> Add(ChatRoomMember entity)
        {
            await db.ChatRoomMember.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id, ChatRoomMember entity, List<ChatRoomMember> entities, string name = "", string name2 = "")
        {
            if (entities.Count > 0)
            {
                db.ChatRoomMember.RemoveRange(entities);
                await db.SaveChangesAsync();
                return true;
            }
            else if (entity != null)
            {
                db.ChatRoomMember.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                if (!string.IsNullOrEmpty(name))// خاص بالجزء بتاع حذف المستخدم نهائى من التطبيق
                {
                    List<ChatRoomMember> room_member = db.ChatRoomMember.Where(a => a.ApplicationUserId.Equals(name)).ToList();
                    db.ChatRoomMember.RemoveRange(room_member);
                    db.SaveChanges();
                    return true;
                }

                ChatRoomMember member = firstOrdefault(id);
                if (member != null)
                {
                    db.ChatRoomMember.Remove(member);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
        }
        public ChatRoomMember firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name) && id > 0)
            {
                ChatRoomMember member = db.ChatRoomMember.FirstOrDefault(a => a.Id == id);
                if (member != null)
                {
                    return member;
                }
                else
                    return null;
            }
            else
            {
                ChatRoomMember member = db.ChatRoomMember.FirstOrDefault(a => a.ApplicationUserId == name && a.ChatRoomId == id);
                if (member != null)
                {
                    return member;
                }
                else
                    return null;
            }

        }
        public IEnumerable<ChatRoomMember> IList(string var = "", string name = "")
        {
            if (!string.IsNullOrEmpty(var))
            {
                return db.ChatRoomMember.Include(a => a.ApplicationUser).Where(a => a.ChatRoomId == int.Parse(var));
            }
            else
                return db.ChatRoomMember.Include(a => a.ApplicationUser);
        }
        public List<ChatRoomMember> list(string var = "")
        {
            int rooomId = 0;
            if (int.TryParse(var, out rooomId))
            {
                return db.ChatRoomMember.Where(a => a.ChatRoomId == rooomId).ToList();
            }
            return db.ChatRoomMember.Where(a => a.ApplicationUserId == var).ToList();
        }
        public async Task<bool> update(ChatRoomMember entity)
        {
            ChatRoomMember p = firstOrdefault(entity.Id);
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
