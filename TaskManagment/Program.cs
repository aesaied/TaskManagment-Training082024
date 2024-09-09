using Microsoft.EntityFrameworkCore;
using TaskManagment.AppServices.Projects;
using TaskManagment.AppServices.Tasks;
using TaskManagment.Entities;

namespace TaskManagment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ITasksAppService, TasksAppService>();
            builder.Services.AddScoped<IProjectAppService, ProjectAppService>();


            //  register TasksDbContext  as service

            var connStr = builder.Configuration.GetConnectionString("MyConnStr");
            builder.Services.AddDbContext<TasksDbContext>(options => {

                options.UseSqlServer(connStr);
            });

        

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
