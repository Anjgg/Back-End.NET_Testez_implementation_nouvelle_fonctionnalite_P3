using P3.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Xunit;

namespace P3.Tests
{
    public class ProductViewModelTests
    {
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
            // Act
            var results = ValidateModel(model);

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
            // Act
            var results = ValidateModel(model);

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
            // Act
            var results = ValidateModel(model);

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
            // Act
            var results = ValidateModel(model);

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
            // Act
            var results = ValidateModel(model);

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
            // Act
            var results = ValidateModel(model);

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
            // Act
            var results = ValidateModel(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.PriceNotGreaterThanZero;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        private List<ValidationResult> ValidateModel(ProductViewModel product)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(product);
            Validator.TryValidateObject(product, context, results, true);
            return results;
        }
    }
}