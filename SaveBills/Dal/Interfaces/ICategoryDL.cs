using Dal.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface ICategoryDL
    {
        Task<List<Category>> GetAllCategoriesAsync();
        //הוספה
        Task AddCategoryAsync(Bill b,Category c);
        
        
    }
}
