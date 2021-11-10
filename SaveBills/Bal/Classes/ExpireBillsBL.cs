using Bal.Interfaces;
using Dal.Classes;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Classes
{
    public class ExpireBillsBL : IExpireBillsBL
    {
        IExpireBillsDL expireBillsDL;
        public ExpireBillsBL(IExpireBillsDL _expireBillsDL)
        {
            expireBillsDL = _expireBillsDL;
        }

        public async Task DeleteExpireAsync(int id)
        {
            await expireBillsDL.DeleteExpireAsync(id);
        }

        public Task<List<Bill>> GetAllExpireBillsAsync(int userId)
        {
            return expireBillsDL.GetAllExpireBillsAsync(userId);
        }
    }
}
