using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebbLabb3
{
    public class WebbLabb3Context : DbContext
    {
        public WebbLabb3Context (DbContextOptions<WebbLabb3Context> options)
            : base(options)
        {
        }

        public DbSet<WebbLabb3.Movie> Movie { get; set; }
        public void SeedDatabase()
        {
            Movie[] movies =
            {
                CreateMovie("Mera kol i bagdad", DateTime.Now, 2),
                CreateMovie("Mindre kol i bagdad", DateTime.Now, 1),
                CreateMovie("Flera kol i bagdad", DateTime.Now, 1),
                CreateMovie("Ofta kol i bagdad", DateTime.Now, 2),
            };
            foreach (var movie in movies)
            {
                Movie.Add(movie);
            }
            this.SaveChanges();
        }
        public static Movie CreateMovie(string title, DateTime startTime, int salon)
        {
            var newMovie = new Movie
            {
                Title = title,
                StartTime = startTime,
                Salon = salon
            };
            if (salon == 1)
            {
                newMovie.SeatsLeft = 50;
                return newMovie;
            }
            else if (salon == 2)
            {
                newMovie.SeatsLeft = 100;
                return newMovie;
            }
            else
            {
                throw new Exception("Invalid salon");
            }
        }
    }
}
