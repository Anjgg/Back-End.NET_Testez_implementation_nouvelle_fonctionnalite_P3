using Microsoft.Extensions.Localization;
using P3.Controllers;
using P3.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Xunit;

namespace P3.Tests
{
    public class ProductServiceTests
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
            var expectedMessage = P3.Resources.Models.Services.ProductService.MissingName;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        private List<ValidationResult> ValidateModel(ProductViewModel product)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(product);
            Validator.TryValidateObject(product, context, results);
            return results;
        }
    }
}