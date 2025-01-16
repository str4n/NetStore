# NetStore

NetStore is the sample e-commerce app built as **Modular Monolith**, written in [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

Each module is an independent **vertical slice** with its custom architecture, and the overall integration between the modules is mostly based on the **event-driven** approach with **shared contracts**.

Depending on the module complexity, different architectural styles are being used, including simple **CRUD** approach, along with **CQRS**, **Clean Architecture** and **Domain-Driven Design**.

The database is being used is [PostgreSQL](https://www.postgresql.org/) & [EntityFrameworkCore](https://learn.microsoft.com/en-us/ef/core/) as ORM.

![NetStore](https://github.com/str4n/NetStore/blob/master/NetStore.png)
<hr>

# Starting the application

Start the infrastructure using [Docker](https://www.docker.com/).

```

docker-compose up -d

```

Then start **Fake Payment Gateway**.

```

cd src/_ExternalSystems/FakePaymentGateway/
dotnet run

```

Run the **Url Shortener** (It is used to shorten links that are send via email).

```

cd src/_ExternalSystems/UrlShortener/
dotnet run

```

Finally, run the actual application.

```

cd src/Bootstrapper/NetStore.Bootstrapper/
dotnet run

```
<hr>

# Solution structure

## Bootstrapper
Web application responsible for initializing and starting all the modules - loading configurations, running DB migrations, exposing public APIs etc.

## Modules
Autonomous modules with the different set of responsibilities, decoupled from each other - there's reference between the modules and shared packages, and the synchronous communication & asynchronous integration (via events) is based on shared contracts approach.


**Customers** - managing the customers (create, complete, browse).

**Users** - managing the users/identity (register, login, permissions etc.).

**Catalogs** - managing the products, categories, brands (create, update, browse, increase stock etc.).

**Orders** - managing the orders/carts (add product to cart, checkout cart, place order etc.).

**Payments** - managing the payments for placed orders (integrated with fake payment gateway).

**Notifications** - sending notifications via email (order placed, account activation, password recovery etc).


Each module contains its own **HTTP** requests definitions file (.rest) in **/tests/Rest/** using [REST Client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client).

## Saga
Sample Saga pattern implementation for transactional handling the business processes spanning across the distinct modules.

## Shared
The set of shared components for the common abstractions & cross-cutting concerns. 
In order to achieve even better decoupling, it's split into the separate Abstractions and Infrastructure, where the former does contain public abstractions and the latter their implementation.


# Development
This year (2025) I plan to rewrite some features and add more modules(eg. Invoices).
