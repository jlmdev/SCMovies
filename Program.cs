using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SCMovies
{
    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryDirector { get; set; }
        public int YearReleased { get; set; }
        public string Genre { get; set; }

        // This is the column in the database
        public int RatingId { get; set; }

        // This is the related object we can use from our code if properly used with Include
        public Rating Rating { get; set; }

        // This is the related list of roles we can use if properly used with Include
        public List<Role> Roles { get; set; }
    }

    class Rating
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }

        // This is the column in the database
        public int MovieId { get; set; }

        // This is the related object we can use from our code if properly used with Include
        public Movie Movie { get; set; }

        // This is the column in the database
        public int ActorId { get; set; }

        // This is the related object we can use from our code if properly used with Include
        public Actor Actor { get; set; }


    }

    // Define a database context for our SuncoastMovies database/
    // It derives from DbContext so we get all the
    // abilities of a database context from EF Core
    class SuncoastMoviesContext : DbContext
    {
        // Define a Movies property that is a DbSet
        public DbSet<Movie> Movies { get; set; }

        // Define Ratings Property that is a DbSet
        public DbSet<Rating> Ratings { get; set; }

        // Define a method required by EF that will configure our connection 
        // to the database.
        // 
        // DbContextOptionsBuilder is provided to us. We then tell that object
        // that we want to connect to a PostgreS database named SuncoastMovies on
        // our local machine.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.UseNpgsql("server=localhost;database=SuncoastMovies");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // Get a new context which will connect to the database
            var context = new SuncoastMoviesContext();

            // Get a reference to our collection of movies.
            // NOTE: this doesn't yet access any of them, just gives
            // us a variable that knows how.
            var movies = context.Movies.Include(movie => movie.Rating);

            // Test database connection by counting the number of movies
            var movieCount = movies.Count();
            Console.WriteLine($"There are {movieCount} movies!");

            // Getting a list of all the movies
            foreach (var movie in movies)
            {
                if (movie.Rating == null)
                {
                    Console.WriteLine($"{movie.Title} has not been rated yet");
                }
                else
                {
                    Console.WriteLine($"{movie.Title} is rated {movie.Rating.Description}");
                }
            }
        }
    }
}
