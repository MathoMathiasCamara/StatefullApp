using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefullApp.Models
{
    public abstract class ModelBase
    {
        public Guid Index { get; set; }
        public DateTime Stamp { get; set; }
        public State State { get; set; }
    }
}
