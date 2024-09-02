using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Mime;
using TaskManagment.Models;

namespace TaskManagment.Controllers
{
    public class TestController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public TestController(IWebHostEnvironment environment)
        {
            
            _webHostEnvironment = environment;

           

            
        }
        public IActionResult Index()
        {
           return Content("Welcome to  test  controller with hot reload");
        }


        public IActionResult TestView()
        {
            TestModel model = new TestModel() { Id=1,Name="Data from Model"};

            return View(model);  
        }


        public IActionResult PageNotFound() {
            // return  NotFound("Custom msg from not found!");
            return NotFound();
        }


        public IActionResult GetJsonFile()
        {
            return Json(new { id = 1, name = "atallah" });

        }


        public IActionResult GetFile(string fname)
        {
            string path = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "MyFiles", fname);
            FileInfo fileInfo = new FileInfo(path);

            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

            string contentType = "application/octet-stream";
           if(provider.TryGetContentType(fname, out var outContent))
            {
                contentType = outContent;
            }

            if (fileInfo.Exists)
            {

                return PhysicalFile(path, contentType);

               // return new PhysicalFileResult()

            }

            return NotFound();
        }


        public IActionResult Google()
        {


           // Request.
            return Redirect("https://www.google.com");

        }

        public IActionResult GoTo() { 
        
            return RedirectToAction("Index","Home");
        }
    }
}
