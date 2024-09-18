using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagment.Entities
{
    public class AppUser:IdentityUser
    {
        [StringLength(100)]
        [Required]
        public string? FullName { get; set; }


        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country? Country { get; set; }


    }
}
