using Microsoft.AspNetCore.Mvc;

namespace Quick_Tickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovieRepository movieRepository;
        private readonly ICinemaRepository cinemaRepository;
        private readonly ICategoryRepository categoryRepository;

        public MoviesController(IMovieRepository movieRepository, ICategoryRepository categoryRepository, ICinemaRepository cinemaRepository)
        {
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var movieView = movieRepository.Get().ToList();
            return View(movieView);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cinemas = new SelectList(cinemaRepository.Get(), "Id", "Name");
            ViewBag.Categories = new SelectList(categoryRepository.Get(), "Id", "Name");

            return View();
        }


        // todo : Here 
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
