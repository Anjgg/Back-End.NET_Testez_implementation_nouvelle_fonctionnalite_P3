using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Moq;
using P3.Data;
using P3.Models;
using P3.Models.Repositories;
using P3.Models.Services;
using P3.Models.ViewModels;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;


namespace P3.Tests
{
    public class ProductServiceTests
    {
        private ProductService _productService;

        public ProductServiceTests() 
        {
            _productService = new ProductService(It.IsAny<ICart>(),
                                                 It.IsAny<IProductRepository>(),
                                                 It.IsAny<IOrderRepository>(),
                                                 It.IsAny<IStringLocalizer<ProductService>>());
        }

        [Fact]
        public void CheckProductViewModelErrors_ShouldReturnListEmpty_WhenProductViewModelIsOK()
        {
            // Arrange
            var product = CreateProductViewModel();

            // Act
            var results = _productService.CheckProductViewModelErrors(product);

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
            var product = CreateProductViewModel(name: string.Empty);
            
            // Act
            var results = _productService.CheckProductViewModelErrors(product);

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
            var product = CreateProductViewModel(stock: string.Empty);
            
            // Act
            var results = _productService.CheckProductViewModelErrors(product);

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
            var product = CreateProductViewModel(stock: "stockNotAMember");
            
            // Act
            var results = _productService.CheckProductViewModelErrors(product);

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
            var product = CreateProductViewModel(stock: "-1");
            
            // Act
            var results = _productService.CheckProductViewModelErrors(product);

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
            var product = CreateProductViewModel(price: string.Empty);
            
            // Act
            var results = _productService.CheckProductViewModelErrors(product);

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
            var product = CreateProductViewModel(price: "priceNotANumber");
            
            // Act
            var results = _productService.CheckProductViewModelErrors(product);

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
            var product = CreateProductViewModel(price: "-1");
           
            // Act
            var results = _productService.CheckProductViewModelErrors(product);

            // Assert
            var expectedMessage = Resources.Models.Services.ProductService.PriceNotGreaterThanZero;
            Assert.Contains(results, res => res.ErrorMessage == expectedMessage);
            Assert.Single(results);
        }

        [Fact]
        public async Task SaveProduct_ShouldAddSingleProductInDB_WhenProductViewModelIsValid()
        {
            // Arrange
            var product = CreateProductViewModel();
            var context = CreateTestDb();

            // Act
            _productService.SaveProduct(product);

            // Assert
            var savedProductList = await context.Product.ToListAsync();
            Assert.Single(savedProductList);

            var savedProduct = await context.Product.SingleOrDefaultAsync(p => p.Id == 1);
            Assert.Equal("TestProductName", savedProduct.Name);
            Assert.Equal(1, savedProduct.Quantity);
            Assert.Equal(1.11, savedProduct.Price);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnOneEntitiesInDB_WhenDBContainsTwoEntities()
        {
            // Arrange

            var product1 = CreateProductViewModel(index : "1");
            var product2 = CreateProductViewModel(index : "2");
            var context = CreateTestDb();
            
            _productService.SaveProduct(product1);
            _productService.SaveProduct(product2);

            // Act 
            _productService.DeleteProduct(1);

            // Assert
            var savedProductList = await context.Product.ToListAsync();
            Assert.Single(savedProductList);

            var savedProduct = await context.Product.SingleOrDefaultAsync(p => p.Id == 2);
            Assert.Equal("2TestProductName", savedProduct.Name);
            Assert.Equal(21, savedProduct.Quantity);
            Assert.Equal(21.11, savedProduct.Price);
        }

        private ProductViewModel CreateProductViewModel(string name = "TestProductName", string stock = "1", string price = "1,11", string index = null)
        {
            return new ProductViewModel
            {
                Name = $"{index}{name}",
                Stock = $"{index}{stock}",
                Price = $"{index}{price}",
            };
        }

        private P3Referential CreateTestDb()
        {
            var options = new DbContextOptionsBuilder<P3Referential>()
                                    .UseSqlServer("Server=.;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true")
                                    .Options;
            var config = new Mock<IConfiguration>();
            var context = new P3Referential(options, config.Object);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var productRepository = new ProductRepository(context);
            _productService = new ProductService(It.IsAny<ICart>(),
                                                    productRepository,
                                                    It.IsAny<IOrderRepository>(),
                                                    It.IsAny<IStringLocalizer<ProductService>>());
            return context;
        }
    }
}