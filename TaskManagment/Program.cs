using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagment.AppServices.Employees;
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
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ITasksAppService, TasksAppService>();
            builder.Services.AddScoped<IProjectAppService, ProjectAppService>();
            builder.Services.AddScoped<IEmployeeAppService, EmployeeAppService>();

            //  register swagger services

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //  register TasksDbContext  as service



            var connStr = builder.Configuration.GetConnectionString("MyConnStr");
            builder.Services.AddDbContext<TasksDbContext>(options => {

                options.UseSqlServer(connStr);
            });

            //builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TasksDbContext>();


            builder.Services.AddIdentity<AppUser,AppRole>(
                options => {

                    //options.User.AllowedUserNameCharacters = "0123456789";
                
                  }
                )
                .AddEntityFrameworkStores<TasksDbContext>().AddDefaultTokenProviders()
                //.AddDefaultUI()
                ;


            builder.Services.ConfigureApplicationCookie(options => {
            
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/Account/login";
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

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // load user info
            app.UseAuthentication();

            app.UseRouting();

            //  check  if authorized
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
