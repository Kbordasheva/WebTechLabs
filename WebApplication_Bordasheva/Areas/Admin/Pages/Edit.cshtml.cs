using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLab4.Data;
using WebLab4.Entities;

namespace WebApplication_Bordasheva.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebLab4.Data.ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public EditModel(WebLab4.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;

        }

        [BindProperty]
        public Phone Phone { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }

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
           ViewData["PhoneGroupId"] = new SelectList(_context.PhoneGroups, "PhoneGroupId", "GroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Phone).State = EntityState.Modified;
            if (Image != null)
            {
                var fileName = $"{Phone.PhoneId}" +
                Path.GetExtension(Image.FileName);
                Phone.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images",
                fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(Phone.PhoneId))
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

        private bool PhoneExists(int id)
        {
            return _context.Phones.Any(e => e.PhoneId == id);
        }
    }
}
