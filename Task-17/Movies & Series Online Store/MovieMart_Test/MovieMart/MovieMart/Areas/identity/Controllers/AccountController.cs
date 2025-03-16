using Microsoft.AspNetCore.Mvc;

namespace MovieMart.Areas.identity.Controllers
{
    [Area(areaName: "identity")]
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
