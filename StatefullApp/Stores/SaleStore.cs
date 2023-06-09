﻿using StatefullApp.Models;
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
        private readonly StateDbContext _dbContext;

        public SaleStore(StateDbContext dbContext)
        { 
            _dbContext = dbContext;
        }



        public IReadOnlyCollection<Sale> GetSales(DateTime? date = null)
        {
            if(date == null)
            return LoadSale();

            return null;
        }

        private IReadOnlyCollection<Sale> LoadSale()
        {

            var sells = GetDatabase().GroupBy(x => x.Id).Select(x =>
            {
                if (x.LastOrDefault().State != State.Deleted)
                    return new Sale
                    {
                        Id = x.Key,
                        Client = x.LastOrDefault().Client,
                        SaleItems = JsonSerializer.Deserialize<ICollection<SaleItem>>(x.LastOrDefault().SaleItems)
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
            return this._dbContext.Sales.ToList();
        }

        private async void Save(Sale sale, State state)
        {
            var item = new SaleModel
            {
                Id = sale.Id,
                Client = sale.Client,
                Index = Guid.NewGuid(),
                Stamp = DateTime.UtcNow,
                State = state,
                SaleItems = JsonSerializer.Serialize(sale.SaleItems)
            };

            this._dbContext.Sales.Add(item);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
