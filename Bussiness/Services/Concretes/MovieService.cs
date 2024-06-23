using Bussiness.Exceptions;
using Bussiness.Extensions;
using Bussiness.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services.Concretes
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IWebHostEnvironment _env;

        public MovieService(IMovieRepository movieRepository, IWebHostEnvironment env)
        {
            _movieRepository = movieRepository;
            _env = env;
        }
        public async Task AddMovie(Movie movie)
        {
            if (movie.ImageFile == null)
                throw new Exceptions.FileNotFoundExxception("The file cannot be empty!");
            movie.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads/movies", movie.ImageFile);
            await _movieRepository.AddAsync(movie);
            await _movieRepository.CommitAsync();

        }

        public void DeleteMovie(int id)
        {
            var existMovie = _movieRepository.Get(x => x.Id == id);
            if (existMovie == null)
                throw new EntityNotFoundException("Movie not found!");
            Helper.DeleteFile(_env.WebRootPath, @"uploads\movies", existMovie.ImageUrl);

            _movieRepository.Delete(existMovie);
            _movieRepository.Commit();
        }

        public List<Movie> GetAllMovies(Func<Movie, bool>? predicate = null)
        {
             return _movieRepository.GetAll(predicate);
        }

        public Movie GetMovie(Func<Movie, bool>? predicate = null)
        {
            return _movieRepository.Get(predicate);
        }

        public void UpdateMovie(Movie newMovie, int id)
        {
            var oldMovie =_movieRepository.Get(x=>x.Id == id);
            if (oldMovie == null)
                throw new EntityNotFoundException("Movie not found!");
            if(newMovie != null)
            {
                Helper.DeleteFile(_env.WebRootPath,@"uploads\movies",oldMovie.ImageUrl);
                oldMovie.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\movies", newMovie.ImageFile);
            }

            _movieRepository.Commit();
        }
    }
}
