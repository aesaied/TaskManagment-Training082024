using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using TaskManagment.Entities;
using TaskManagment.Models;

namespace TaskManagment.AppServices.Security
{
    public class AccountAppService(UserManager<AppUser> userManager) : IAccountAppService
    {

        public async Task<ResultDto> Register(RegisterViewModel input)
        {
            AppUser user = new AppUser() { Email = input.EmailAddress, UserName = input.EmailAddress, FullName = input.FullName, CountryId = input.CountryId };

            var result = await userManager.CreateAsync(user, input.Password);




            if (result.Succeeded)
            {

                return ResultDto.Ok;
            }
            else if (result.Errors.Any())
            {

                return new ResultDto() { Errors = result.Errors.Select(s => s.Description).ToArray() };
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError("", error.Description);

                //}
            }
            else
            {
                return new ResultDto() { Errors = ["Unable to create user , Please try again"] };
            }
        }


    }
}
