CREATE DATABASE PizzaProject;
GO

USE PizzaProject;
GO

CREATE SCHEMA Project;
GO

DROP TABLE Project.Store;
CREATE TABLE Project.Store(ID INT IDENTITY(1, 1) PRIMARY KEY, [Name] NVARCHAR(100));
GO

INSERT INTO Project.Store VALUES('Fricano''s Pizza'), ('Hungry Howie''s');
GO

SELECT * FROM Project.Store;

DROP TABLE Project.Pizza;
CREATE TABLE Project.Pizza(
  ID INT IDENTITY(1, 1) PRIMARY KEY,  -- make NOT NULL
  [Name] VARCHAR(100),
  Price FLOAT(2),
  Toppings VARCHAR(250),
  Crust VARCHAR(50)
);
GO

INSERT INTO Project.Pizza VALUES
  ('Supreme', 15.00, 'black olives,sausage,pepperoni,mushroom,onion', 'garlic butter'),
  ('Meatzza', 10.00, 'bacon,ham,pepperoni,sausage', 'stuffed'),
  ('Pepperoni', 5.00, 'pepperoni', 'thick'),
  ('Cheese', 2.00, NULL, 'thin');
GO

SELECT * FROM Project.Pizza;

DROP TABLE Project.Menu;
-- junction table
CREATE TABLE Project.Menu(
  StoreID INT FOREIGN KEY REFERENCES Project.Store(ID), -- make NOT NULL
  PizzaID INT FOREIGN KEY REFERENCES Project.Pizza(ID)  -- make NOT NULL
);
GO

INSERT INTO Project.Menu VALUES(1, 1), (1, 2), (1, 3), (2, 2), (2, 3), (2, 4);
GO

SELECT * FROM Project.Menu;

DROP TABLE Project.PizzaOrder;
CREATE TABLE Project.PizzaOrder(
  OrderID INT NOT NULL PRIMARY KEY,
  StoreID INT FOREIGN KEY REFERENCES Project.Store(ID),
  PizzaID INT FOREIGN KEY REFERENCES Project.Pizza(ID),
  UserID INT NOT NULL,
  WhenOrdered DATETIME NOT NULL,
  TotalCost FLOAT(2) NOT NULL
);

INSERT INTO Project.PizzaOrder VALUES(
  1, 2, 3, 1, '01/01/98 23:59:59', 1.00
);
GO