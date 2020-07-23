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
    
    public bool AddPizza(Pizza pizzaToAdd) {
      if (pizzas.Count + 1 > 50) {
        Console.WriteLine("Could not add pizza; Order caps at 50");
        return false;
      }
      if (totalCost + pizzaToAdd.GetPrice() > 250.00M) {
        Console.WriteLine("Could not add pizza; Order total cannot exceed $250.00");
        return false;
      }
      pizzas.Add(pizzaToAdd);
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