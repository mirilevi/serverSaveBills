using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Classes
{
    public class CategoryDL : ICategoryDL
    {
        SaveBillsContext db;
        public CategoryDL(SaveBillsContext _db)
        {
            db = _db;
        }
        public async Task AddCategoryAsync(Bill b, Category c)
        {
            db.Categories.Add(c);
            db.BillCategories.Add(new BillCategory { BillId = b.BillId,CategoryId=c.CategoryId }) ;
            await db.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await db.Categories.ToListAsync();
        }
    }
}
