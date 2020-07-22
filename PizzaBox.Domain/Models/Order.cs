using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models {
  public class Order {
    DateTime created;
    List<Pizza> pizzas;
    decimal totalCost;

    public Order() {
      created = DateTime.Now;
      pizzas = new List<Pizza>();
      totalCost = 0.00M;
    }

    public bool AddPizzas(List<Pizza> pizzasToOrder) {
      decimal cost = 0.00M;
      if (pizzasToOrder.Count + pizzas.Count > 50) {
        Console.WriteLine("Order exceeds 50 pizzas");
        return false;
      }
      foreach (Pizza pizza in pizzasToOrder) {
        cost += pizza.GetPrice();
      }
      if (totalCost + cost > 250.00M) {
        Console.WriteLine("Order exceeds $250.00");
        return false;
      }
      pizzas.AddRange(pizzasToOrder);
      totalCost += cost;
      return true;
    }

    public Pizza RemovePizzas(int index) {
      Pizza pizza;
      try {
        pizza = pizzas[index];
        pizzas.Remove(pizza);
        return pizza;
      } catch (IndexOutOfRangeException) {
        return null;
      }
    }

    public override string ToString() {
      return "";  // TODO
    }

    public DateTime GetOrderCreationDate() {
      return created;
    }

    public decimal GetTotalCost() {
      decimal totalCost = 0.00M;
      foreach (Pizza pizza in pizzas) {
        totalCost += pizza.GetPrice();
      }
      return totalCost;
    }

    public List<Pizza> GetOrder() {
      return GetOrder();
    }
  }
}