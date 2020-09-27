using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreak.Controllers
{
    [Authorize(Policy = "admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
