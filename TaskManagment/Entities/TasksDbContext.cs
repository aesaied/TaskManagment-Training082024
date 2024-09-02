using Microsoft.EntityFrameworkCore;

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
    }
}
