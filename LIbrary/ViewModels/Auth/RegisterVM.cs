namespace LIbrary.ViewModels.Auth
{
    public class RegisterVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public string imageUrl { get; set; }
        public bool rememberMe { get; set; }
    }
}
