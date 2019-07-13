using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productId) => View(repository.Products.FirstOrDefault(p => p.ProductId == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var deleteProduct = repository.DeleteProduct(productId);
            if(deleteProduct != null)
            {
                TempData["message"] = $"{deleteProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }
    }
}