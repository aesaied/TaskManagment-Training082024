using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(50)]
        public required string JobTitle { get; set; }

        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        [StringLength(50)]
        public string? PhoneNumber { get; set; }

        public DateTime AssignDate { get; set; }

        public bool IsActive { get; set; }
    }
}
