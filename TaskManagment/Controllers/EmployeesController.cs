using Microsoft.AspNetCore.Mvc;
using TaskManagment.AppServices.Employees;

namespace TaskManagment.Controllers
{
    public class EmployeesController(IEmployeeAppService _employeeAppService) : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateEmployeeDto input)
        {
            if (ModelState.IsValid) {
               bool result= await _employeeAppService.Create(input);

                if (result)
                { 
                   return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Unable to  save data. Please check  your data and try again");
            }


            return View(input);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id) { 
        

            var emp  = await _employeeAppService.GetForEditById(id);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id,[FromForm] CreateEmployeeDto input)
        {

            if (id != input.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                bool result = await _employeeAppService.Update(input);

                if (result) { 
                return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Error Occurred ! please try again");

            }

            return View(input);
        }
    }
}
