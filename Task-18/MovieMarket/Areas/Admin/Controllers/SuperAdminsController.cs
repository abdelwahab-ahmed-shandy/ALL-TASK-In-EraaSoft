using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieMarket.Repositories.IRepositories;

namespace MovieMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuberAdmin")]
    public class SuperAdminsController : Controller
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuperAdminsController(IApplicationUserRepository applicationUserRepository, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._applicationUserRepository = applicationUserRepository;
        }

        public async Task<IActionResult> AllSuperAdmins()
        {
            var users = _applicationUserRepository.Get().ToList();
            var alldmins = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("SuberAdmin"))
                {
                    alldmins.Add(user);
                }
            }

            return View(alldmins);
        }
    }
}
