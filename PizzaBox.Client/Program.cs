using System;
using PizzaBox.Domain.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace PizzaBox.Client {
    class Program {
        static void Main(string[] args) {
          // move database handling to PizzaStore.Storing
          //dotnet-ef dbcontext scaffold -s PizzaBox.Client/PizzaBox.Client.csproj -p PizzaBox.Storing/PizzaBox.Storing.csproj 'server=localhost;database=PizzaBoxDb;user id=sa;password=Password12345' microsoft.entityframeworkcore.sqlserver
          SqlConnection conn = new SqlConnection();
          conn.ConnectionString = "Data Source=localhost;Initial Catalog=PizzaProject;User id=sa;Password=Passw0rd;";
          conn.Open();

          Dictionary<int, Store> stores = new Dictionary<int, Store>();
          SqlCommand command = new SqlCommand("SELECT * FROM Project.Store;", conn);
          SqlDataReader reader = command.ExecuteReader();
          while (reader.Read()) {
            stores.Add(reader.GetInt32(0), new Store(reader.GetString(1)));
          }
          reader.Close();

          Dictionary<int, Pizza> pizzas = new Dictionary<int, Pizza>();
          command.CommandText = "SELECT * FROM Project.Pizza;";
          reader = command.ExecuteReader();
          while (reader.Read()) {
            string[] toppings = new string[0];
            try {
              toppings = reader.GetString(3).Split(',');
            } catch (SqlNullValueException) {}
            
            Pizza pizza = new Pizza(reader.GetInt32(0), reader.GetString(1), reader.GetFloat(2), toppings, reader.GetString(4));
            pizzas.Add(pizza.GetID(), pizza);
          }
          reader.Close();

          command.CommandText = "SELECT * FROM Project.Menu;";
          reader = command.ExecuteReader();
          while (reader.Read()) {
            stores[reader.GetInt32(0)].AddPizza(pizzas[reader.GetInt32(1)]);
          }
          reader.Close();

          User user = new User(1);
          command.CommandText = $"SELECT * FROM Project.PizzaOrder WHERE UserID = {user.GetUserID()};";
          reader = command.ExecuteReader();
          Dictionary<int, Order> orders = new Dictionary<int, Order>();
          while (reader.Read()) {
            try {
              orders[reader.GetInt32(0)].AddPizza(pizzas[reader.GetInt32(2)]);
            } catch (KeyNotFoundException) {
              Store store = stores[reader.GetInt32(1)];
              List<Pizza> newPizzas = new List<Pizza>{
                pizzas[reader.GetInt32(2)]
              };
            Order order = new Order(store, user, reader.GetDateTime(4), newPizzas, reader.GetFloat(5));
            }
          }

          bool tryAgain = true;
          while (tryAgain) {
            Console.WriteLine("Hello! Please select which store you would like to visit.");
            int i = 1;
            int[] keys = new int[stores.Keys.Count];
            stores.Keys.CopyTo(keys, 0);
            foreach (int key in keys) {
              Console.WriteLine($"{i++} - {stores[key].GetName()}");
            }
            Console.WriteLine($"{i} - Exit");
            int selection;
            if (int.TryParse(Console.ReadLine(), out selection)) {
              if (selection != i) {
                stores[selection].Visit(user);

                bool tryAgain2 = true;
                Console.Write("Do you want to visit another store (Y/N)? ");
                while (tryAgain2) {
                  char response = char.ToUpper(Console.ReadKey().KeyChar);
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
