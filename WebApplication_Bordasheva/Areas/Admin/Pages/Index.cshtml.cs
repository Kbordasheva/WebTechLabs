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
    public class IndexModel : PageModel
    {
        private readonly WebLab4.Data.ApplicationDbContext _context;

        public IndexModel(WebLab4.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Phone> Phone { get;set; }

        public async Task OnGetAsync()
        {
            Phone = await _context.Phones
                .Include(p => p.Group).ToListAsync();
        }
    }
}
