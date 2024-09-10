using Microsoft.EntityFrameworkCore;
using TaskManagment.AppServices.Employees;

namespace TaskManagment.Entities
{
    public class TasksDbContext: DbContext
    {

        // when  register TasksDbContext send (i.e  Provider ,  connection string)

        public TasksDbContext(DbContextOptions<TasksDbContext> option):base(option) { }
        

        //  Register entities
        //  table projects
        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get;  set; }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // don't delete call  base class method
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Email="atallah.esaied@gmail.com", Id=1, IsActive=true, JobTitle="Trainer", Name="Atallah Esaied", AssignDate=new DateTime(2008, 1,1), PhoneNumber="059271035"  },
                new Employee() { Email = "mahdi@gmail.com", Id = 2, IsActive = true, JobTitle = "Manager", Name = "Mahdi Turkman", AssignDate = new DateTime(2008, 1, 1), PhoneNumber = "059271035" }

                );


        }
       
    }
}
