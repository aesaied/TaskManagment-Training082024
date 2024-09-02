using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagment.Entities;
using TaskManagment.Models;


namespace TaskManagment.Controllers
{
    public class TasksController : Controller
    {
        private readonly TasksDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TasksController(TasksDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {
            // 
            var projects =await _dbContext.Projects.ToListAsync() ;

            //  SelectList

            var  projectSelectList = new SelectList(projects,nameof(Project.Id), nameof(Project.Name));

            ViewBag.Projects = projectSelectList;


            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Create(CreateTaskModel input)
        {
            //
            if (ModelState.IsValid)
            {


                var task = new ETask();

                task.Title = input.Title;
                task.Description = input.Description;
                task.DueDate = input.DueDate;
                task.CreatedDate= DateTime.Now;
                task.ProjectId = input.ProjectId;
                task.CurrentStatus = Entities.TaskStatus.New;
                task.Attachment = new Attachment();
                task.Attachment.OrginalName=input.Attachment.FileName;
                task.Attachment.ContentLength=input.Attachment.Length;
                
               //1)  Place to  save file
               string basePath  = System.IO.Path.Combine( _webHostEnvironment.ContentRootPath, "Attachments");
               //2) Generate random name
               string newName=System.IO.Path.GetRandomFileName();
                string extension = System.IO.Path.GetExtension(input.Attachment.FileName);

                var FileFullPath = System.IO.Path.Combine(basePath,newName+extension);



               //3) Save file 

                MemoryStream stream = new MemoryStream();
                input.Attachment.CopyTo(stream);

                System.IO.File.WriteAllBytes(FileFullPath, stream.ToArray());


                task.Attachment.Path= FileFullPath; 


                _dbContext.Tasks.Add(task);
                await _dbContext.SaveChangesAsync();


                return RedirectToAction("Index");


             // System.IO.File.w
               // input.Attachment.co



            }


            var projects = await _dbContext.Projects.ToListAsync();

            var projectSelectList = new SelectList(projects, nameof(Project.Id), nameof(Project.Name));

            ViewBag.Projects = projectSelectList;


            return View(input);


        }
    }
}
