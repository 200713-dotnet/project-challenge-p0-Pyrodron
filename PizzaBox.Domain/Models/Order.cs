using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models {
  public class Order {
    DateTime? _created = null;
    public DateTime created {
        get {
          if (_created == null) {
            throw new ArgumentException("No date or time asscoiated with this order; it is required");
          }
          return (DateTime) _created;
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
    
    public void AddPizza(Pizza pizza, params bool[] supressPrinting) {
      bool costOver = ((decimal) pizza.cost + totalCost) > 250.00M;
      bool countOver = _pizzas.Count + 1 > 5;
      bool isSupressing = supressPrinting.Length > 0 ? supressPrinting[0] : false;
      string output = "";

      if (!costOver && !countOver) {
        _pizzas.Add(pizza);
        _totalCost += (decimal) pizza.cost;
        output = $"Added {pizza}";
      } else {
        output = $"Cannot add another pizza; {(costOver ? "Limited to $250 " : "")}{(countOver ? "Limited to 5 pizzas" : "")}";
      }

      if (!isSupressing) {
        Console.WriteLine(output);
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
      for (int i = 0; i < pizzas.Count; i++) {
        sb.Append($"\t{pizzas[i].name}\n");
      }
      return sb.ToString();
    }

    public void RemovePizza(Pizza pizza) {
      _pizzas.Remove(pizza);
    }
  }
}