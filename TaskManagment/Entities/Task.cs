using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagment.Entities
{
    public class Task
    {

        public int Id { get; set; }

        [Required]
        [StringLength(200)]//  nvarchar(max=4000)
        public string? Title { get; set; }

        [Required]
        [Column(TypeName ="ntext")]
        public string? Description { get; set; }


        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }  


        public TaskStatus CurrentStatus { get; set; }

        public Guid AttachmentId { get; set; }

        [ForeignKey(nameof(AttachmentId))]
        public Attachment? Attachment { get; set; }

       public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
        // 
       // public string 


    }
}
