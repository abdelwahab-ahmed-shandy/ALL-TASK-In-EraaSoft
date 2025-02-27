using Microsoft.AspNetCore.Mvc;

namespace Quick_Tickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovieRepository movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            var movieView = movieRepository.Get().ToList();
            return View(movieView);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieRepository.Create(movie);

                movieRepository.Commit();

                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }
    }
}
