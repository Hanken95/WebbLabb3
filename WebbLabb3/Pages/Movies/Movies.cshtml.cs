using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebbLabb3
{
    public class MoviesModel : PageModel
    {
        private readonly WebbLabb3Context _context;

        public MoviesModel(WebbLabb3Context context)
        {
            _context = context;
        }

        public string StartTimeSort { get; set; }
        public string SeatsLeftSort { get; set; }
        public IList<Movie> Movie { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            Movie = await _context.Movie.ToListAsync();
            if (sortOrder == "StartTime")
            {
                Movie = Movie.OrderByDescending(s => s.StartTime).ThenByDescending(s => s.SeatsLeft).ToList();
            }
            else if (sortOrder == "SeatsLeft")
            {
                Movie = Movie.OrderByDescending(s => s.SeatsLeft).ThenByDescending(s => s.StartTime).ToList();
            }
            await _context.SaveChangesAsync();
        }
    }
}
