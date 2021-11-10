using Dal.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IExpireBillsDL
    {
        Task<List<Bill>> GetAllExpireBillsAsync(int userId);
        Task DeleteExpireAsync(int id);
    }
}
