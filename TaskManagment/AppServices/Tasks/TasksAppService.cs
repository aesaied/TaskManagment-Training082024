using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TaskManagment.Entities;
using TaskManagment.Models;
using System.Linq.Dynamic.Core;
using AutoMapper.QueryableExtensions;

namespace TaskManagment.AppServices.Tasks
{
    public class TasksAppService(TasksDbContext _dbContext, IWebHostEnvironment _webHostEnvironment, IMapper mapper) : ITasksAppService
    {


        public async Task<bool> Create(CreateTaskModel input)
        {
            var task = new ETask();

            task.Title = input.Title;
            task.Description = input.Description;
            task.DueDate = input.DueDate;
            task.CreatedDate = DateTime.Now;
            task.ProjectId = input.ProjectId;
            task.CurrentStatus = Entities.TaskStatus.New;
            task.Attachment = new Attachment();
            task.Attachment.OrginalName = input.Attachment.FileName;
            task.Attachment.ContentLength = input.Attachment.Length;

            //1)  Place to  save file
            string basePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "Attachments");
            //2) Generate random name
            string newName = System.IO.Path.GetRandomFileName();
            string extension = System.IO.Path.GetExtension(input.Attachment.FileName);

            var FileFullPath = System.IO.Path.Combine(basePath, newName + extension);



            //3) Save file 

            MemoryStream stream = new MemoryStream();
            input.Attachment.CopyTo(stream);

            System.IO.File.WriteAllBytes(FileFullPath, stream.ToArray());


            task.Attachment.Path = FileFullPath;


            _dbContext.Tasks.Add(task);
            return await _dbContext.SaveChangesAsync() > 0;
        }



        public async Task<PageResult<TaskModel>> GetAll(DataTableFilter filter)
        {
            var tasks = _dbContext.Tasks
               //.Select(task => new TaskModel() { Id=task.Id, Title=task.Title })
               .AsQueryable();
            //.ToListAsync();

            if (string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                filter.OrderBy = nameof(ETask.Title);
                filter.OrderDir = "asc";
            }

            int recordsTotal = await tasks.CountAsync();

            if (!string.IsNullOrWhiteSpace(filter.Filter))
            {
                var trimFilter = filter.Filter.Trim();

                tasks = tasks.Where(s => s.Description.Contains(trimFilter) || s.Project.Name.Contains(trimFilter) || s.Title.Contains(trimFilter));

            }

            int filteredRecords = await tasks.CountAsync();

            var result = await tasks.OrderBy($"{filter.OrderBy} {filter.OrderDir}").Skip(filter.Start).Take(filter.Length).ProjectTo<TaskModel>(mapper.ConfigurationProvider).ToListAsync();


            return new PageResult<TaskModel> { RecordsTotal = recordsTotal, RecordsFiltered = filteredRecords, Data = result };


        }



    }
}
