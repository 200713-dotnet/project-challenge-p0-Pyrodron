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
    public List<Pizza> pizzas {
      get {
        return new List<Pizza>(_pizzas);
      }
    }
    public void AddPizza(Pizza pizza) {
      bool costOver = ((decimal) pizza.cost + totalCost) > 250.00M;
      bool countOver = _pizzas.Count + 1 > 5;
      if (!costOver && !countOver) {
        _pizzas.Add(pizza);
        _totalCost += (decimal) pizza.cost;
        Console.WriteLine($"Added {pizza}");
      } else {
        Console.Write("Cannot add another pizza; ");
        if (costOver) {
          Console.Write("Order is limited to $250 ");
        }
        if (countOver) {
          Console.Write("Order is limited to 5 pizzas");
        }
        Console.WriteLine();
      }
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