using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagment.AppServices.Employees;
using TaskManagment.AppServices.Projects;
using TaskManagment.AppServices.Security;
using TaskManagment.AppServices.Tasks;
using TaskManagment.Entities;
using TaskManagment.Hubs;
using TaskManagment.Services;

namespace TaskManagment
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // add all required services  for signalR
            builder.Services.AddSignalR();

            // Add services to the container.
            builder.Services.AddControllersWithViews(options => {


                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();


             

                // Prevent access the system by not loged in users
                options.Filters.Add(new AuthorizeFilter(policy));

            });


           builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>,
    AdditionalUserClaimsPrincipalFactory>();


            var adminPolicy = new AuthorizationPolicyBuilder()
                .RequireRole(SystemRoles.Admins).Build();


            builder.Services.AddAuthorization(options =>
            {

                options.AddPolicy("AdminOnly", adminPolicy);

                options.AddPolicy("PAGE_TASKS", policy => policy.RequireAuthenticatedUser().RequireClaim("Pages", "Tasks"));
                options.AddPolicy("PAGE_PROJECTS", policy => policy.RequireAuthenticatedUser().RequireClaim("Pages", "Projects"));

                options.AddPolicy("Palestine", policy => policy.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Country, "1"));


            });


            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ITasksAppService, TasksAppService>();
            builder.Services.AddScoped<IProjectAppService, ProjectAppService>();
            builder.Services.AddScoped<IEmployeeAppService, EmployeeAppService>();
            builder.Services.AddScoped<IAccountAppService, AccountAppService>();    

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


        
            using(var  scope = app.Services.CreateScope())
            {
              var  roleManager=  scope.ServiceProvider.GetService<RoleManager<AppRole>>();




                var createRoles = (string roleName) =>
                {

                    bool result = roleManager.RoleExistsAsync(roleName).Result;
                    if (!result)
                    {
                        _ = roleManager.CreateAsync(new AppRole() { Name = roleName }).Result;
                    }
                };



                createRoles(SystemRoles.Admins);
                createRoles(SystemRoles.Users);



                var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();

                var user = userManager.FindByEmailAsync("admin@experts.ps").Result;
                if(user != null && !userManager.IsInRoleAsync(user,"ADMINS").Result)
                {
                  _=  userManager.AddToRoleAsync(user, "ADMINS").Result;
                }




                // var dbContext=  scope.ServiceProvider.GetService<TasksDbContext>();


            }

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
            app.MapHub<ChatHub>("/chathub");
            app.MapHub<NotificationHub>("/notification");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
