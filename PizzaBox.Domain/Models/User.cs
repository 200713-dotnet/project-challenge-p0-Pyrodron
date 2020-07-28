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
    public List<Order> orders = new List<Order>();

    // public User(List<Store> stores, int userID) {
    //   orders = new Dictionary<Store, List<Order>>();
    //   lastAccessed = new Dictionary<Store, DateTime?>();
    //   foreach (Store store in stores) {
    //     lastAccessed.Add(store, null);
    //   }
    //   lastOrderAdded = null;
    //   storesToOrderFrom = new List<Store>(stores);
    //   id = userID;
    // }

    // public List<Order> GetOrders(Store store) {
    //   List<Order> ordersFromStore;
    //   try {
    //     ordersFromStore = orders[store];
    //   } catch (KeyNotFoundException) {
    //     return null;
    //   }

    //   try {
    //     TimeSpan elapsed = DateTime.Now - (DateTime) lastAccessed[store];
    //     int hours = elapsed.Hours;
    //     if (hours < 24) {
    //       int minutes = elapsed.Minutes;
    //       Console.WriteLine($"You must wait {hours} hour{(hours != 2 ? "" : "s")} and {minutes} minute{(minutes != 2 ? "" : "s")} before viewing your order.");
    //       return null;
    //     }
    //     return ordersFromStore;
    //   } catch (KeyNotFoundException) {
    //     lastAccessed.Add(store, DateTime.Now);
    //     return ordersFromStore;
    //   } catch (InvalidOperationException e) {
    //     if (e.Message.Contains("Nullable object must have a value")) {
    //       lastAccessed[store] = DateTime.Now;
    //       return ordersFromStore;
    //     } else {
    //       throw e;
    //     }
    //   }
    // }

    public bool AddOrder(Store store, Order order) {
      // if (lastOrderAdded == null) {
      //   lastOrderAdded = DateTime.Now;
      // } else {
      //   if (!CanOrder(store)) {
      //     return false;
      //   }
      // }
      // List<Order> ordersFromStore;
      // try {
      //   ordersFromStore = orders[store];
      // } catch (KeyNotFoundException) {
      //   orders.Add(store, new List<Order>());
      //   ordersFromStore = orders[store];
      // }
      // ordersFromStore.Add(order);
      // return true;
      return false;
    }
  }
}