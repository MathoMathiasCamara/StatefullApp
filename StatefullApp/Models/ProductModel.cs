﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullApp.Models
{
    public  class ProductModel : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Index : {Index}, Stamp : {Stamp}, State : {State}, Id : {Id}, Name : {Name}";
        }
    }

    public enum State
    {
        Added,
        Deleted,
        Modified
    }
}
