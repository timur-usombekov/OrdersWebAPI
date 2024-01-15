# Orders Web API

This is an ASP.NET Core Web API application designed to manage customer orders and order items. It uses the latest version of Entity Framework Core and follows RESTful principles.
The application handles CRUD operations for two entities: Order and OrderItem.

## Entities

The application manages two main entities:

1. **Order**: Represents customer orders with properties such as OrderId, OrderNumber, CustomerName, OrderDate, and TotalAmount.
2. **OrderItem**: Represents the items within each order with properties such as OrderItemId, OrderId, ProductName, Quantity, UnitPrice, and TotalPrice.

