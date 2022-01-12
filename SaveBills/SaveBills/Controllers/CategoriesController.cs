using Bal.Interfaces;
using Dal.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Models;
namespace SaveBills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryBL categoryBL;
        public CategoriesController(ICategoryBL _categoryBL)
        {
            categoryBL = _categoryBL;
        }
        [HttpGet("GetAllCategories")]
        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return (await categoryBL.GetAllCategoriesAsync()).convertListToDTO();
        }
        [HttpGet("GetAllCategoriesUser/{userId:int}")]
        public async Task<List<CategoryDTO>> GetAllCategoriesUser(int userId)
        {
            return (await categoryBL.GetAllCategoriesUserAsync(userId)).convertListToDTO();
        }

        [HttpPost("AddNewCategory/{userId:int}")]
        public async Task<List<Category>> AddNewCategory(int userId,[FromBody] Category category)
        {
            await categoryBL.AddNewCategoryAsync(category.CategoryName,userId);
            return await categoryBL.GetAllCategoriesUserAsync(userId);
        }
    }
}
