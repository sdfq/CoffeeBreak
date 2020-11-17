using CoffeeBreak.Data;
using CoffeeBreak.Entities;
using CoffeeBreak.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoffeeBreak.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь с таким логином не найден");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Логин или пароль неверный");
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            
            if (user != null)
            {
                ModelState.AddModelError("", "Такой пользователь уже зарегистрирован");
                return View(model);
            }

            var newUser = new ApplicationUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.CellPhone
            };
            
            var result = await userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, "customer"));
                await context.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Не удалось зарегистрировать пользователя, обатитесь к администратору");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }

        [Authorize(Policy = "admin")]
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Customer()
        {
            return View();
        }

    }
}
