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
            Console.WriteLine("Hello! Please select which store you would like to visit.");
            int i = 1;
            int[] keys = new int[stores.Keys.Count];
            stores.Keys.CopyTo(keys, 0);
            foreach (int key in keys) {
              Console.WriteLine($"{i++} - {stores[key].name}");
            }
            Console.WriteLine($"{i} - Exit");
            int selection;
            if (int.TryParse(Console.ReadLine(), out selection)) {
              if (selection != i) {
                Order newOrder = stores[selection].Visit(user);

                if (newOrder != null) {
                  pizzaDB.AddOrderToDB(newOrder);
                  user.AddOrder(newOrder);
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
              } else {
                tryAgain = false;
              }
            } else {
              Console.WriteLine("Invalid input detected. Please try again.");
            }
          }
        }
    }
}
