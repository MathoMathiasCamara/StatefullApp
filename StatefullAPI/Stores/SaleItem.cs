using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullAPI.Stores
{
    public class SaleItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Qty { get; set; }

        public override string ToString()
        {
            return $"Product : {Product.Name}, Qty : {Qty}";
        }
    }
}
