using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaBox.Domain.Models {
  public class Store {
    string name;
    List<Pizza> menu;
    Dictionary<DateTime, List<Order>> completedOrders;

    public string GetName() {
      return name;
    }

    public Store(string nameOfStore, List<Pizza> pizzas) {
      name = nameOfStore;
      menu = pizzas;
      completedOrders = new Dictionary<DateTime, List<Order>>();
    }

    public void Visit(User user) {
      if (user.CanOrder(this)) {
        Order order = Menu();
        user.AddOrder(this, order);
      }
    }

    public Order Menu() {
      Order order = new Order();
      bool ordering = true;
      while (ordering) {
        Console.WriteLine("\nPlease select a pizza from the menu below:");
        for (int i = 0; i < menu.Count; i++) {
          Console.WriteLine($"{i + 1} - {menu[i].ToString()}");
        }
        Console.WriteLine($"{menu.Count + 1} - Finished Ordering"); // let the last option allow the user to finish ordering
        int selection;
        if (int.TryParse(Console.ReadLine(), out selection)) {
          if (selection == menu.Count + 1) {
            break;
          } else if (selection >= 1 && selection <= menu.Count) {
            order.AddPizza(menu[--selection]);
            Console.WriteLine($"Added {menu[selection]}");
          } else {
            Console.WriteLine("Error: Invalid option selected. Please try again.");
          }
        } else {
          Console.WriteLine("Error: Invalid input detected. Please try again.");
        }
      }
      return order;
    }

    public List<Order> GetCompletedOrders() {
      List<Order> orders = new List<Order>();
      foreach (DateTime day in completedOrders.Keys) {
        foreach (Order order in completedOrders[day]) {
          orders.Add(order);
        }
      }
      return orders;
    }

    private void GenerateReport(int interval) {
      Dictionary<DateTime, List<Order>> dict = new Dictionary<DateTime, List<Order>>();

      foreach (DateTime day in completedOrders.Keys) {
        DateTime startingDay = day.AddDays((interval == 7 ? (int) day.DayOfWeek : (day.Day - 1)) * -1);
        try {
          dict[startingDay].AddRange(completedOrders[day]);
        } catch (KeyNotFoundException) {
          List<Order> orders = new List<Order>();
          orders.AddRange(completedOrders[day]);
          dict.Add(startingDay, orders);
        }
      }

      Console.WriteLine($"{(interval == 7 ? "Weekly" : "Monthly")} sales report for {name}:");
      foreach (DateTime startingDay in dict.Keys) {
        decimal totalSales = 0.00M;
        foreach (Order order in dict[startingDay]) {
          totalSales += order.GetTotalCost();
        }
        string data = interval == 7 ? $"the week of {startingDay.ToLongDateString()}" : $"{startingDay.Month} {startingDay.Year}";
        Console.WriteLine($"> For {data} : ${totalSales}");
      }
    }

    public void GetWeeklySalesReports() {
      GenerateReport(7);
    }

    public void GetMonthlySalesReports() {
      GenerateReport(30);
    }
  }
}