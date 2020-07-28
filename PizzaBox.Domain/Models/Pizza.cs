using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PizzaBox.Domain.Models {
  public class Pizza {
    string _crust = null;
    public string crust {
      get {
        return _crust;
      }
      set {
        if (_crust == null) {
          _crust = value;
        }
      }
    }

    string[] _toppings = null;
    public string[] toppings {
      get {
        return _toppings;
      }
      set {
        if (_toppings == null) {
          _toppings = value;
        }
      }
    }

    double _cost = -0.01;
    public double cost {
      get {
        return _cost;
      }
      set {
        if (_cost == -0.01 && value >= 0.00) {
          _cost = value;
        }
      }
    }

    int _id = -1;
    public int id {
      get {
        return _id;
      }
      set {
        if (_id == -1 && value >= 0) {
          _id = value;
        }
      }
    }

    string _name = null;
    public string name {
      get {
        return _name;
      }
      set {
        if (_name == null) {
          _name = value;
        }
      }
    }

    char _size = '0';
    public char size {
      get {
        return _size;
      }
      set {
        if (_size == '0') {
          _size = value;
        }
      }
    }

    public override string ToString() {
      // string size = _size == 'S' ? "Small" : _size == 'M' ? "Medium" : _size == 'L' ? "Large" : "Unknown Size";
      return $"{name} pizza with {string.Join(", ", _toppings)} and {_crust} crust - ${cost}";
    }
    // public Pizza(int pizzaID, string sizeOfPizza, string crustType, List<string> toppingsOnPizza, double cost) {
    //   id = pizzaID;
    //   if (sizeOfPizza != "S" && sizeOfPizza != "M" && sizeOfPizza != "L") {
    //     throw new ArgumentException("SIZE_INVALID");
    //   }
    //   size = sizeOfPizza;
    //   foreach (string topping in toppingsOnPizza) {
    //     if (!Regex.IsMatch(topping.ToLower(), "anchovies|pepperoni|black olives|mushrooms|onion|pineapple|ham|bacon|sausage|green peppers")) {
    //       throw new ArgumentException($"INVALID_TOPPING - {topping}");
    //     }
    //   }
    //   if (toppingsOnPizza.Count > 5) {
    //     throw new ArgumentException($"HIGH_TOPPING_COUNT");
    //   }
    //   toppings = toppingsOnPizza;
    //   if (!Regex.IsMatch(crustType.ToLower(), "garlic butter|stuffed|thin|thick")) {
    //     throw new ArgumentException("INVALID_CRUST");
    //   }
    //   crust = crustType;
    //   price = Math.Round(cost, 2, MidpointRounding.AwayFromZero);
    // }

    // public int GetPizzaID() {
    //   return id;
    // }

    // public decimal GetPrice() {
    //   return (decimal) price;
    // }

    // public override string ToString() {
    //   StringBuilder stringBuilder = new StringBuilder();
    //   stringBuilder.Append($"{size} ");
    //   for (int i = 0; i < toppings.Count; i++) {
    //     stringBuilder.Append(toppings[i]);
    //     if (i != toppings.Count - 1) {
    //       stringBuilder.Append(", ");
    //     }
    //   }
    //   if (toppings.Count == 0) {
    //     stringBuilder.Append($"cheese");
    //   }
    //   stringBuilder.Append($" pizza with {crust} crust");
    //   return stringBuilder.ToString();
    // }
  }
}