using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebbLabb3
{
    public class PurchaseTicketModel : PageModel
    {
        private readonly WebbLabb3Context _context;

        public PurchaseTicketModel(WebbLabb3Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        [BindProperty]
        public int TicketAmount { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostPurchaseTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);
            if (Movie != null)
            {
                if (Movie.SeatsLeft >= TicketAmount)
                {
                    Movie.SeatsLeft -= TicketAmount;
                }
            }
            else
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return Page();
        }
        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
