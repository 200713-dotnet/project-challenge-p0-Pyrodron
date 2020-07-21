using System;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client {
    class Program {
        static void Main(string[] args) {
            bool tryAgain = true;
            User user = new User();
            Store store1 = new Store("Fricano's Pizzareia");
            Store store2 = new Store("Giornado's Pizzareia");

            while (tryAgain) {
              Console.WriteLine("Hello! Please select which store you would like to visit.");
              Console.WriteLine($"1 - {store1.GetName()}\n2 - {store2.GetName()}\n3 - Exit");

              int selection;
              if (int.TryParse(Console.ReadLine(), out selection)) {
                switch (selection) {
                  case 1:
                    tryAgain = false;
                    store1.Visit(user);
                    break;
                  case 2:
                    tryAgain = false;
                    store2.Visit(user);
                    break;
                  case 3:
                    break;
                  default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
                }
              } else {
                Console.WriteLine("Error: An invalid input was detected. Please try again.");
              }
            }
        }
    }
}
