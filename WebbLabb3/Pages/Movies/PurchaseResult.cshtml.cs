﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebbLabb3
{
    public class PurchaseResultModel : PageModel
    {
        private readonly WebbLabb3Context _context;

        public PurchaseResultModel(WebbLabb3Context context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }
        public int? TicketAmount { get; set; }
        public bool? PurchaseSuccess { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? ticketAmount, bool? purchaseSuccess)
        {
            TicketAmount = ticketAmount;
            PurchaseSuccess = purchaseSuccess;
            if (id == null || TicketAmount == null || PurchaseSuccess == null)
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
    }
}
