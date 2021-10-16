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
        public async Task<List<Category>> GetAllBills()
        {

            return await categoryBL.GetAllCategoriesAsync();
        }
    }
}
