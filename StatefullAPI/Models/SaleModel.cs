using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullAPI.Models
{
    public class SaleModel : ModelBase
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public string SaleItems { get; set; }
    }
}
