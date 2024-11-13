using Microsoft.AspNetCore.Mvc;
using P3.Models;

namespace P3.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart _cart;

        public CartSummaryViewComponent(ICart cart)
        {
            _cart = cart as Cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
