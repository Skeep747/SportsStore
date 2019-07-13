using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;

        public AdminController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index() => View(_repository.Products);

        public ViewResult Edit(int productId) => View(_repository.Products.FirstOrDefault(p => p.ProductId == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
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
            var deleteProduct = _repository.DeleteProduct(productId);
            if(deleteProduct != null)
            {
                TempData["message"] = $"{deleteProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }

        public ViewResult Create() => View("Edit", new Product());
    }
}