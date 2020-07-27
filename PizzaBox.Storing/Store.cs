using System;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
    public partial class Store
    {
        public Store()
        {
            PizzaOrder = new HashSet<PizzaOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
