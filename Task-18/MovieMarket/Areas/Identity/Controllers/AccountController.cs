using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MovieMarket.Models.ViewModels;

namespace MovieMarket.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        #region Register

        // Display the registration page when requested via HTTP GET
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            // Check if roles do not exist and create them if they do
            if (_roleManager.Roles.IsNullOrEmpty())
            {
                await _roleManager.CreateAsync(role: new IdentityRole("SuperAdmin")); // Create the "SuperAdmin" role
                await _roleManager.CreateAsync(role: new IdentityRole("Admin")); // Create the "Admin" role
                await _roleManager.CreateAsync(role: new IdentityRole("Customer")); // Create the "Customer" role
            }

            return View(); // Display the registration page
        }

        // Process registration data when sent via HTTP POST
        [HttpPost]
        [ValidateAntiForgeryToken] // Protection against CSRF attacks
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            // Validate the entered data
            if (ModelState.IsValid)
            {
                // Create a new user object from the entered data
                ApplicationUser applicationUser = new ApplicationUser
                {
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Address = registerVM.Address,
                    UserName = registerVM.UserName,
                    Email = registerVM.Email
                };

                // Create a user account in the database
                var newUser = await _userManager.CreateAsync(applicationUser, registerVM.Password);

                if (newUser.Succeeded)
                {
                    // Automatically assign the "Customer" role to each new user
                    await _userManager.AddToRoleAsync(applicationUser, "Customer");

                    // Automatically log the user in after successful registration
                    await _signInManager.SignInAsync(applicationUser, isPersistent: false);

                    // Redirect the user to the home page in the "Customer" area
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
                else
                {
                    // If registration fails, display errors in the form
                    foreach (var error in newUser.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // Re-display the registration page with the entered data if errors are found
            return View(registerVM);
        }

        #endregion


        #region Login

        // Display the login page when requested via HTTP GET
        [HttpGet]

        public async Task<IActionResult> Login()
        {
            return View(); // Display the login interface
        }

        // Process login data when sent via HTTP POST
        [HttpPost]
        [ValidateAntiForgeryToken] // Protection against CSRF attacks
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            // Validate the entered data
            if (ModelState.IsValid)
            {
                // Find the user using email
                var user = await _userManager.FindByEmailAsync(loginVM.Email);

                if (user != null)
                {
                    // Validate the entered password
                    var checkPassByUser = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                    if (checkPassByUser)
                    {
                        // User login successfully
                        await _signInManager.SignInAsync(user, loginVM.RememberMe);

                        // Redirect the user to the home page in the "Customer" area
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                    else
                    {
                        // Add an error message if the password does not match
                        ModelState.AddModelError("Password", "Invalid password");
                        ModelState.AddModelError("Email", "Email not found");
                    }
                }
                else
                {
                    // Add an error message if the email address is not found
                    ModelState.AddModelError("Password", "Invalid password");
                    ModelState.AddModelError("Email", "Email not found");
                }
            }

            // Re-display the login page with the entered data in case of errors
            return View(loginVM);
        }

        #endregion


        #region LogOut

        // Log the user out
        public async Task<IActionResult> Logout()
        {
            // Perform the logout operation
            await _signInManager.SignOutAsync();

            // Redirect the user to the login page
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        #endregion


        #region AccDenied, NotFound, and IntSerErr

        // Display the "Access Denied" page when attempting to access an unauthorized resource
        public IActionResult AccessDenied() => View();
        // Display the "Not Found" page when attempting to access a non-existent page
        public IActionResult NotFound() => View();
        // Display the "Internal Server Error" page when an internal server error occurs
        public IActionResult InternalServerError() => View();

        #endregion
    }
}
