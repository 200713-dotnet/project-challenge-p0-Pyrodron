using System.Collections.Generic;

namespace PizzaBox.Domain.Models {
  public class User {
    List<Order> orders;

    public User() {
      orders = new List<Order>();
    }

    public List<Order> GetOrders() {
      return orders;
    }

    public void AddOrder(Order order) {
      orders.Add(order);
    }
  }
}