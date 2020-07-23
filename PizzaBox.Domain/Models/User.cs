using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models {
  public class User {
    Dictionary<Store, List<Order>> orders;
    Dictionary<Store, DateTime> lastAccessed;
    DateTime? lastOrderAdded;
    List<Store> storesToOrderFrom;

    public User(List<Store> stores) {
      orders = new Dictionary<Store, List<Order>>();
      lastAccessed = null;
      lastOrderAdded = null;
      storesToOrderFrom = new List<Store>(stores);
    }

    public bool CanOrder(Store store) {
      try {
        TimeSpan elapsed = DateTime.Now - lastAccessed[store];
        int hours = elapsed.Hours;
        if (elapsed.Hours < 2) {
          int minutes = elapsed.Minutes;
          Console.WriteLine($"You must wait {hours} hour{(hours != 2 ? "" : "s")} and {minutes} minute{(minutes != 2 ? "" : "s")} before placing another order.");
          return false;
        }
        return true;
      } catch (KeyNotFoundException) {
        return true;
      }
    }

    public List<Order> GetOrders(Store store) {
      List<Order> ordersFromStore;
      try {
        ordersFromStore = orders[store];
      } catch (KeyNotFoundException) {
        return null;
      }

      try {
        TimeSpan elapsed = DateTime.Now - lastAccessed[store];
        int hours = elapsed.Hours;
        if (hours < 24) {
          int minutes = elapsed.Minutes;
          Console.WriteLine($"You must wait {hours} hour{(hours != 2 ? "" : "s")} and {minutes} minute{(minutes != 2 ? "" : "s")} before viewing your order.");
          return null;
        }
        return ordersFromStore;
      } catch (KeyNotFoundException) {
        lastAccessed.Add(store, DateTime.Now);
        return ordersFromStore;
      }
    }

    public bool AddOrder(Store store, Order order) {
      if (lastOrderAdded == null) {
        lastOrderAdded = DateTime.Now;
      } else {
        if (!CanOrder(store)) {
          return false;
        }
      }
      List<Order> ordersFromStore;
      try {
        ordersFromStore = orders[store];
      } catch (KeyNotFoundException) {
        orders.Add(store, new List<Order>());
        ordersFromStore = orders[store];
      }
      ordersFromStore.Add(order);
      return true;
    }
  }
}