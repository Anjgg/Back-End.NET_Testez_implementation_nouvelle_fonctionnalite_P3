using P3.Models;
using P3.Models.Services;
using P3.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Xunit;
using Moq;
using System.Drawing.Text;
using P3.Models.Repositories;
using Microsoft.Extensions.Localization;


namespace P3.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void ProductViewModel_ShouldReturnListEmpty_WhenProductViewModelIsOK()
        {
            // Arrange
            var model = new ProductViewModel
            {
                Name = "Name",
                Stock = "1",
                Price = "1"
            };
            var productService = new ProductService(It.IsAny<ICart>(),
                                                    It.IsAny<IProductRepository>(),
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());

            // Act
            var results = productService.CheckProductModelErrors(model);

            // Assert
            Assert.Empty(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void ProductViewModel_ShouldThrowError_WhenNameIsEmpty(string culture)
        {
            // Arrange
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            var model = new ProductViewModel
            {
                Name = string.Empty,
                Stock = "1",
                Price = "1"
            };
            var productService = new ProductService(It.IsAny<ICart>(),
                                                    It.IsAny<IProductRepository>(),
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());

            // Act
            var results = productService.CheckProductModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.MissingName;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void ProductViewModel_ShouldThrowError_WhenStockIsEmpty(string culture)
        {
            // Arrange
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            var model = new ProductViewModel
            {
                Name = "Name",
                Stock = string.Empty,
                Price = "1"
            };
            var productService = new ProductService(It.IsAny<ICart>(),
                                                    It.IsAny<IProductRepository>(),
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());


            // Act
            var results = productService.CheckProductModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.MissingStock;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void ProductViewModel_ShouldThrowError_WhenStockNotAInteger(string culture)
        {
            // Arrange
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            var model = new ProductViewModel
            {
                Name = "Name",
                Stock = "Stock",
                Price = "1"
            };
            var productService = new ProductService(It.IsAny<ICart>(), 
                                                    It.IsAny<IProductRepository>(), 
                                                    It.IsAny<IOrderRepository>(), 
                                                    It.IsAny<IStringLocalizer<ProductService>>());


            // Act
            var results = productService.CheckProductModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.StockNotAnInteger;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Equal(2, results.Count);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void ProductViewModel_ShouldThrowError_WhenStockNotGreaterThanZero(string culture)
        {
            // Arrange
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            var model = new ProductViewModel
            {
                Name = "Name",
                Stock = "-1",
                Price = "1"
            };
            var productService = new ProductService(It.IsAny<ICart>(), 
                                                    It.IsAny<IProductRepository>(), 
                                                    It.IsAny<IOrderRepository>(), 
                                                    It.IsAny<IStringLocalizer<ProductService>>());


            // Act
            var results = productService.CheckProductModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.StockNotGreaterThanZero;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void ProductViewModel_ShouldThrowError_WhenPriceIsEmpty(string culture)
        {
            // Arrange
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            var model = new ProductViewModel
            {
                Name = "Name",
                Stock = "1",
                Price = string.Empty
            };
            var productService = new ProductService(It.IsAny<ICart>(),
                                                    It.IsAny<IProductRepository>(),
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());

            // Act
            var results = productService.CheckProductModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.MissingPrice;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void ProductViewModel_ShouldThrowError_WhenPriceNotANumber(string culture)
        {
            // Arrange
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            var model = new ProductViewModel
            {
                Name = "Name",
                Stock = "1",
                Price = "Price"
            };
            var productService = new ProductService(It.IsAny<ICart>(),
                                                    It.IsAny<IProductRepository>(),
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());

            // Act
            var results = productService.CheckProductModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.PriceNotANumber;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Equal(2, results.Count);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void ProductViewModel_ShouldThrowError_WhenPriceNotGreaterThanZero(string culture)   
        {
            // Arrange
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            var model = new ProductViewModel
            {
                Name = "Name",
                Stock = "1",
                Price = "-1"
            };
            var productService = new ProductService(It.IsAny<ICart>(),
                                                    It.IsAny<IProductRepository>(),
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());

            // Act
            var results = productService.CheckProductModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.PriceNotGreaterThanZero;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }
    }
}