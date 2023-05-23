using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullApp.Models
{
    public class SaleItemModel : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductData { get; set; }
        public int Qty { get; set; }
    }
}
