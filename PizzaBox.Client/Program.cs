using System;
using PizzaBox.Domain.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using PizzaBox.Storing.Repositories;
using System.Linq;

namespace PizzaBox.Client {
    class Program {
        static void Main(string[] args) {
          // move database handling to PizzaStore.Storing
          //dotnet-ef dbcontext scaffold -s PizzaBox.Client/PizzaBox.Client.csproj -p PizzaBox.Storing/PizzaBox.Storing.csproj 'server=localhost;database=PizzaBoxDb;user id=sa;password=Password12345' microsoft.entityframeworkcore.sqlserver
          
          PizzaRepository pizzaDB = new PizzaRepository();
          Dictionary<int, Store> stores = pizzaDB.GetStores();
          Dictionary<int, Pizza> pizzas = pizzaDB.GetPizzas();

          User user = new User{ id = 1 };
          Dictionary<int, Order> orders = pizzaDB.GetOrders(user.id);
          foreach (int orderID in orders.Keys) {
            user.AddOrder(orders[orderID]);
          }

          bool tryAgain = true;
          while (tryAgain) {
            Console.WriteLine("Hello! Please select which store you would like to visit, or any option below.");
            int i = 1;
            int[] keys = new int[stores.Keys.Count];
            stores.Keys.CopyTo(keys, 0);
            foreach (int key in keys) {
              Console.WriteLine($"{i++} - {stores[key].name}");
            }
            Console.WriteLine($"{i++} - View Order History");
            Console.WriteLine($"{i} - Exit");
            int selection;
            if (int.TryParse(Console.ReadLine(), out selection)) {
              if (selection >= 1 && selection <= i) {
                if (selection != i && selection != i - 1) {
                  Order newOrder = stores[selection].Visit(user);

                  if (newOrder != null && newOrder.pizzas.Count != 0) {
                    pizzaDB.AddOrderToDB(newOrder);
                    user.AddOrder(newOrder);
                  } else {
                    Console.WriteLine("Empty order was not submitted");
                  }

                  bool tryAgain2 = true;
                  Console.Write("Do you want to visit another store (Y/N)? ");
                  while (tryAgain2) {
                    char response = char.ToUpper(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                    if (response == 'Y' || response == 'N') {
                      tryAgain2 = false;
                      if (response == 'N') {
                        tryAgain = false;
                      }
                    } else {
                      Console.WriteLine("Invalid input detected. Please press Y or Shift+Y for yes, or N or Shift+N for no.");
                    }
                  }
                } else if (selection == i - 1) {
                  Console.WriteLine("Order History:");
                  foreach (int orderID in orders.Keys) {
                    Order order = orders[orderID];
                    Console.WriteLine($"\t> Order #{orderID}: ");
                    foreach (Pizza pizza in order.pizzas) {
                      Console.WriteLine($"\t\t> {pizza}");
                    }
                  }
                } else {
                  tryAgain = false;
                }
              } else {
                Console.WriteLine("Invalid integer. Please enter an integer for one of the options above.");
              }
            } else {
              Console.WriteLine("Invalid input detected. Please try again.");
            }
          }
        }
    }
}
