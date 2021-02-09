﻿using System;
using Microsoft.EntityFrameworkCore;

namespace SCMovies
{
    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryDirector { get; set; }
        public int YearReleased { get; set; }
        public string Genre { get; set; }
    }

    // Define a database context for our SuncoastMovies database/
    // It derives from DbContext so we get all the
    // abilities of a database context from EF Core
    class SuncoastMoviesContext : DbContext
    {
        // Define a Movies property that is a DbSet
        public DbSet<Movie> Movies { get; set; }

        // Define a method required by EF that will configure our connection 
        // to the database.
        // 
        // DbContextOptionsBuilder is provided to us. We then tell that object
        // that we want to connect to a PostgreS database named SuncoastMovies on
        // our local machine.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=SuncoastMovies");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to C#");
        }
    }
}
