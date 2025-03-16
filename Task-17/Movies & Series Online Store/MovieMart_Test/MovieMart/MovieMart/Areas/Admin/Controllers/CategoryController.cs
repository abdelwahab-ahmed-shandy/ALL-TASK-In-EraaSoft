using Microsoft.AspNetCore.Mvc;
using MovieMart.Repositories;

namespace MovieMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var category = _categoryRepository.Get().ToList();

            return View(category);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Create(category);
                _categoryRepository.SaveDB();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = _categoryRepository.GetOne(c => c.Id == Id);

            if (category != null)
            {
                return View(category);
            }
            return RedirectToAction("NotFound", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category == null || !ModelState.IsValid)
            {
                return RedirectToAction("NotFound", "Home");
            }
            _categoryRepository.Edit(category);
            _categoryRepository.SaveDB();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            var category = _categoryRepository.GetOne(c => c.Id == Id);

            if (category == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            _categoryRepository.Delete(category);
            _categoryRepository.SaveDB();
            return RedirectToAction(nameof(Index));
        }
    }
}
