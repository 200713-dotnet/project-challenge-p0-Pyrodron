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
      List<Order> orders = Menu();
    }

    public List<Order> Menu() {
      List<Order> ordersToAdd = new List<Order>();
      bool ordering = true;
      while (ordering) {
        Console.WriteLine("\nMenu:");
        ordering = false;
      }
      return ordersToAdd;
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
      Dictionary<DateTime, List<Order>> weeks = new Dictionary<DateTime, List<Order>>();

      foreach (DateTime day in completedOrders.Keys) {
        DateTime sunday = day.AddDays((int) day.DayOfWeek * -1).Date;
        try {
          weeks[sunday].AddRange(completedOrders[day]);
        } catch (KeyNotFoundException) {
          List<Order> orders = new List<Order>();
          orders.AddRange(completedOrders[day]);
          weeks.Add(sunday, orders);
        }
      }
      
      Console.WriteLine($"Weekly sales report for {name}:");
      foreach (DateTime sunday in weeks.Keys) {
        decimal totalSalesOfWeek = 0.00M;
        foreach (Order order in weeks[sunday]) {
          totalSalesOfWeek += order.GetTotalCost();
        }
        Console.WriteLine($"> For the week of {sunday.ToLongDateString()}: ${totalSalesOfWeek}");
      }
    }

    public void GetMonthlySalesReports() {
      Dictionary<DateTime, List<Order>> months = new Dictionary<DateTime, List<Order>>();

      foreach (DateTime day in completedOrders.Keys) {
        DateTime first = day.AddDays((day.Day - 1) * -1).Date;
        try {
          months[first].AddRange(completedOrders[day]);
        } catch (KeyNotFoundException) {
          List<Order> orders = new List<Order>();
          orders.AddRange(completedOrders[day]);
          months.Add(first, orders);
        }
      }
      
      Console.WriteLine($"Monthly sales report for {name}:");
      foreach (DateTime month in months.Keys) {
        decimal totalSalesOfMonth = 0.00M;
        foreach (Order order in months[month]) {
          totalSalesOfMonth += order.GetTotalCost();
        }
        Console.WriteLine($"> For {month.Month} {month.Year}: ${totalSalesOfMonth}");
      }
    }
  }
}