using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        [EmailAddress]
        [Required]
        public string? EmailAddress { get; set; }


        [Required]
       [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password),ErrorMessage ="Confirm password doesn't match password!!")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="Country")]
        public int CountryId { get; set; }
    }
}
