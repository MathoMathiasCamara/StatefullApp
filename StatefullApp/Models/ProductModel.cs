using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullApp.Models
{
    public  class ProductModel
    {
        public Guid Index { get; set; }
        public DateTime Stamp { get; set; }
        public State State { get; set; }

        public int Id { get; set; }
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
