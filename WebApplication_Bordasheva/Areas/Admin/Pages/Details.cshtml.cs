using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLab4.Data;
using WebLab4.Entities;

namespace WebApplication_Bordasheva.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WebLab4.Data.ApplicationDbContext _context;

        public DetailsModel(WebLab4.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Phone Phone { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Phone = await _context.Phones
                .Include(p => p.Group).FirstOrDefaultAsync(m => m.PhoneId == id);

            if (Phone == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
