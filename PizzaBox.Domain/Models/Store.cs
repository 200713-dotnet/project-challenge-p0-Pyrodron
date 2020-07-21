using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models {
  public class Store {
    string name;
    List<Pizza> menu;

    public string GetName() {
      return name;
    }

    public Store(string nameOfStore, List<Pizza> pizzas) {
      name = nameOfStore;
      menu = pizzas;
    }

    public void Visit(User user) {
      List<Order> orders = Menu();
    }

    public List<Order> Menu() {
      List<Order> ordersToAdd = new List<Order>();
      bool ordering = true;
      while (ordering) {
        Console.WriteLine("\nMenu:");
      }
    }
  }
}