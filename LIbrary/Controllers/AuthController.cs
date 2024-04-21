using LIbrary.Data;
using LIbrary.Models;
using LIbrary.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LIbrary.Controllers
{
    public class AuthController: Controller
    {
        private readonly UserManager<Reader> _userManager;
        private readonly SignInManager<Reader> _signInManager;
        public AuthController(UserManager<Reader> userManager,SignInManager<Reader> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Login()
        {
            var loginvm = new LoginVM();
            return View(loginvm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginvm)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with the provided login model if validation fails
                return View(loginvm);
            }

            var user = await _userManager.FindByEmailAsync(loginvm.Email);
            if (user == null)
            {
                // Show error message if user is not found
                ViewBag.ErrorMessage = "Wrong Credentials! Try Again";
                return View(loginvm);
            }

            // Check the password
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginvm.Password);
            if (!passwordCheck)
            {
                // Show error message if password is incorrect
                ViewBag.ErrorMessage = "Wrong Credentials! Try Again";
                return View(loginvm);
            }

            // Sign in the user if both email and password are correct
            var result = await _signInManager.PasswordSignInAsync(user, loginvm.Password, loginvm.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            // Show error message if sign-in fails for any reason
            ViewBag.ErrorMessage = "An error occurred while trying to sign in. Please try again later.";
            return View(loginvm);
        }
        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registervm)
        {
            if (!ModelState.IsValid)
            {
                return View(registervm);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(registervm.Email);
                if (user != null)
                {
                    ViewBag.ErrorMessage = "This Email Address has already been taken!";
                    return View(registervm);
                }
                else
                {
                    if (registervm.Password != registervm.ConfirmationPassword)
                    {
                        ViewBag.ErrorMessage = "Password and Confirmation Password are not the same!";
                        return View(registervm);
                    }
                    else
                    {
                        var newUser = new Reader()
                        {
                            Name = registervm.Name,
                            UserName = registervm.Email,
                            Email = registervm.Email
                        };
                        var newUserResponse = await _userManager.CreateAsync(newUser, registervm.Password);
                        if (newUserResponse.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, UserRoles.Reader);
                            var result = await _signInManager.PasswordSignInAsync(newUser, registervm.Password, registervm.RememberMe, false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "There has been an error, please try again!";
                                return View(registervm);
                            }
                        }
                        else
                        {
                            return View(registervm);
                        }
                    }
                }
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
