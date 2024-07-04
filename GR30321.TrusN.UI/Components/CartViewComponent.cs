using GR30321.Domain.Entities;
using GR30321.TrusN.UI.Session;
using Microsoft.AspNetCore.Mvc;

namespace GR30321.TrusN.UI.Components
{
    public class CartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<Cart>("cart");
            return View(cart);

        }
    }
}
