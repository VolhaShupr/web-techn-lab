using lab.DAL.Data;
using lab.Extensions;
using lab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext _context; 
        private string cartKey = "cart";
        private Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Index()
        {
            _cart = HttpContext.Session.Get<Cart>(cartKey); 
            return View(_cart.Items.Values);
        }

        public IActionResult Add(int id, string returnUrl)
        {
            _cart = HttpContext.Session.Get<Cart>(cartKey); 
            var item = _context.MusInstruments.Find(id); 
            
            if (item != null)
            {
                _cart.AddToCart(item);
                HttpContext.Session.Set<Cart>(cartKey, _cart);
            }
            return Redirect(returnUrl);
        }

        public IActionResult Delete(int id)
        {
            _cart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }
    }
}
