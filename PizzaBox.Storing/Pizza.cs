using System;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
    public partial class Pizza
    {
        public Pizza()
        {
            PizzaOrder = new HashSet<PizzaOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float? Price { get; set; }
        public string Toppings { get; set; }
        public string Crust { get; set; }

        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
