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
        if (_id == -1 && value >= 0) {
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

    public bool CanOrder(User user) {
      try {
        Dictionary<int, Order> orders = _completedOrders[user.id];
        DateTime lastOrderPlaced = DateTime.MinValue;
        foreach (int orderID in orders.Keys) {
          Order order = orders[orderID];
          if (order.store == this && order.created != null && order.created > lastOrderPlaced) {
            lastOrderPlaced = (DateTime) order.created;
          }
        }
        TimeSpan elapsed = DateTime.Now - lastOrderPlaced;
        int hours = elapsed.Hours;
        if (elapsed.Hours < 2) {
          int minutes = 120 - elapsed.Minutes - (60 * elapsed.Hours);
          hours = minutes / 60;
          minutes %= 60;
          Console.WriteLine($"You must wait {hours} hour{(hours != 1 ? "" : "s")} and {minutes} minute{(minutes != 2 ? "" : "s")} before placing another order.");
          return false;
        }
        return true;
      } catch (NullReferenceException) {  // store may not have any orders placed
        return true;
      } catch (KeyNotFoundException) {  // user may not have any orders placed
        return true;
      }
    }

    public Order Visit(User user) {
      if (CanOrder(user)) {
        Order order = Menu(user);
        return order;
      } else {
        // CanOrder returning false will print a message
        return null;
      }
    }

    private void PrintOrder(Order order) {
      List<Pizza> pizzas = order.pizzas;
      Console.WriteLine($"There are {pizzas.Count} pizzas in this order");
      int i = 1;
      foreach (Pizza pizza in pizzas) {
        Console.WriteLine($"{i++}: {pizza}");
      }
    }

    private Order Menu(User user) {
      Order order = new Order{
        created = DateTime.Now,
        store = this,
        user = user.id
      };
      bool ordering = true;
      while (ordering) {
        Console.WriteLine("\nPlease select a pizza from the menu below:");
        for (int i = 0; i < menu.Length; i++) {
          Console.WriteLine($"{i + 1} - {menu[i].ToString()}");
        }
        Console.WriteLine($"{menu.Length + 1} - Finished Ordering");
        Console.WriteLine($"{menu.Length + 2} - View Order");
        Console.WriteLine($"{menu.Length + 3} - Remove Pizza From Order");
        int selection;
        if (int.TryParse(Console.ReadLine(), out selection)) {
          if (selection == menu.Length + 1) {
            ordering = false;
            break;
          } else if (selection == menu.Length + 2) {
            PrintOrder(order);
          } else if (selection == menu.Length + 3) {
            PrintOrder(order);
            Console.WriteLine("Which pizza would you like to delete from this order (type 0 to leave this menu without deleting any pizza)? ");
            bool tryAgain = true;
            while (tryAgain) {
              int pizzaToDelete;
              if (int.TryParse(Console.ReadLine(), out pizzaToDelete)) {
                if (pizzaToDelete >= 1 && pizzaToDelete <= order.pizzas.Count) {
                  order.RemovePizza(order.pizzas[pizzaToDelete - 1]);
                  tryAgain = false;
                  Console.WriteLine($"Pizza {pizzaToDelete} has been removed from the order");
                } else if (pizzaToDelete == 0) {
                  tryAgain = false;
                } else {
                  Console.WriteLine("Invalid selection. Please enter an integer that corresponds to the pizza you want to delete, or 0 if you do not want to make changes.");
                }
              } else {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
              }
            }
          } else if (selection >= 1 && selection <= menu.Length) {
            Console.WriteLine("What size pizza do you want (S/M/L)? ");
            bool tryAgain = true;
            Pizza pizza = menu[--selection];
            while (tryAgain) {
              char sizeSelection = char.ToUpper(Console.ReadKey().KeyChar);
              Console.WriteLine();
              if (sizeSelection == 'S' || sizeSelection == 'M' || sizeSelection == 'L') {
                pizza.size = sizeSelection;
                tryAgain = false;
              } else {
                Console.WriteLine("Invalid input. Must be S for small, M for medium, or L for large.");
              }
            }
            order.AddPizza(pizza);  // AddPizza will print whether successful or not
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
      List<Order> listOfOrders = new List<Order>();
      foreach (int userID in _completedOrders.Keys) {
        Dictionary<int, Order> orders = _completedOrders[userID];
        foreach (int orderID in orders.Keys) {
          listOfOrders.Add(orders[orderID]);
        }
      }
      return listOfOrders;
    }

    private void GenerateReport(int interval) {
      Dictionary<DateTime, List<Order>> dict = new Dictionary<DateTime, List<Order>>();
      List<Order> listOfOrders = GetCompletedOrders();

      foreach (Order order in listOfOrders) {
        DateTime day = order.created.Date;
        DateTime startingDay = DateTime.Now;
        if (interval == 7) {
          startingDay = day.AddDays(-((int) day.DayOfWeek));
        } else if (interval == 30) {
          startingDay = day.AddDays(-(day.Day - 1));
        }
        try {
          dict[startingDay].Add(order);
        } catch (KeyNotFoundException) {
          dict.Add(startingDay, new List<Order>{order});
        }
      }

      Console.WriteLine($"{(interval == 7 ? "Weekly" : "Monthly")} sales report for {name}:");
      foreach (DateTime startingDay in dict.Keys) {
        decimal totalSales = 0.00M;
        foreach (Order order in dict[startingDay]) {
          totalSales += order.totalCost;
        }
        string data = interval == 7 ? $"the week of {startingDay.ToLongDateString()}" : $"{startingDay.Month}/{startingDay.Year}";
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