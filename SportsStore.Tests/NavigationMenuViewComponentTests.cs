using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductId = 1, Name = "P1", Category = "Apples"},
                new Product{ProductId = 2, Name = "P2", Category = "Apples"},
                new Product{ProductId = 3, Name = "P3", Category = "Plums"},
                new Product{ProductId = 4, Name = "P4", Category = "Oranges"},
            }).AsQueryable());

            var tartget = new NavigationMenuViewComponent(mock.Object);

            //Act
            string[] results = ((IEnumerable<string>)(tartget.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            //Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            //Arrange
            string categoryToSelect = "Apples";
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductId = 1, Name = "P1", Category = "Apples"},
                new Product{ProductId = 4, Name = "P2", Category = "Oranges"},
            }).AsQueryable());

            var tartget = new NavigationMenuViewComponent(mock.Object);
            tartget.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };
            tartget.RouteData.Values["category"] = categoryToSelect;

            //Act
            string result = (string)(tartget.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            //Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}
