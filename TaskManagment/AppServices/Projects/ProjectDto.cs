using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaskManagment.AppServices.Projects
{
    public class ProjectDto
    {
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:00}")]
        public int Id { get; set; }

        // nvarchar(max) --sqlserver  max=4000
        [StringLength(200)]  // nvarchar(200)
        [Required] //  not null 
        [DisplayName("Project Name")]
        public required string Name { get; set; }
    }
}
