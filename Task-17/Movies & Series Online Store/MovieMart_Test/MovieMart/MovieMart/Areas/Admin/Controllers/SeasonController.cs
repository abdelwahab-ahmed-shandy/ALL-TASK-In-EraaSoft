using Microsoft.AspNetCore.Mvc;
using MovieMart.Models;

namespace MovieMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeasonController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly ITvSeriesRepository _tvSeriesRepository;
        public SeasonController(ISeasonRepository seasonRepository, ITvSeriesRepository tvSeriesRepository)
        {
            this._seasonRepository = seasonRepository;
            _tvSeriesRepository = tvSeriesRepository;
        }
        public IActionResult Index()
        {
            var season = _seasonRepository.Get()
                .Include(s => s.TvSeries)
                .ToList();
            return View(season);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.TvSeries = new SelectList(_tvSeriesRepository.Get(), "Id", "Title");
            return View(new Season());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Season season)
        {
            if (ModelState.IsValid)
            {
                _seasonRepository.Create(season);
                _seasonRepository.SaveDB();
                return RedirectToAction(nameof(Index));
            }
            return View(season);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var season = _seasonRepository.GetOne(s => s.Id == Id);
            if (season != null)
            {
                ViewBag.TvSeries = new SelectList(_tvSeriesRepository.Get(), "Id", "Title", season.TvSeriesId);
                return View(season);
            }
            return RedirectToAction("NotFound", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Season season)
        {
            if (season != null || ModelState.IsValid)
            {
                _seasonRepository.Edit(season);
                _seasonRepository.SaveDB();
                ViewBag.TvSeries = new SelectList(_tvSeriesRepository.Get(), "Id", "Title");
                return RedirectToAction(nameof(Index));
            }
            return View(season);
        }
        public IActionResult Delete(int Id)
        {
            var season = _seasonRepository.GetOne(s => s.Id == Id);

            if (season == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            _seasonRepository.Delete(season);
            _seasonRepository.SaveDB();
            return RedirectToAction(nameof(Index));
        }
    }
}
