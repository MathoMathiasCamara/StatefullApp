using StatefullApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StatefullApp.Stores
{
    public class SaleStore
    {
        public event EventHandler<object> StoreChanged;
        private readonly ICollection<SaleModel> saleDb;

        public SaleStore()
        {
            this.saleDb = new HashSet<SaleModel>();
        }



        public IReadOnlyCollection<Sale> GetSales(DateTime? date = null)
        {
            if(date == null)
            return LoadSale();

            return null;
        }

        private IReadOnlyCollection<Sale> LoadSale()
        {

            var sells = saleDb.GroupBy(x => x.Id).Select(x =>
            {
                if (x.LastOrDefault().State != State.Deleted)
                    return new Sale
                    {
                        Id = x.Key,
                        Qty = x.Last().Qty,
                        Product = JsonSerializer.Deserialize<Product>(x.LastOrDefault().ProductData)
                    };
                return null;
            });


            return sells.Where(x => x is not null).ToList();
        }

        public void AddSale(Sale sale)
        {
            Save(sale, State.Added);
        }

        public void RemoveSale(Sale sale)
        {
            Save(sale, State.Deleted);
        }

        public void UpdateSale(Sale sale)
        {
            Save(sale, State.Modified);
        }

        public IReadOnlyCollection<SaleModel> GetDatabase()
        {
            return saleDb.ToList();
        }

        private void Save(Sale sale, State state)
        {
            var item = new SaleModel
            {
                Id = sale.Id,
                Qty = sale.Qty,
                Index = Guid.NewGuid(),
                Stamp = DateTime.UtcNow,
                State = state,
                ProductData = JsonSerializer.Serialize(sale.Product)
            };

            this.saleDb.Add(item);

        }
    }
}
