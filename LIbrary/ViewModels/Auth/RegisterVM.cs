using System.ComponentModel.DataAnnotations;

namespace LIbrary.ViewModels.Auth
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match")]
        [DataType(DataType.Password)]
        public string ConfirmationPassword { get; set; }

        [Url(ErrorMessage = "Invalid Image URL")]
        public string? ImageUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
