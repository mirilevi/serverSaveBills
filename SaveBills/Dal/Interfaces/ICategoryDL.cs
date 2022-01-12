using Dal.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface ICategoryDL
    {
        Task<List<Category>> GetAllCategoriesAsync();
        //הוספה
        Task AddCategoryAsync(Bill b,Category c);

        Task<List<Category>> GetAllCategoriesUserAsync(int userId);

        Task AddNewCategoryAsync(string category,int userId);

    }
}
