using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieMarket.Models.ViewModels;
using MovieMarket.Repositories;
using MovieMarket.Repositories.IRepositories;
using MovieMart.Models;
using System.Linq.Expressions;

namespace MovieMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuberAdmin")]
    public class CustomersController : Controller
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomersController(IApplicationUserRepository applicationUserRepository, UserManager<ApplicationUser> userManager)
        {
            this._applicationUserRepository = applicationUserRepository;
            this._userManager = userManager;
        }

        #region View All Customers
        // Define an Action to retrieve all customers
        [HttpGet]
        public async Task<IActionResult> AllCustomers()
        {
            // Fetch all users from the database and convert them to a list
            var users = _applicationUserRepository.Get().ToList();

            // Create a list to store only customers (except administrators)
            var allCustomers = new List<ApplicationUser>();

            // Iterate through all users to check their roles
            foreach (var user in users)
            {
                // Fetch roles for the current user
                var roles = await _userManager.GetRolesAsync(user);

                // Add the user to the customer list if it is neither "Admin" nor "SuperAdmin"
                if (!roles.Contains("Admin") && !roles.Contains("SuberAdmin"))
                {
                    allCustomers.Add(user);
                }
            }

            // Pass the customer list to the View
            return View(allCustomers);
        }
        #endregion

        #region Create Customer

        // Display the new customer creation form
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Process a new customer creation request when the form is submitted
        [HttpPost]
        [ValidateAntiForgeryToken] // Protection against CSRF attacks
        public async Task<IActionResult> Create(ApplicationUser applicationUser)
        {
            // Validate the entered data before saving
            if (ModelState.IsValid)
            {
                // Add the new customer to the database
                _applicationUserRepository.Create(applicationUser);
                _applicationUserRepository.SaveDB(); // Save changes to the database

                // Store a success message in TempData to display after redirection
                TempData["notifiction"] = "The Customer was created successfully!";
                TempData["MessageType"] = "success";

                // Redirect to the All Customers view page
                return RedirectToAction(nameof(AllCustomers));
            }

            // If there is an input error, re-render the form with the same data
            return View(applicationUser);
        }

        #endregion

        #region Edit Customer (I stopped trying this action because I found it unreasonable for someone to modify someone else's file )

        //[HttpGet]
        //public IActionResult Edit(string Id)
        //{
        //    var user = _applicationUserRepository.GetOne(u => u.Id == Id);
        //    if (user != null)
        //    {
        //        var userEdit = new ManageProfileVM
        //        {
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            Address = user.Address,
        //            UserName = user.UserName,
        //            Email = user.Email

        //        };
        //        return View(user);
        //    }
        //    return RedirectToAction("NotFound", "Account", new { area = "Identity" });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(ManageProfileVM model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = _applicationUserRepository.GetOne(u => u.Id == model.Id as string);
        //    if (user == null)
        //    {
        //        return RedirectToAction("NotFound", "Account", new { area = "Identity" });
        //    }

        //    user.FirstName = model.FirstName;
        //    user.LastName = model.LastName;
        //    user.UserName = model.UserName;
        //    user.Email = model.Email;
        //    user.Address = model.Address;

        //    _applicationUserRepository.Edit(user);
        //    _applicationUserRepository.SaveDB();

        //    TempData["notifiction"] = "Edit Customer Successfully!";
        //    TempData["MessageType"] = "Success";

        //    return RedirectToAction(nameof(Index));
        //}
        #endregion




    }
}
