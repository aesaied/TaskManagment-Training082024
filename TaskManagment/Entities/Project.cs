using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagment.Entities
{
    public class Project
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [DisplayFormat(ApplyFormatInEditMode =false, DataFormatString ="{0:00}")]
        public int Id { get; set; }

        // nvarchar(max) --sqlserver  max=4000
        [StringLength(200)]  // nvarchar(200)
        [Required] //  not null 
        [DisplayName("Project Name")]
        public required string Name { get; set; }



        public ICollection<Task>? Tasks { get; set; }
        
       // public string? Description {  get; set; }
    }
}
