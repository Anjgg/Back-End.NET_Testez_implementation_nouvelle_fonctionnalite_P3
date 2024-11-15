using P3.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Xunit;
using Moq;
using P3.Models.Services;
using P3.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using P3.Models.Entities;


namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void ProductControllerCreate_ShouldReturnView_WhenProductViewModelIsInvalid()
        {
            // Arrange
            var product = new ProductViewModel
            {
                Name = string.Empty,
                Stock = string.Empty,
                Price = string.Empty
            };
            var listError = new List<ValidationResult>
            {
                new ValidationResult("MissingName"),
                new ValidationResult("MissingStock"),
                new ValidationResult("MissingPrice"),
            };

            var productService = new Mock<IProductService>();
            productService.Setup(ps => ps.ValidateModel(It.IsAny<ProductViewModel>())).Returns(listError);

            var controller = new ProductController(productService.Object,It.IsAny<ILanguageService>());

            // Act
            var actionResult = controller.Create(product);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            Assert.Equal(product, viewResult.Model);
            productService.Verify(service => service.SaveProduct(It.IsAny<ProductViewModel>()), Times.Never);
        }

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
            productService.Setup(ps => ps.ValidateModel(It.IsAny<ProductViewModel>())).Returns(listError);

            var controller = new ProductController(productService.Object, It.IsAny<ILanguageService>());

            // Act
            var actionResult = controller.Create(product);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Equal("Admin", redirectResult.ActionName);
            productService.Verify(service => service.SaveProduct(product), Times.Once);
        }
    }
}

            
