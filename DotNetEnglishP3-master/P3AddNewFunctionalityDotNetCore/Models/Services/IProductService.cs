using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using P3.Models.Entities;
using P3.Models.ViewModels;

namespace P3.Models.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<ProductViewModel> GetAllProductsViewModel();
        Product GetProductById(int id);
        ProductViewModel GetProductByIdViewModel(int id);
        void UpdateProductQuantities();
        public List<ValidationResult> CheckProductViewModelErrors(ProductViewModel product);
        void SaveProduct(ProductViewModel product);
        void DeleteProduct(int id);
        Task<Product> GetProduct(int id);
        Task<IList<Product>> GetProduct();
    }
}
