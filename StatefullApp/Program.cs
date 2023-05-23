using StatefullApp.Models;
using StatefullApp.Stores;
using System.Net.NetworkInformation;

namespace StatefullApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            var fullPath = path + "\\statefull.db";

            using (var dbContext = new StateDbContext(fullPath))
            {
                var saleStore = new SaleStore(dbContext);
                var productStore = new ProductStore(saleStore, dbContext);

                // create the initial datas
                    if (dbContext.Database.CreateIfNotExists())
                    {
                        // products
                        if (productStore.GetDatabase().Count() == 0)
                        {
                            productStore.AddProduct(new Product { Name = "Banana" });
                            productStore.AddProduct(new Product { Name = "Apple" });
                            productStore.AddProduct(new Product { Name = "Sprite" });
                            productStore.AddProduct(new Product { Name = "Fish" });
                        }
                    }


                Console.WriteLine("");
                Console.WriteLine("Current Products : ");
                foreach (var item in productStore.GetProducts())
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine("");

                //operation on product
                saleStore.AddSale(new Sale
                {
                    Client = "Moussa Keita",
                    SaleItems = new List<SaleItem>
                    {
                        new SaleItem { Qty = 10  , Product = productStore.GetProducts().First()},
                        new SaleItem { Qty = 5  , Product = productStore.GetProducts().Last()},
                    }
                });

              

                Console.WriteLine("");
                Console.WriteLine("Current Sales : ");
                foreach (var item in saleStore.GetSales())
                {
                    Console.WriteLine(item.ToString());
                }

                Console.WriteLine("End.............");
            }

        }

    }
}