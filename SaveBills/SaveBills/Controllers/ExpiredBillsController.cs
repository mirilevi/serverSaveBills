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
    public class ExpiredBillsController : ControllerBase
    {
        IExpireBillsBL expireBillsBL;
        public ExpiredBillsController(IExpireBillsBL _expireBillsBL)
        {
            expireBillsBL = _expireBillsBL;
        }
        [HttpGet("GetAllExpireBills/{userId:int}")]
        public async Task<List<Bill>> GetAllExpireBills(int userId)
        {
            return await expireBillsBL.GetAllExpireBillsAsync(userId);
        }

    }
}
