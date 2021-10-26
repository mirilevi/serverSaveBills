using Bal.Interfaces;
using Dal.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SaveBills.Controllers
{
    //[Route("User")]
    [Route("api/[controller]")]

    public class UserController : Controller
    {
        IuserBL userBL;
        public UserController(IuserBL _userBL)
        {
            userBL = _userBL;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("{email}/{password}")]
        [Route("CheckLogin")]

        public async Task<User> CheckLogin(string email, string password)
        {
            return await userBL.CheckUser(email, password);
        }
        [HttpPost]
        public async Task<int> Post([FromBody] User user)
        {
            return await userBL.AddUser(user);
        }
       
    }
}
