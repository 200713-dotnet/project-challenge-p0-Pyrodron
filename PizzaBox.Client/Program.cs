﻿using System;
using PizzaBox.Domain.Models;
using System.Collections.Generic;

namespace PizzaBox.Client {
    class Program {
        static void Main(string[] args) {
            bool tryAgain = true;
            User user = new User();
            List<Pizza> pizzas = new List<Pizza>{
              new Pizza("L", "thin", new List<string>("mushroom", "green pepper", "onion", "pepperoni", "black olives", "sausage")),
              new Pizza("L", "thin", new List<string>("pepperoni")),
              new Pizza("L", "thin", new List<string>())
            };
            Store store1 = new Store("Fricano's Pizzareia");
            Store store2 = new Store("Hungry Howie's");

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
