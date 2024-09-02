using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Models
{
    public class CreateTaskModel
    {
        [Required]
        [StringLength(200)]//  nvarchar(max=4000)
        public string? Title { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }


        public DateTime? DueDate { get; set; }
        // 
        public IFormFile Attachment { get; set; }

      
        public int ProjectId { get; set; }
    }
}
