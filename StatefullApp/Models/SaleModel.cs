﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullApp.Models
{
    public class SaleModel : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Client { get; set; }
        [Required]
        public string SaleItems { get; set; }
    }
}
