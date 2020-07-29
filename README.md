# pizzabox

The goal of the project is to build a Pizza Ordering System.

## architecture (REQUIRED)

+ [solution] PizzaBox.sln
  + [project - console] PizzaBox.Client.csproj
  + [project - classlib] PizzaBox.Domain.csproj
    + [folder] Abstracts
    + [folder] Interfaces
    + [folder] Models
  + [project - classlib ] PizzaBox.Storing.csproj
    + [folder] Repositories
  + [project - xunit] PizzaBox.Testing.csproj
    + [folder] Tests

## requirements

The project should support objects of User, Store, Order, Pizza.

### store

O [required] there should exist at least 2 stores for a user to choose from
O [required] each store should be able to view/list any and all of their completed/placed orders
O [required] each store should be able to view/list any and all of their sales (amount of revenue weekly or monthly)

### order

O [required] each order must be able to view/list/edit its collection of pizzas
O [required] each order must be able to compute its pricing
O [required] each order must be limited to a total pricing of no more than $250
O [required] each order must be limited to a collection of pizzas of no more than 50

### pizza

O [required] each pizza must be able to have a crust
O [required] each pizza must be able to have a size
O [required] each pizza must be able to have toppings
O [required] each pizza must be able to compute its pricing
O [required] each pizza must have no less than 2 default toppings
O [required] each pizza must limit its toppings to no more 5

### user

O [required] must be able to view/list its order history
O [required] must be able to only order from 1 location in a 24-hour period with no reset
O [required] must be able to only order once every 2-hour period

## technologies

+ .NET Core - C#
+ .NET Core - EF + SQL
+ .NET Core - xUnit

## timelines

+ due on Jul-28 at 11p Central
+ present on Jul-29 starting at 9.30a Central
+ implement as many requirements as you can

## user story

as a user, i should be able to do this:

O access the application
O see a list of locations
O select a location
O place an order
+ with either custom or preset pizzas
+ if custom
+ select crust, size and toppings
O if preset
O select pizza and its size
O see a tally of my order
O add or remove more pizzas
O and checkout when complete with latest order
O see my order history
O make a new order

## store story

as a store, i should be able do this:

+ access the application
+ select options for order history, sales
+ if order history
+ select options for all store orders and orders associated to a user (filtering)
+ if sales
+ see pizza type, count, revenue by week or by month

> the goal is to try to complete as many reqs as you can in the time alloted. :)
