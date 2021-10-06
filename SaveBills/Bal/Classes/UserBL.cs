using Bal.Interfaces;
using Dal.Classes;
using Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Classes
{
    public class UserBL : IuserBL
    {
        IUserDL userDL;
        public UserBL(IUserDL _userDL)
        {
            userDL = _userDL;
        }
        public async Task<User> CheckUser(string email, string password)
        {
            return await userDL.CheckUser(email, password);
        }
    }
}
