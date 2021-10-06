using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bal.Interfaces;
using Dal.Classes;
using Microsoft.AspNetCore.Mvc;



namespace SeveBills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {

        IBillBL billBL;
        public BillsController(IBillBL _billBL)
        {
            billBL = _billBL;
        }
        // GET api/values
        
        //[HttpGet]
        //[Route("GetAllBills")]

        //public async Task<List<Bill>> GetAllBills()
        //{
        //    return await billBL.GetAllBiils();
        //}

        // GET api/values/5
        [HttpGet("{userId}")]
        [Route("GetAllBills")]
        public async Task<List<Bill>> Get(int userId)
        {
            return await billBL.GetAllBiils(userId);
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
    }
}
