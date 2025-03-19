using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieMarket.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieMarket.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SettingsController(UserManager<ApplicationUser> userManager,
                                  RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        #region Profile

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (_userManager == null)
            {
                return RedirectToAction("InternalServerError", "Account", new { area = "Identity" });
            }
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var profile = new ProfileVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                UserName = user.UserName
            };

            return View(profile);
        }
        #endregion


        #region Manage Profile

        // Display the edit profile page
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            // Get the current user's data
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Redirect to the "Not Found" page if the user is not found
                return RedirectToAction("NotFound", "Home", new { area = "Customer" });
            }

            // Fill the edit profile form with the current user's data
            var profile = new ManageProfileVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
            };
            return View(profile);
        }


        #endregion

    }
}
