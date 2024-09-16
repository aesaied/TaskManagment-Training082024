using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Entities;
using TaskManagment.Models;

namespace TaskManagment.Controllers
{
    public class AccountController(SignInManager<AppUser> _signInManager) : Controller
    {


        public IActionResult Index()
        {
            return RedirectToActionPermanent("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]  
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
            var  result =   await  _signInManager.PasswordSignInAsync(input.Email, input.Password,input.RememberMe,false);

                if (result.Succeeded) {

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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel input, [FromServices] UserManager<AppUser> userManager)
        {
            if (ModelState.IsValid) {

                AppUser user = new AppUser() { Email = input.EmailAddress, UserName = input.EmailAddress };

             var  result =  await  userManager.CreateAsync(user, input.Password);


            

                if (result.Succeeded)
                {

                    return RedirectToAction(nameof(Login));
                }
                else if (result.Errors.Any()) {

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Unable to create user , Please try again");
                }

            }


            return View(input);
        }

    }
}
