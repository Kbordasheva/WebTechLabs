using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Bordasheva.Extensions;
using WebApplication_Bordasheva.Models;
using WebLab4.Entities;
using WebLab4.Data;

namespace WebApplication_Bordasheva.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _context;
        int _pageSize;

        public ProductController(ApplicationDbContext context)
        {
            _pageSize = 3;
            _context = context;
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo=1)
        {
            var phonesFiltered = _context.Phones.Where(p => !group.HasValue || p.PhoneGroupId == group.Value);
            // Поместить список групп во ViewData 
            ViewData["Groups"] = _context.PhoneGroups;

            // Получить id текущей группы и поместить в TempData 
            ViewData["CurrentGroup"] = group ?? 0;

            var model = ListViewModel<Phone>.GetModel(phonesFiltered, pageNo, _pageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }
    }
}
