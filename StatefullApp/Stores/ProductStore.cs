using StatefullApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullApp.Stores
{
    public class ProductStore
    {
        private readonly SaleStore sellStore;
        private readonly ICollection<ProductModel> productDb;
        public DateTime StampToUse { get; set; } = DateTime.Now;
        public ProductStore(SaleStore sellStore)
        {
            this.productDb = new HashSet<ProductModel>(); 
            this.sellStore = sellStore;
            this.sellStore.StoreChanged += SellStoreChanged;
        }

        private void SellStoreChanged(object? sender, object e)
        {
            //throw new NotImplementedException();
        }

        public IReadOnlyCollection<Product> GetProducts(DateTime? dateTime = null)
        {
            if(dateTime == null)
            return LoadProducts();

            return LoadProductsFrom(dateTime.Value);
        }

        private IReadOnlyCollection<Product> LoadProducts()
        {
            var products = productDb.GroupBy(x => x.Id).Select(x =>
            {
                if (x.LastOrDefault().State != State.Deleted)
                    return new Product
                    {
                        Id = x.Key,
                        Name = x.LastOrDefault()?.Name ?? "NA",
                    };
                return null;
            });

            return products.Where(x => x is not null).ToList();
        }
        private IReadOnlyCollection<Product> LoadProductsFrom(DateTime date)
        {
            var products = productDb.Where(x => x.Stamp.Date == date.Date).GroupBy(x => x.Id).Select(x =>
            {
                if (x.LastOrDefault().State != State.Deleted)
                    return new Product
                    {
                        Id = x.Key,
                        Name = x.LastOrDefault()?.Name ?? "NA",
                    };
                return null;
            });

            return products.Where(x => x is not null).ToList();
        }

        public void AddProduct(Product product)
        {
            var newItem = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Index = Guid.NewGuid(),
                Stamp = StampToUse,
                State = State.Added,
            };

            this.productDb.Add(newItem);
        }

        public void RemoveProduct(Product product)
        {
            var item = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Index = Guid.NewGuid(),
                Stamp = StampToUse,
                State = State.Deleted,
            };

            this.productDb.Add(item);
        }

        public void UpdateProduct(Product product)
        {
            var item = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Index = Guid.NewGuid(),
                Stamp = StampToUse,
                State = State.Modified,
            };

            this.productDb.Add(item);
        }

        public IReadOnlyCollection<ProductModel> GetDatabase()
        {
            return productDb.ToList();
        }
    }
}
