using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Bordasheva.Extensions;
using WebApplication_Bordasheva.Models;
using WebLab4.Entities;

namespace WebApplication_Bordasheva.Controllers
{
    public class ProductController : Controller
    {
        public List<Phone> _phones;
        List<PhoneGroup> _phoneGroups;
        int _pageSize;

        public ProductController()
        {
            _pageSize = 3;
            SetupData();
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo=1)
        {
            var phonesFiltered = _phones.Where(p => !group.HasValue || p.PhoneGroupId == group.Value);
            // Поместить список групп во ViewData 
            ViewData["Groups"] = _phoneGroups;

            // Получить id текущей группы и поместить в TempData 
            ViewData["CurrentGroup"] = group ?? 0;

            var model = ListViewModel<Phone>.GetModel(phonesFiltered, pageNo, _pageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }

        /// <summary> 
        /// Инициализация списков 
        /// </summary> 
        private void SetupData()
        {
            _phoneGroups = new List<PhoneGroup>
            {
                new PhoneGroup {PhoneGroupId=1, GroupName="Apple"},
                new PhoneGroup {PhoneGroupId=2, GroupName="Honor"},
                new PhoneGroup {PhoneGroupId=3, GroupName="Pixel"},
                new PhoneGroup {PhoneGroupId=4, GroupName="Samsung"}, 
                new PhoneGroup {PhoneGroupId=5, GroupName="Xiaomi"},
                new PhoneGroup {PhoneGroupId=6, GroupName="Nokia"}
            };

            _phones = new List<Phone>
            {
                new Phone {PhoneId = 1, PhoneName="Apple iPhone 11 64GB",
                    Description="Apple iOS, экран 6.1 IPS (828x1792)",
                    Price =1840, PhoneGroupId=1, Image="Apple.png" },
                new Phone { PhoneId = 2, PhoneName="HONOR 10X Lite",
                    Description="Android, экран 6.67 IPS (1080x2400)",
                    Price =529, PhoneGroupId=2, Image="Honor.png" },
                new Phone { PhoneId = 3, PhoneName="Google Pixel 4a",
                    Description="Android, экран 5.8 OLED (1080x2340)",
                    Price =1290, PhoneGroupId=3, Image="Pixel.png" },
                new Phone { PhoneId = 4, PhoneName="Samsung Galaxy M31",
                    Description="Android, экран 6.4 AMOLED (1080x2340)",
                    Price =660, PhoneGroupId=4, Image="Samsung.png" },
                new Phone { PhoneId = 5, PhoneName="Xiaomi Redmi 9",
                    Description="Android, экран 6.53 IPS (1080x2340)",
                    Price =470, PhoneGroupId=5, Image="Xiaomi.png" },
                new Phone { PhoneId = 6, PhoneName="Nokia 105",
                    Description="экран 1.77 TFT (120x160)",
                    Price =49, PhoneGroupId=6, Image="Nokia.png" }
            };
        }
    }
}
