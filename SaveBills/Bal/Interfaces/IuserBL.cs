using Dal.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Interfaces
{
    public interface IuserBL
    {
        Task<User> CheckUser(string email, string password);

    }
}
