using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Bal.Interfaces;
using Dal.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dal.Models;

namespace SeveBills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : Controller
    {

        IBillBL billBL;
        ICategoryBL categoryBL;
        public BillsController(IBillBL _billBL, ICategoryBL _categoryBL)
        {
            billBL = _billBL;
            categoryBL = _categoryBL;
        }

        //GET api/values/5
        [HttpGet("GetAllBills/{userId:int}")]
        public async Task<List<BillDTO>> GetAllBills(int userId)
        {

            return (await billBL.GetAllBiils(userId)).convertListToDTO();
        }

        [HttpGet("GetBillsByCategory/{userId:int}/{categoryId:int}")]
        public async Task<List<BillDTO>> GetBillsByCategory(int userId, int categoryId)
        {

            return (await billBL.GetBillsByCategory(userId, categoryId)).convertListToDTO();
        }

        [HttpGet("GetBillsByStoreName/{userId:int}/{storeName}")]
        public async Task<List<BillDTO>> GetBillsByStoreName(int userId, string storeName)
        {

            return (await billBL.GetBillsByStoreName(userId, storeName)).convertListToDTO();
        }
        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Bill bill)
        {
            await billBL.AddBill(bill);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Bill bill)
        {
            await billBL.UpdateBill(id,bill);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await billBL.DeleteBill(id);
        }

        ////TODO:
        [Route("GetBillFromFile")]
        public async Task<BillDTO> GetBillFromFile(string imageName,string token)
        {
            string path = "https://firebasestorage.googleapis.com/v0/b/savebills-66d22.appspot.com/o/bills%2F" + imageName + "&" + token;
            imageName = imageName.Substring(0, imageName.IndexOf("?"));//remove the parameters from the imageName
            List<Category> categories = await categoryBL.GetAllCategoriesAsync();
            List<string> stores = await billBL.GetAllStoresNames();
            Bill b = new Bill(path ,imageName, categories,stores); 
            return b.ConvertToDTO();
        }
    }
}