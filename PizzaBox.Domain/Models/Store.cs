using System.Collections.Generic;

namespace PizzaBox.Domain.Models {
  public class Store {
    string name;

    public string GetName() {
      return name;
    }

    public Store(string nameOfStore) {
      name = nameOfStore;
    }

    public void Visit(User user) {
      
    }

    public void Menu() {
      List<Order> ordersToAdd;
    }
  }
}