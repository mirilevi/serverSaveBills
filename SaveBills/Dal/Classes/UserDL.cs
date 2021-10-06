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
        public async Task<User> CheckUser(string email, string password)
        {
            User findUser = await db.Users.FirstOrDefaultAsync(u => u.Email==email&&u.Password==password);
            
            return findUser;
        }
    }
}
