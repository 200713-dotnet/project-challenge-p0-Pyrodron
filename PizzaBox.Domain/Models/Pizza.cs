using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PizzaBox.Domain.Models {
  public class Pizza {
    string size;
    string crust;
    List<string> toppings;

    public Pizza(string sizeOfPizza, string crustType, List<string> toppingsOnPizza) {
      if (sizeOfPizza != "S" && sizeOfPizza != "M" && sizeOfPizza != "L") {
        throw new ArgumentException("SIZE_INVALID");
      }
      size = sizeOfPizza;
      foreach (string topping in toppingsOnPizza) {
        if (!Regex.IsMatch(topping.ToLower(), "anchovies|pepperoni|black olives|mushrooms|onion|pineapple|ham|bacon|sausage")) {
          throw new ArgumentException($"INVALID_TOPPING - {topping}")
        }
      }
      if (!Regex.IsMatch(crustType.ToLower(), "garlic butter|stuffed|thin|thick")) {
        throw new ArgumentException("INVALID_CRUST");
      }
    }
  }
}