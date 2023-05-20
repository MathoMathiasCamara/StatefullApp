using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullApp.Models
{
    public class SaleModel
    {
        public Guid Index { get; set; }
        public DateTime Stamp { get; set; }
        public State State { get; set; }

        public int Id { get; set; }
        public string ProductData { get; set; }
        public int Qty { get; set; }

    }
}
