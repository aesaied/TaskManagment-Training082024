using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagment.Entities;
using TaskManagment.Models;
using System.Linq.Dynamic.Core;
using TaskManagment.AppServices.Tasks;
using TaskManagment.AppServices.Projects;
using TaskManagment.Filters;
using Microsoft.AspNetCore.Authorization;


namespace TaskManagment.Controllers
{ // [Authorize(Roles="Admins,Users")  ==> OR
  // [Authorize(Roles =$"{SystemRoles.Admins},{SystemRoles.Users}")]
  //[Authorize(Roles =SystemRoles.Admins)][

    [Authorize(Roles =SystemRoles.Admins), Authorize("Palestine")]
 

    public class TasksController : Controller
    {
       
        private readonly ITasksAppService _tasksAppService;
        private readonly IProjectAppService _projectAppService;
        public TasksController(ITasksAppService tasksAppService, IProjectAppService projectAppService)
        {
           _tasksAppService = tasksAppService;
            _projectAppService = projectAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize("AdminOnly")]
        public async Task<IActionResult> Create()
        {
         
            await FillLookups();

            return View();
        }


       
        private async System.Threading.Tasks.Task FillLookups()
        {
            var projects = await _projectAppService.GetAll();
            //  SelectList

            var projectSelectList = new SelectList(projects, nameof(Project.Id), nameof(Project.Name));

            ViewBag.Projects = projectSelectList;
        }


        [HttpPost]
        [Authorize("AdminOnly")]
        public async Task<IActionResult> Create(CreateTaskModel input)
        {
            //
            if (ModelState.IsValid)
            {


              await _tasksAppService.Create(input);


                return RedirectToAction("Index");





            }


           await FillLookups();


            return View(input);


        }

        //list-json

        [ActionName("list-json")]
        public async Task<IActionResult> GetJson([FromServices] IMapper mapper, DataTableFilter filter)
        {

            return Json(await _tasksAppService.GetAll(filter));

           
        }


        public async Task<IActionResult> Edit(int id)
        {

            return View();

        }

        public async Task<IActionResult> View(int id)
        {

            return View();

        }
    }
}
