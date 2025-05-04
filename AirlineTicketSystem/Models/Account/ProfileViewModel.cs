using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirlineTicketSystem.Models.Account
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я0-9\s]+$", ErrorMessage = "The Name field can only contain letters, digits, and spaces.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public required string Email { get; set; }

        [ValidateNever]
        public IList<String> Roles { get; set; }
    }
}
