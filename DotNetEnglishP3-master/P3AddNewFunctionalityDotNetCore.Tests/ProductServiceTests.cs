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
using Microsoft.EntityFrameworkCore;
using P3.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using P3.Models.Entities;


namespace P3.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void CheckProductViewModelErrors_ShouldReturnListEmpty_WhenProductViewModelIsOK()
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
            var results = productService.CheckProductViewModelErrors(model);

            // Assert
            Assert.Empty(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void CheckProductViewModelErrors_ShouldThrowError_WhenNameIsEmpty(string culture)
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
            var results = productService.CheckProductViewModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.MissingName;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void CheckProductViewModelErrors_ShouldThrowError_WhenStockIsEmpty(string culture)
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
            var results = productService.CheckProductViewModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.MissingStock;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void CheckProductViewModelErrors_ShouldThrowError_WhenStockNotAInteger(string culture)
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
            var results = productService.CheckProductViewModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.StockNotAnInteger;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Equal(2, results.Count);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void CheckProductViewModelErrors_ShouldThrowError_WhenStockNotGreaterThanZero(string culture)
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
            var results = productService.CheckProductViewModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.StockNotGreaterThanZero;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void CheckProductViewModelErrors_ShouldThrowError_WhenPriceIsEmpty(string culture)
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
            var results = productService.CheckProductViewModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.MissingPrice;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void CheckProductViewModelErrors_ShouldThrowError_WhenPriceNotANumber(string culture)
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
            var results = productService.CheckProductViewModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.PriceNotANumber;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Equal(2, results.Count);
        }

        [Theory]
        [InlineData("en")]
        [InlineData("fr")]
        [InlineData("es")]
        public void CheckProductViewModelErrors_ShouldThrowError_WhenPriceNotGreaterThanZero(string culture)   
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
            var results = productService.CheckProductViewModelErrors(model);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.PriceNotGreaterThanZero;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Fact]
        public async Task SaveProduct_ShouldAddSingleProductInDB_WhenProductViewModelIsValid()
        {
            // Arrange
            var product = new ProductViewModel
            {
                Name = "TestProduct Name",
                Stock = "22",
                Price = "1,35",
                Description = "TestProduct Description",
                Details = "TestProduct Details"
            };

            var options = new DbContextOptionsBuilder<P3Referential>()
                                    .UseSqlServer("Server=.;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true")
                                    .Options;
            var config = new Mock<IConfiguration>();
            var context = new P3Referential(options, config.Object);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var productRepository = new ProductRepository(context);
            var productService = new ProductService(It.IsAny<ICart>(),
                                                    productRepository,
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());

            // Act
            productService.SaveProduct(product);

            // Assert
            var savedProductList = await context.Product.ToListAsync();
            Assert.Single(savedProductList);

            var savedProduct = await context.Product.SingleOrDefaultAsync(p => p.Id == 1);
            Assert.Equal("TestProduct Name", savedProduct.Name);
            Assert.Equal(22, savedProduct.Quantity);
            Assert.Equal(1.35, savedProduct.Price);
            Assert.Equal("TestProduct Description", savedProduct.Description);
            Assert.Equal("TestProduct Details", savedProduct.Details);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnOneEntitiesInDB_WhenDBContainsTwoEntities()
        {
            // Arrange
            var product1 = new ProductViewModel
            {
                Name = "TestProduct1 Name",
                Stock = "11",
                Price = "1,11",
                Description = "TestProduct1 Description",
                Details = "TestProduct1 Details"
            };
            var product2 = new ProductViewModel
            {
                Name = "TestProduct2 Name",
                Stock = "22",
                Price = "2,22",
                Description = "TestProduct2 Description",
                Details = "TestProduct2 Details"
            };

            var options = new DbContextOptionsBuilder<P3Referential>()
                                    .UseSqlServer("Server=.;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true")
                                    .Options;
            var config = new Mock<IConfiguration>();
            var context = new P3Referential(options, config.Object);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var productRepository = new ProductRepository(context);
            var productService = new ProductService(It.IsAny<ICart>(),
                                                    productRepository,
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());
            
            productService.SaveProduct(product1);
            productService.SaveProduct(product2);

            
            var savedProductListBeforeDelete = await context.Product.ToListAsync();
            Assert.Equal(2, savedProductListBeforeDelete.Count);

            // Act 
            productService.DeleteProduct(1);

            // Assert
            var savedProductList = await context.Product.ToListAsync();
            Assert.Single(savedProductList);

            var savedProduct = await context.Product.SingleOrDefaultAsync(p => p.Id == 2);
            Assert.Equal("TestProduct2 Name", savedProduct.Name);
            Assert.Equal(22, savedProduct.Quantity);
            Assert.Equal(2.22, savedProduct.Price);
            Assert.Equal("TestProduct2 Description", savedProduct.Description);
            Assert.Equal("TestProduct2 Details", savedProduct.Details);
        }
    }
}