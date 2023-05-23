using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullAPI.Models
{
    public  class ProductModel : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Index : {Index}, Stamp : {Stamp}, State : {State}, Id : {Id}, Name : {Name}";
        }
    }

    public enum State
    {
        /// <summary>
        /// A new line to add
        /// </summary>
        Added,
        /// <summary>
        /// A line to remove
        /// </summary>
        Deleted,
        /// <summary>
        /// Just like remove, sets the labels to Modified
        /// </summary>
        Modified
    }
}
