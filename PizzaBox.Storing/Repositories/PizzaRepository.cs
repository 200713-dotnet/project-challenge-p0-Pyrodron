using System;
using System.Collections.Generic;
using System.Linq;
using domain = PizzaBox.Domain.Models;

namespace PizzaBox.Storing.Repositories {
  // CRUD methods for database

  public class PizzaRepository {
    private PizzaProjectContext db = new PizzaProjectContext();
    Dictionary<int, List<domain.Pizza>> menu = new Dictionary<int, List<domain.Pizza>>(); // storeId, pizza list
    Dictionary<int, domain.Pizza> pizzas = new Dictionary<int, domain.Pizza>(); // pizzaId, pizza

    // store id, <user id, <order id, order>>>
    Dictionary<int, Dictionary<int, Dictionary<int, domain.Order>>> orders = 
      new Dictionary<int, Dictionary<int, Dictionary<int, domain.Order>>>();
    Dictionary<int, domain.Store> stores = new Dictionary<int, domain.Store>(); // storeID, store

    public PizzaRepository() {
      
      foreach (Store store in db.Store.ToList()) {
        stores.Add(store.Id, new domain.Store() {
          name = store.Name,
          id = store.Id,
          // add menus later
          // add orders later
        });
      }

      pizzas = ReadAllPizzas();

      // populate menus
      foreach (Menu menuItem in db.Menu.ToList()) {
        int storeID = (int) menuItem.StoreId;
        int pizzaID = (int) menuItem.PizzaId;
        try {
          menu[storeID].Add(pizzas[pizzaID]);
        } catch (KeyNotFoundException) {
          menu.Add(storeID, new List<domain.Pizza>{ pizzas[pizzaID] });
        }
      }
      
      foreach (PizzaOrder order in db.PizzaOrder.ToList()) {
        int storeID = (int) order.StoreId;
        int userID = (int) order.UserId;
        int orderID = (int) order.OrderId;
        int pizzaID = (int) order.PizzaId;
        
        Dictionary<int, Dictionary<int, domain.Order>> newStoreOrder;
        try {
          newStoreOrder = orders[storeID];
        } catch (KeyNotFoundException) {
          orders.Add(storeID, new Dictionary<int, Dictionary<int, domain.Order>>());
          newStoreOrder = orders[storeID];
        }
        Dictionary<int, domain.Order> newUserOrder;
        try {
          newUserOrder = newStoreOrder[userID];
        } catch (KeyNotFoundException) {
          newStoreOrder.Add(userID, new Dictionary<int, domain.Order>());
          newUserOrder = newStoreOrder[userID];
        }
        domain.Order domainOrder;
        try {
          domainOrder = newUserOrder[orderID];
        }  catch (KeyNotFoundException) {
            domainOrder = new domain.Order(){
              created = order.WhenOrdered,
              // add pizzas later
              totalCost = (decimal) order.TotalCost,
              store = stores[storeID],
              user = order.UserId
            };
            newUserOrder.Add(orderID, domainOrder);
        }
        domainOrder.AddPizza(pizzas[pizzaID]);
      }

      foreach (int storeID in stores.Keys) {
        try {
          stores[storeID].completedOrders = orders[storeID];
        } catch (KeyNotFoundException) {
          // a store may not have any orders placed
        }
        try {
          stores[storeID].menu = menu[storeID].ToArray();
        } catch (KeyNotFoundException) {
          // a store may not have pizzas to sell. maybe they're just selling breadsticks?
        }
      }
    }

    public Dictionary<int, domain.Pizza> ReadAllPizzas() {
      Dictionary<int, domain.Pizza> pizzas = new Dictionary<int, domain.Pizza>();
      foreach (Pizza pizza in db.Pizza.ToList()) {
        pizzas.Add(pizza.Id, new domain.Pizza(){
          id = pizza.Id,
          name = pizza.Name,
          cost = (double) pizza.Price,
          toppings = pizza.Toppings == null? new string[0] : pizza.Toppings.Split(','),
          crust = pizza.Crust
        });
      }
      return pizzas;
    }
  
    public Dictionary<int, domain.Store> GetStores() {
      return new Dictionary<int, domain.Store>(stores);
    }

    public Dictionary<int, domain.Pizza> GetPizzas() {
      return new Dictionary<int, domain.Pizza>(pizzas);
    }

    public Dictionary<int, domain.Order> GetOrders(int userID) {
      Dictionary<int, domain.Order> ordersForUser = new Dictionary<int, domain.Order>();
      foreach (int storeID in orders.Keys) {
        Dictionary<int, Dictionary<int, domain.Order>> ordersForStore = orders[storeID];
        foreach (int orderID in ordersForStore[userID].Keys) {
          ordersForUser.Add(orderID, ordersForStore[userID][orderID]);
        }
      }
      return ordersForUser;
    }

    public void AddOrder(domain.Order order) {
      foreach (domain.Pizza pizza in order.pizzas) {
        PizzaOrder dbOrder = new PizzaOrder {
          StoreId = order.store.id,
          PizzaId = pizza.id,
          UserId = order.user,
          WhenOrdered = (DateTime) order.created,
          TotalCost = (float) order.totalCost
        };
        db.PizzaOrder.Add(dbOrder);
        // db.SaveChanges();
      }
    }
  }
}