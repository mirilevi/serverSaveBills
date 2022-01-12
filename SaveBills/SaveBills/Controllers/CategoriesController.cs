using Bal.Interfaces;
using Dal.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<Category>> GetAllCategories()
        {
            return await categoryBL.GetAllCategoriesAsync();
        }
        [HttpGet("GetAllCategoriesUser/{userId:int}")]
        public async Task<List<Category>> GetAllCategoriesUser(int userId)
        {
            return await categoryBL.GetAllCategoriesUserAsync(userId);
        }
        //TODO:!!{:?}
        //[HttpPost("AddNewCategory/{category}")]
        [HttpPost]
        public async Task<List<Category>> AddCategory([FromBody] string category)
        {
             await categoryBL.AddNewCategoryAsync(category);
            return await categoryBL.GetAllCategoriesAsync();
        }
    }
}
