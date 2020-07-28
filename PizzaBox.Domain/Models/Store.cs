using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models {
  public class Store {
    string _name = null;
    public string name {
      get {
        return _name;
      }
      set {
        if (_name == null) {
          _name = value;
        }
      }
    }
    int _id = -1;
    public int id {
      get {
        return _id;
      }
      set {
        if (_id == -1 && id >= 0) {
          _id = value;
        }
      }
    }
    Pizza[] _menu = null;
    public Pizza[] menu {
      get {
        return _menu;
      }
      set {
        if (_menu == null) {
          _menu = value;
        }
      }
    }
    Dictionary<int, Dictionary<int, Order>> _completedOrders = null;  // user id, <order id, order>
    public Dictionary<int, Dictionary<int, Order>> completedOrders {
      get {
        return _completedOrders;
      }
      set {
        if (_completedOrders == null) {
          _completedOrders = value;
        }
      }
    }

    // public string GetName() {
    //   return name;
    // }

    // public Store(string nameOfStore) {
    //   name = nameOfStore;
    // }

    // public void AddOrder(Order order, DateTime orderPlaced) {
      
    // }

    // public void AddPizza(Pizza pizza) {
    //   menu.Add(pizza);
    // }

    public void Visit(User user) {

      // if (user.CanOrder(this)) {
      //   Order order = Menu();
      //   if (user.AddOrder(this, order)) {
      //     Console.WriteLine("Order placed! Please note that you may need to wait up to two hours before placing another order at this store.");
      //   } else {
      //     Console.WriteLine("There was a problem placing this order.");
      //   }
      // } else {
      //   // CanOrder returning false will print a message
      // }
    }

    // public Order Menu() {
      // Order order = new Order();
      // bool ordering = true;
      // while (ordering) {
      //   Console.WriteLine("\nPlease select a pizza from the menu below:");
      //   for (int i = 0; i < menu.Count; i++) {
      //     Console.WriteLine($"{i + 1} - {menu[i].ToString()}");
      //   }
      //   Console.WriteLine($"{menu.Count + 1} - Finished Ordering"); // let the last option allow the user to finish ordering
      //   int selection;
      //   if (int.TryParse(Console.ReadLine(), out selection)) {
      //     if (selection == menu.Count + 1) {
      //       ordering = false;
      //       break;
      //     } else if (selection >= 1 && selection <= menu.Count) {
      //       order.AddPizza(menu[--selection]);
      //       Console.WriteLine($"Added {menu[selection]}");
      //     } else {
      //       Console.WriteLine("Error: Invalid option selected. Please try again.");
      //     }
      //   } else {
      //     Console.WriteLine("Error: Invalid input detected. Please try again.");
      //   }
      // }
      // return order;
    //   return null;
    // }

    // public List<Order> GetCompletedOrders() {
    //   return completedOrders;
    // }

    // private void GenerateReport(int interval) {
    //   Dictionary<DateTime, List<Order>> dict = new Dictionary<DateTime, List<Order>>();

    //   foreach (Order order in completedOrders) {
    //     DateTime day = order.GetOrderPlacedTime();
    //     DateTime startingDay = day.AddDays((interval == 7 ? (int) day.DayOfWeek : (day.Day - 1)) * -1);
    //     try {
    //       dict[startingDay].Add(order);
    //     } catch (KeyNotFoundException) {
    //       List<Order> orders = new List<Order>();
    //       orders.Add(order);
    //       dict.Add(startingDay, orders);
    //     }
    //   }

    //   Console.WriteLine($"{(interval == 7 ? "Weekly" : "Monthly")} sales report for {name}:");
    //   foreach (DateTime startingDay in dict.Keys) {
    //     decimal totalSales = 0.00M;
    //     foreach (Order order in dict[startingDay]) {
    //       totalSales += order.GetTotalCost();
    //     }
    //     string data = interval == 7 ? $"the week of {startingDay.ToLongDateString()}" : $"{startingDay.Month} {startingDay.Year}";
    //     Console.WriteLine($"> For {data} : ${totalSales}");
    //   }
    // }

    // public void GetWeeklySalesReports() {
    //   GenerateReport(7);
    // }

    // public void GetMonthlySalesReports() {
    //   GenerateReport(30);
    // }
  }
}