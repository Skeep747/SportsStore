using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult List(int productPage = 1) 
            => View(_repository.Products
                .OrderBy(p => p.ProductId)
                .Skip((productPage -1) * PageSize)
                .Take(PageSize));
    }
}