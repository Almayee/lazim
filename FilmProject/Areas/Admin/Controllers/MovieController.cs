using Bussiness.Exceptions;
using Bussiness.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace FilmProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService; 
        }
        public IActionResult Index()
        {
            var movies= _movieService.GetAllMovies();
            return View(movies);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _movieService.AddMovie(movie);
            }
            catch (ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileNullReferenceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var existMovie = _movieService.GetMovie(x => x.Id == id);
            if (existMovie == null) 
                return View("Error");
            return View(existMovie);
        }
        [HttpPost]
        public IActionResult Update(Movie movie)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _movieService.UpdateMovie(movie, movie.Id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound("Error");
            }
            catch (ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Bussiness.Exceptions.FileNotFoundExxception ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var existMovie = _movieService.GetMovie(x => x.Id == id);
            if (existMovie == null) 
                return View("Error");
            return View(existMovie);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {

            try
            {
                _movieService.DeleteMovie(id);
            }
            catch (EntityNotFoundException ex)
            {
                return View("Error");
            }
            catch (Bussiness.Exceptions.FileNotFoundExxception ex)
            {
                return View("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
