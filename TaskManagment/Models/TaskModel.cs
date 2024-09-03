using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagment.Models
{
    public class TaskModel
    {

        public int Id { get; set; }
        public string? Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }


        public TaskStatus CurrentStatus { get; set; }

       
       public bool HasAttachment {get; set; }

        public int ProjectId { get; set; }

       public string? ProjectName { get; set; }
    }
}
