using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagment.AppServices.Security;
using TaskManagment.Entities;
using TaskManagment.Models;

namespace TaskManagment.Controllers
{
    [AllowAnonymous]
    public class AccountController(SignInManager<AppUser> _signInManager, TasksDbContext dbContext) : Controller
    {


        public IActionResult Index()
        {
            return RedirectToActionPermanent("Login");
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {

         
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, false);

                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }

                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is locked please try  after  10 mins.");

                }

                else
                {
                    ModelState.AddModelError("", "Invalid username or password!");

                }


            }


            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await FillRegisterLookups();
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel input, [FromServices] IAccountAppService accountAppService )
        {
            if (ModelState.IsValid)
            {


                var result =await accountAppService.Register(input);
                if (result.Success)
                {


                    return RedirectToAction(nameof(Login));
                }
                else if (result.Errors.Any())
                {

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Unable to create user , Please try again");
                }

            }

            await FillRegisterLookups();

            return View(input);
        }


        private async System.Threading.Tasks.Task FillRegisterLookups()
        {
            var countries = await dbContext.Countries.ToListAsync();
            ViewBag.Countries = new SelectList(countries, nameof(Country.Id), nameof(Country.Name));
        }

    }
}