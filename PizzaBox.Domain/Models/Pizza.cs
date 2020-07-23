using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PizzaBox.Domain.Models {
  public class Pizza {
    string size;
    string crust;
    List<string> toppings;
    double price;

    public Pizza(string sizeOfPizza, string crustType, List<string> toppingsOnPizza, double cost) {
      if (sizeOfPizza != "S" && sizeOfPizza != "M" && sizeOfPizza != "L") {
        throw new ArgumentException("SIZE_INVALID");
      }
      size = sizeOfPizza;
      foreach (string topping in toppingsOnPizza) {
        if (!Regex.IsMatch(topping.ToLower(), "anchovies|pepperoni|black olives|mushrooms|onion|pineapple|ham|bacon|sausage|green peppers")) {
          throw new ArgumentException($"INVALID_TOPPING - {topping}");
        }
      }
      if (toppingsOnPizza.Count > 5) {
        throw new ArgumentException($"HIGH_TOPPING_COUNT");
      }
      toppings = toppingsOnPizza;
      if (!Regex.IsMatch(crustType.ToLower(), "garlic butter|stuffed|thin|thick")) {
        throw new ArgumentException("INVALID_CRUST");
      }
      crust = crustType;
      price = Math.Round(cost, 2, MidpointRounding.AwayFromZero);
    }

    public decimal GetPrice() {
      return (decimal) price;
    }

    public override string ToString() {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append($"{size} ");
      for (int i = 0; i < toppings.Count; i++) {
        stringBuilder.Append(toppings[i]);
        if (i != toppings.Count - 1) {
          stringBuilder.Append(", ");
        }
      }
      if (toppings.Count == 0) {
        stringBuilder.Append($"cheese");
      }
      stringBuilder.Append($" pizza with {crust} crust");
      return stringBuilder.ToString();
    }
  }
}