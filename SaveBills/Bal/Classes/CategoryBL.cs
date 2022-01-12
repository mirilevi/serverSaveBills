﻿using Bal.Interfaces;
using Dal.Classes;
using Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Classes
{
    public class CategoryBL : ICategoryBL
    {
        ICategoryDL categoryDL;
        public CategoryBL(ICategoryDL _categoryDL)
        {
            categoryDL = _categoryDL;
        }
        public async Task AddCategoryAsync(Bill b, Category c)
        {
            await categoryDL.AddCategoryAsync(b, c);
        }

        public async Task AddNewCategoryAsync(string category, int userId)
        {
            await categoryDL.AddNewCategoryAsync(category,userId);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
           return await categoryDL.GetAllCategoriesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesUserAsync(int userId)
        {
            return await categoryDL.GetAllCategoriesUserAsync(userId);
        }
    }
}
