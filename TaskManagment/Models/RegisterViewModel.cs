using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Models
{
    public class RegisterViewModel
    {

        [EmailAddress]
        [Required]
        public string? EmailAddress { get; set; }


        [Required]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password),ErrorMessage ="Confirm password doesn't match password!!")]
        public string? ConfirmPassword { get; set; }

    }
}
