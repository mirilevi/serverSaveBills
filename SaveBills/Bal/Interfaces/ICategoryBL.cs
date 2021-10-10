﻿using Dal.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Interfaces
{
    public interface ICategoryBL
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task AddCategoryAsync(Bill b, Category c);
    }
}
