
using P3.Models.Entities;

namespace P3.Models
{
    public interface ICart
    {
        void AddItem(Product product, int quantity);

        void RemoveLine(Product product);

        void Clear();

        double GetTotalValue();

        double GetAverageValue();

        public bool HasThisProduct(Product product);
    }
}