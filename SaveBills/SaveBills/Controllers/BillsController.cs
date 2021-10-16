using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Bal.Interfaces;
using Dal.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace SeveBills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : Controller
    {

        IBillBL billBL;
        public BillsController(IBillBL _billBL)
        {
            billBL = _billBL;
        }

        //GET api/values/5
        [HttpGet("GetAllBills/{userId:int}")]
        public async Task<List<Bill>> GetAllBills(int userId)
        {

            return await billBL.GetAllBiils(userId);
        }

        [HttpGet("GetBillsByCategory/{userId:int}/{categoryId:int}")]
        public async Task<List<Bill>> GetBillsByCategory(int userId, int categoryId)
        {

            return await billBL.GetBillsByCategory(userId, categoryId);
        }

        [HttpGet("GetBillsByStoreName/{userId:int}/{storeName}")]
        public async Task<List<Bill>> GetBillsByStoreName(int userId, string storeName)
        {

            return await billBL.GetBillsByStoreName(userId, storeName);
        }
        // POST api/values
        [HttpPost]
        public async void Post([FromBody] Bill bill)
        {
            await billBL.AddBill(bill);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] Bill bill)
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
        public async Task<Bill> GetBillFromFile(string imageName,string token)
        {
            //string path = "https://firebasestorage.googleapis.com/v0/b/savebills-66d22.appspot.com/o/bills%2F"+ imageName + "&" + token;
            string path = @"E:\תכנות\final project\bills\bill examples\IMG\1.jpg";
            Bill b = new Bill(path);
            return b;
        }
    }
}
