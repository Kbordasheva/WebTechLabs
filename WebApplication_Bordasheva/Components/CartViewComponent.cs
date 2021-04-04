using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Bordasheva.Extensions;
using WebApplication_Bordasheva.Models;

namespace WebApplication_Bordasheva.Components
{
    public class CartViewComponent : ViewComponent
    {
        private Cart _cart;
        public CartViewComponent(Cart cart)
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }

    }
}
