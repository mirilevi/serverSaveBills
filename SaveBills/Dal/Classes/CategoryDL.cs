using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            db.BillCategories.Add(new BillCategory { BillId = b.BillId, CategoryId = c.CategoryId });
            await db.SaveChangesAsync();
        }

        public async Task AddNewCategoryAsync(string category, int userId)
        {
            if (!db.Categories.Any(c=>c.CategoryName == category))
            {
                await db.Categories.AddAsync(new Category() { CategoryName = category });
                await db.SaveChangesAsync();
            }
            await db.UserCategories.AddAsync(new UserCategory() { UserId = userId, CategoryId = db.Categories.Single(c => c.CategoryName == category).CategoryId });
            await db.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await db.Categories.ToListAsync();
        }


        //החזרת כל הקטגוריות למשתמש ספציפי
        public async Task<List<Category>> GetAllCategoriesUserAsync(int userId)
        {
            return await db.UserCategories.Where(c => c.UserId == userId).Select(uc => uc.Category).ToListAsync();
            //var userBills = await db.Bills.Where(b => b.UserId == userId).Include(bill => bill.BillCategories)
            //    .ThenInclude(BillCategories => BillCategories.Category).ToListAsync();
            //List<Category> categoriesUser = new List<Category>();
            //userBills.ForEach(bill=> categoriesUser.Add(bill.BillCategories.(b=>b.Category).ToList() }
            //foreach (var bill in userBills)
            //{
            //    foreach (var billCategory in bill.BillCategories)
            //    {
            //        if (!categoriesUser.Contains(billCategory.Category))
            //            categoriesUser.Add(billCategory.Category);
            //    }
            //}
            //return categoriesUser;
        }
    }
}
