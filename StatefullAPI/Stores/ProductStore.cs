using StatefullAPI.Models;

namespace StatefullAPI.Stores
{
    public class ProductStore
    {
        private readonly SaleStore sellStore;
        private readonly StateDbContext _dbContext;
        private DateTime StampToUse { get; set; } = DateTime.Now;
        public ProductStore(SaleStore sellStore, StateDbContext dbContext)
        {

            this._dbContext = dbContext; 
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
            var products = GetDatabase().GroupBy(x => x.Id).Select(x =>
            {
                if (x.LastOrDefault().State == State.Added)
                    return new Product
                    {
                        Id = x.Key,
                        Name = x.LastOrDefault()?.Name ?? "NA",
                    };
                return null;
            }).ToList();

            return products.Where(x => x is not null).ToList();
        }
        private IReadOnlyCollection<Product> LoadProductsFrom(DateTime date)
        {
            var list = GetDatabase().ToList();
            var products = list.Where(x => x.Stamp.Date == date.Date).GroupBy(x => x.Id).Select(x =>
            {
                if (x.LastOrDefault().State == State.Added)
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
            Save(product, State.Added);
        }

        public void RemoveProduct(Product product)
        {
            Save(product, State.Deleted);            
        }

        private async void Save(Product product, State state) {
            var item = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Index = Guid.NewGuid(),
                Stamp = StampToUse,
                State = state,
            };

            this._dbContext.Products.Add(item);
            await this._dbContext.SaveChangesAsync();
        }

        public void UpdateProduct(Product product)
        {
            var previousProduct = LoadProducts().FirstOrDefault(defaultValue => defaultValue.Id == product.Id);
            if (previousProduct is null) return;

            //labels a copy of previous as Modified
            Save(previousProduct, State.Modified);
            // and add a new line considered as the new value
            Save(product, State.Added);
        }

        public IReadOnlyCollection<ProductModel> GetDatabase()
        {
            return _dbContext.Products.ToList();
        }
    }
}
