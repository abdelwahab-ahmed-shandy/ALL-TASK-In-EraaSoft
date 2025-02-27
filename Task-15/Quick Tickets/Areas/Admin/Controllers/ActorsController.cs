using Microsoft.AspNetCore.Mvc;

namespace Quick_Tickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorsController : Controller
    {


        private readonly IActorRepository actorRepository;
        private readonly IMovieRepository movieRepository;

        public ActorsController(IActorRepository actorRepository, IMovieRepository movieRepository)
        {
            this.actorRepository = actorRepository;
            this.movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            var viewActor = actorRepository.Get().ToList();
            return View(viewActor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Movies = movieRepository.Get().ToList(); // تمرير الأفلام إلى `ViewBag`
            return View(new Actor());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Actor actor, IFormFile file, List<int> Movies)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        var nameFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //        var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Assats", "image", "Custumer", "ActorProfilePicture", nameFile);

        //        using (var stream = new FileStream(pathFile, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //        }

        //        actor.ProfilePicture = nameFile;
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("file", "Please upload an image.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // حفظ الممثل أولاً
        //        actorRepository.Create(actor);
        //        actorRepository.Commit();

        //        // إضافة العلاقة بين الممثل والأفلام
        //        if (Movies != null && Movies.Count > 0)
        //        {
        //            foreach (var movieId in Movies)
        //            {
        //                var actorMovie = new ActorMovie
        //                {
        //                    ActorId = actor.Id,
        //                    MovieId = movieId
        //                };
        //                actorRepository(actorMovie);
        //            }
        //            actorRepository.Commit();
        //        }

        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Movies = movieRepository.Get().ToList(); // إعادة تحميل قائمة الأفلام عند حدوث خطأ
        //    return View(actor);
        //}

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var ActorEdit = actorRepository.GetOne(a => a.Id == Id);

            if (ActorEdit == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            return View(ActorEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Actor actor)
        {
            if (actor != null)
            {
                actorRepository.Edit(actor);
                actorRepository.Commit();

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("NotFoundPage", "Home");
        }


    }
}
