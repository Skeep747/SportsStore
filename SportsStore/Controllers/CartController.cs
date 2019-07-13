using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private readonly Cart _cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            _cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if(product != null)
            {
                _cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if(product != null)
            {
                _cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}