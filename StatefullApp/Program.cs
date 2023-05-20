using StatefullApp.Stores;
using System.Net.NetworkInformation;

namespace StatefullApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sellStore = new SaleStore();
            var productStore = ProductExample(sellStore);
            
            // new sales
            var sale = new Sale
            {
                Id = 1,
                Product = productStore.GetProducts().First(),
                Qty = 10
            };
            sellStore.AddSale(sale);

            sale = new Sale
            {
                Id = 2,
                Product = productStore.GetProducts().Last(),
                Qty = 2
            };
            sellStore.AddSale(sale);    

            //update a product to check the sale
            var product = productStore.GetProducts().First();
            product.Name = product.Name + " update";
            productStore.UpdateProduct(product);
            sale = new Sale
            {
                Id = 3,
                Product = productStore.GetProducts().First(),
                Qty = 5
            };
            sellStore.AddSale(sale);


            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Sales :");
            var sales = sellStore.GetSales();
            foreach (var item in sales)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("");
            Console.WriteLine("");


            Console.WriteLine("The end ..........");

        }

        public static ProductStore ProductExample(SaleStore saleStore)
        {
            var productStore = new ProductStore(saleStore);

            productStore.AddProduct(new Product { Name = "Orange", Id = 1 });
            productStore.AddProduct(new Product { Name = "Banana", Id = 2 });
            productStore.AddProduct(new Product { Name = "Apple", Id = 3 });
            productStore.AddProduct(new Product { Name = "Coffee", Id = 4 });

            // update
            Console.WriteLine("Products :");
            var products = productStore.GetProducts();
            foreach (var item in products)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("");
            Console.WriteLine("");
            var product = productStore.GetProducts().Last();
            product.Name = "Black Coffee";
            productStore.UpdateProduct(product);

            product = productStore.GetProducts().First();
            product.Name = "Big Orange";
            productStore.UpdateProduct(product);
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Products :");
            products = productStore.GetProducts();
            foreach (var item in products)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("");
            Console.WriteLine("");
            // remove
            product = productStore.GetProducts().First(x => x.Id == 2); // banana
            productStore.RemoveProduct(product);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Products :");
            products = productStore.GetProducts();
            foreach (var item in products)
            {
                Console.WriteLine(item.ToString());
            }

            //add product to the pass for testing
            productStore.StampToUse = DateTime.Now.AddDays(-1);
            productStore.AddProduct(new Product { Id = 5, Name = "Volvic" });
            productStore.AddProduct(new Product { Id = 6, Name = "Tonic" });

            // load product of 
            Console.WriteLine("showing old products  :", productStore.StampToUse.ToShortDateString());
            var oldProducts = productStore.GetProducts(productStore.StampToUse);
            foreach (var item in oldProducts)
            {
                Console.WriteLine(item.ToString());
            }

            // display database
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Product Database :");
            var productDb = productStore.GetDatabase();
            foreach (var item in productDb)
            {
                Console.WriteLine(item.ToString());
            }

            return productStore;
        }



    }
}