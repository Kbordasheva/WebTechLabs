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
using WebLab4.Data;
using WebLab4.Entities;

namespace WebApplication_Bordasheva.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WebLab4.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(WebLab4.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        public IActionResult OnGet()
        {
        ViewData["PhoneGroupId"] = new SelectList(_context.PhoneGroups, "PhoneGroupId", "GroupName");
            return Page();
        }

        [BindProperty]
        public Phone Phone { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Phones.Add(Phone);
            await _context.SaveChangesAsync();

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
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
