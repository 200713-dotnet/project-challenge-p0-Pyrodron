using System;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
    public partial class PizzaOrder
    {
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public int PizzaId { get; set; }
        public int UserId { get; set; }
        public DateTime WhenOrdered { get; set; }
        public float TotalCost { get; set; }
        public string Size { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Store Store { get; set; }
    }
}
