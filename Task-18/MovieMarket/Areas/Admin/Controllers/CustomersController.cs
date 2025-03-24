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
        public async Task<IActionResult> AllCustomers(string? query, int page = 1)
        {

            IEnumerable<ApplicationUser> allUsers = _applicationUserRepository.Get().AsNoTracking().ToList();

            var allCustomer = new List<ApplicationUser>();

            foreach (var user in allUsers)
            {
                var role = await _userManager.GetRolesAsync(user);

                if (!role.Contains("Admin") && !role.Contains("SuberAdmin") && role.Contains("Customer"))
                {
                    allCustomer.Add(user);
                }
            }

            if (query != null)
            {
                allCustomer = allCustomer.Where(e => (e.UserName ?? "").Contains(query)
                                                          || (e.Email ?? "").Contains(query)
                                                          || (e.FirstName ?? "").Contains(query)
                                                          || (e.LastName ?? "").Contains(query)
                                                          || (e.Address ?? "").Contains(query)
                                                          || (e.Id ?? "").Contains(query))
                                                          .ToList();
            }
            // todo: here a page Numer :: 52
            // allCustomer = await allCustomer.Skip((page - 1) * 5).Take(count: 5);

            return View(allCustomer.ToList());
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

                // Assign the "Customer" role to the new user
                await _userManager.AddToRoleAsync(applicationUser, "Customer");

                //Save changes to the database
                _applicationUserRepository.SaveDB();

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

        // todo:here......
        // #region Delete Customer Account :

        //[HttpDelete"{id}"]
        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var userDelete = await _applicationUserRepository.Get(u => u.Id == Id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _applicationUserRepository.Delete(userDelete);
        //    _applicationUserRepository.SaveDB();

        //    return RedirectToAction("AllCustomers", "Customers", new { area = "Admin" });
        //}

        //#endregion

    }
}
