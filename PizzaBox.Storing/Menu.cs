using System;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
    public partial class Menu
    {
        public int? StoreId { get; set; }
        public int? PizzaId { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Store Store { get; set; }
    }
}
