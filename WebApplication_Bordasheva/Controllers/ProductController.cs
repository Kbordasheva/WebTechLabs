using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Bordasheva.Extensions;
using WebApplication_Bordasheva.Models;
using WebLab4.Entities;
using WebLab4.Data;
using Microsoft.Extensions.Logging;

namespace WebApplication_Bordasheva.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _context;
        int _pageSize;
        private ILogger _logger;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _pageSize = 3;
            _context = context;
            _logger = logger;
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
            _logger.LogInformation($"info: group={group},  page={pageNo}");
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }
    }
}
