using Dal.Classes;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IUserDL
    {
        Task<User> CheckUser(string email, string password);
    }
}
