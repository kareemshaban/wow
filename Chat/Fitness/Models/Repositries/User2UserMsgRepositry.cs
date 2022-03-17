using Fitness.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public class User2UserMsgRepositry:IFitnessRepositry<User2UserMsg>
    {
        private readonly FitnessDbContext db;

        public User2UserMsgRepositry(FitnessDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Add(User2UserMsg entity)
        {
                await db.User2UserMsg.AddAsync(entity);
                await db.SaveChangesAsync();
                return true;
          
        }
        public async Task<bool> Delete(int id, User2UserMsg entity,List<User2UserMsg> entities, string name = "", string name2 = "")
        {
            if (!string.IsNullOrEmpty(name))// خاص بالجزء بتاع حذف المستخدم نهائى من التطبيق
            {
                List<User2UserMsg> msgs = db.User2UserMsg.Where(a => a.RecieverId.Equals(name) || a.SenderId.Equals(name)).ToList();
                db.User2UserMsg.RemoveRange(msgs);
                db.SaveChanges();
                return true;
            }
            var msg = firstOrdefault(id);
            if (msg != null)
            {
                db.User2UserMsg.Remove(msg);
                await db.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public User2UserMsg firstOrdefault(int id, string name = "", string name2 = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                User2UserMsg msg = db.User2UserMsg.FirstOrDefault(a => a.Id == id);
                if (msg != null)
                {
                    return msg;
                }
                else
                    return null;
            }
            else
            {
                User2UserMsg msg = db.User2UserMsg.FirstOrDefault(a => a.Msg == name);
                if (msg != null)
                {
                    return msg;
                }
                else
                    return null;
            }

        }
        public IEnumerable<User2UserMsg> IList(string var = "", string name = "")
        {
            if (var =="")
            {
                return db.User2UserMsg.Include(a => a.Reciever).Include(a=>a.Sender);
            }
            return db.User2UserMsg.Include(a => a.Reciever).Where(a=>a.SenderId==var && a.RecieverId==name);
        }
        public List<User2UserMsg> list(string var = "")
        {
            var sdsdsd = db.User2UserMsg.ToList();
            if (!string.IsNullOrEmpty(var))
            {
                return db.User2UserMsg.Where(a => a.SenderId == var || a.RecieverId == var).ToList();
            }
            return db.User2UserMsg.ToList();
        }
        public async Task<bool> update(User2UserMsg entity)
        {
            User2UserMsg msg = firstOrdefault(entity.Id);
            if (msg != null)
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
