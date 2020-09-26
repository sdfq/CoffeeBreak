using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreak.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// If user is authenticated display priceList for coffee and tea 
        /// If user not authenicated redirect to account/login
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
