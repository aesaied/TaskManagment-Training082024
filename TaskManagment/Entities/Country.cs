using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string? Name { get; set; }
    }
}
