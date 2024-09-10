using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TaskManagment.AppServices.Employees;
using TaskManagment.Models;

namespace TaskManagment.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeAppService _employeeAppService) : ControllerBase
    {

        //  api/employees
        [HttpPost]
        public async Task<ActionResult<PageResult<EmployeeDto>>> GetAll([FromForm] DataTableFilter input)
        {
            return Ok(await _employeeAppService.GetAll(input));
        }

        [HttpPost("create")]
        // api/Employees/create
        public async Task<IActionResult> Create(CreateEmployeeDto input)
        {
            if (ModelState.IsValid)
            {
                bool result = await _employeeAppService.Create(input);

                if (result)
                {
                    return Created();
                }

                return BadRequest("Unable to serve your request!");
            }

            return BadRequest(ModelState);
        }
    }
}
