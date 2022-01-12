using Dal.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public static class CategoryDTOConverter
    {
        public static CategoryDTO ConvertToDTO(this Category c)
        {
            return new CategoryDTO() {
                BillCategories = c.BillCategories,
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            };
        }
        public static Category ConvertFromDTO(this CategoryDTO c)
        {
            return new Category()
            {
               CategoryId = c.CategoryId,
               BillCategories = c.BillCategories ,
               CategoryName = c.CategoryName,
               UserCategories = null,
            };
        }

        public static List<CategoryDTO> convertListToDTO(this List<Category> categories)
        {
            return categories.Select(b => b.ConvertToDTO()).ToList();
        }

        public static List<Category> convertListFormDTO(this List<CategoryDTO> categories)
        {
            return categories.Select(b => b.ConvertFromDTO()).ToList();
        }
    }
}
