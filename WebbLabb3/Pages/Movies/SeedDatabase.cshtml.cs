using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebbLabb3
{
    public class SeedDatabaseModel : PageModel
    {
        WebbLabb3Context _context;
        public SeedDatabaseModel(WebbLabb3Context contex)
        {
            _context = contex;
        }
        public void OnGet()
        {

        }
        public RedirectToPageResult OnPostSeedDatabase()
        {
             _context.SeedDatabase();
            return RedirectToPage("./Movies");
        }
        public RedirectToPageResult OnPostClearDatabase()
        {
            foreach (var movie in _context.Movie)
            {
                _context.Movie.Remove(movie);
            }
            _context.SaveChanges();
            return RedirectToPage("./Movies");
        }
        public bool CheckDatabase()
        {
            if (_context.Movie.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}