using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using P3.Controllers;
using P3.Data;
using P3.Models.Entities;
using P3.Models.Repositories;
using P3.Models.Services;
using P3.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using P3.Models;
using Microsoft.Extensions.Localization;


namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductControllerTests
    {
        

        [Fact]
        public void ProductControllerCreate_ShouldReturnAdminAction_WhenProductViewModelIsOK()
        {
            // Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Stock = "1",
                Price = "1"
            };
            var listError = new List<ValidationResult> { };

            var productService = new Mock<IProductService>();
            productService.Setup(ps => ps.CheckProductViewModelErrors(It.IsAny<ProductViewModel>())).Returns(listError);

            var controller = new ProductController(productService.Object, It.IsAny<ILanguageService>());

            // Act
            var actionResult = controller.Create(product);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Equal("Admin", redirectResult.ActionName);
            productService.Verify(service => service.SaveProduct(product), Times.Once);
        }

        // test save product, 
    }
}

            
