using System;
using PizzaBox.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.Client {
    class Program {
        static void Main(string[] args) {
          bool tryAgain = true;
          List<Pizza> pizzas = new List<Pizza>{
            new Pizza("L", "thin", new List<string>{"mushrooms", "green peppers", "pepperoni", "black olives", "sausage"}, 15.00),
            new Pizza("L", "thin", new List<string>{"pepperoni"}, 10.00),
            new Pizza("L", "thin", new List<string>{}, 5.00)
          };
          Store store1 = new Store("Fricano's Pizzareia", pizzas);
          pizzas = new List<Pizza>{
            new Pizza("S", "thin", new List<string>{"mushrooms", "green peppers", "pepperoni", "black olives", "sausage"}, 10.00),
            new Pizza("M", "thin", new List<string>{"mushrooms", "green peppers", "pepperoni", "black olives", "sausage"}, 15.00),
            new Pizza("L", "thin", new List<string>{"mushrooms", "green peppers", "pepperoni", "black olives", "sausage"}, 20.00),
            new Pizza("S", "thick", new List<string>{"pepperoni"}, 5.00),
            new Pizza("M", "thick", new List<string>{"pepperoni"}, 10.00),
            new Pizza("L", "thick", new List<string>{"pepperoni"}, 15.00),
          };
          Store store2 = new Store("Hungry Howie's", pizzas);
          User user = new User(new List<Store>{store1, store2});

          while (tryAgain) {
            Console.WriteLine("Hello! Please select which store you would like to visit.");
            Console.WriteLine($"1 - {store1.GetName()}\n2 - {store2.GetName()}\n3 - Exit");

            int selection;
            if (int.TryParse(Console.ReadLine(), out selection)) {
              if (selection >= 1 && selection <= 3) {
                tryAgain = false;
                switch (selection) {
                  case 1:
                    store1.Visit(user);
                    break;
                  case 2:
                    store1.Visit(user);
                    break;
                  case 3:
                    break;
                }
              } else {
                Console.WriteLine("Invalid option. Please try again.");
              }
            } else {
              Console.WriteLine("Error: Invalid input was detected. Please try again.");
            }
          }
        }
    }
}
