using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Entities
{
    public  class Attachment
    {

        public Guid Id { get; set; }

        [StringLength(300)]
        [Required]
        public string? OrginalName { get; set; }

        [Required]
        public long ContentLength { get; set; }

        [Required]
        public string? Path { get; set; }
    }
}
