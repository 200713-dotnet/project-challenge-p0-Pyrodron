using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models {
  public class Order {
    DateTime? _created = null;
    public DateTime? created {
        get {
          return _created;
        }
        set {
          if (_created == null) {
            _created = value;
          }
        }
    }

    List<Pizza> _pizzas = new List<Pizza>();
    public void AddPizza(Pizza pizza) {
      _pizzas.Add(pizza);
    }

    decimal _totalCost = -0.01M;
    public decimal totalCost {
      get {
        if (_totalCost < 0.00M) {
          return 0.00M;
        }
        return _totalCost;
      }
      set {
        if (_totalCost == -0.01M && value >= 0.00M) {
          _totalCost = value;
        }
      }
    }
    
    Store _store = null;
    public Store store {
      get {
        return _store;
      }
      set {
        if (_store == null) {
          _store = value;
        }
      }
    }

    int _user = -1;
    public int user {
      get {
        return _user;
      }
      set {
        if (_user == -1 && value >= 0) {
          _user = value;
        }
      }
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder($"{store.name}, User {user}, ${totalCost}, Order Created {_created},\nPizzas: \n");
      for (int i = 0; i < _pizzas.Count; i++) {
        sb.Append($"\t{_pizzas[i].name}\n");
      }
      return sb.ToString();
    }

    // public Order(Store storeOrderPlacedIn, User userThatPlacedOrder) {
    //   created = DateTime.Now;
    //   pizzas = new List<Pizza>();
    //   totalCost = 0.00M;
    //   store = storeOrderPlacedIn;
    //   user = userThatPlacedOrder;
    // }

    // public Order(Store storeOrderPlacedIn, User userThatPlacedOrder, DateTime orderCreated, List<Pizza> pizzasInOrder, float costOfOrder) {
    //   created = orderCreated;
    //   pizzas = pizzasInOrder;
    //   totalCost = (decimal) costOfOrder;
    //   store = storeOrderPlacedIn;
    //   user = userThatPlacedOrder;
    // }
    
    // public bool AddPizza(Pizza pizzaToAdd) {
      // if (pizzas.Count + 1 > 50) {
      //   Console.WriteLine("Could not add pizza; Order caps at 50");
      //   return false;
      // }
      // if (totalCost + pizzaToAdd.GetPrice() > 250.00M) {
      //   Console.WriteLine("Could not add pizza; Order total cannot exceed $250.00");
      //   return false;
      // }
      // pizzas.Add(pizzaToAdd);
      // return true;
      // pizzas.Add(pizzaToAdd);
      // totalCost += (decimal) pizzaToAdd.GetPrice();
      // return true;
    // }

    // public Pizza RemovePizzas(int index) {
    //   Pizza pizza;
    //   try {
    //     pizza = pizzas[index];
    //     pizzas.Remove(pizza);
    //     return pizza;
    //   } catch (IndexOutOfRangeException) {
    //     return null;
    //   }
    // }

    // public override string ToString() {
    //   return "";  // TODO
    // }

    // public DateTime GetOrderCreationDate() {
    //   return created;
    // }

    // public decimal GetTotalCost() {
      // decimal totalCost = 0.00M;
      // foreach (Pizza pizza in pizzas) {
      //   totalCost += pizza.GetPrice();
      // }
      // return totalCost;
      // return -1.00M;
    // }

    // public List<Pizza> GetOrder() {
    //   return pizzas;
    // }

    // public DateTime GetOrderPlacedTime() {
    //   return created;
    // }
  }
}