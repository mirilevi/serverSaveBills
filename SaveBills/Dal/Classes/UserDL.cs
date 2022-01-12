using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Classes
{
    public class UserDL :IUserDL 
    {
        SaveBillsContext db;
        public UserDL(SaveBillsContext _db)
        {
            db = _db;
        }

        public async Task<int> AddUser(User user)
        {
            if (db.Users.CountAsync(u => u.Email == user.Email).Result != 0)
            {
                return -1;
            }
            db.Users.Add(user);
            await db.SaveChangesAsync();
            //User findUser = await db.Users.FirstOrDefaultAsync(u => u.Email == user.email && u.Password == password);
            User user1 =await CheckUser(user.Email, user.Password);
            if (user1!=null)
            {
                return user1.UserId;
            }
            return -1;
        }

        public async Task<User> CheckUser(string email, string password)
        {
            User findUser = await db.Users.FirstOrDefaultAsync(u => u.Email==email&&u.Password==password);
            
            return findUser;
        }
    }
}
