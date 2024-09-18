using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using TaskManagment.AppServices.Security;
using TaskManagment.Models;

namespace TaskManagment.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountAppService accountAppService) : ControllerBase
    {

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterViewModel input)
        {
           

            if (ModelState.IsValid)
            {


                var result = await accountAppService.Register(input);
                if (result.Success)
                {

                    return Ok();
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

            return BadRequest(ModelState);

        }

    }
}
