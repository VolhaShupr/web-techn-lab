using Microsoft.AspNetCore.Mvc;

namespace lab.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
