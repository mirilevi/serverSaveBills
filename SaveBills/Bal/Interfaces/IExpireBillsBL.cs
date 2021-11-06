using Dal.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bal.Interfaces
{
    public interface IExpireBillsBL
    {
        Task<List<Bill>> GetAllExpireBillsAsync(int userId);

    }
}
