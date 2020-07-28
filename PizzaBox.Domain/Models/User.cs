using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models {
  public class User {
    // Dictionary<Store, List<Order>> orders;
    // Dictionary<Store, DateTime?> lastAccessed;
    // DateTime? lastOrderAdded;
    // List<Store> storesToOrderFrom;

    int _id = -1;
    public int id {
      get {
        return _id;
      }
      set {
        if (_id == -1 && value >= 0) {
          _id = value;
        }
      }
    }
    List<Order> _orders = new List<Order>();
    public List<Order> orders {
      get {
        return new List<Order>(_orders);
      }
    }
    
    public void AddOrder(Order order) {
      _orders.Add(order);
    }
  }
}