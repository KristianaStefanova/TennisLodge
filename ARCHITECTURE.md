# 🧱 TennisLodge Architecture Overview

This document describes the architectural structure and tier responsibilities of the **TennisLodge** ASP.NET Core web application. The solution is structured around a clean, scalable multitier design promoting modularity, reusability, and maintainability.

---

## 🎯 Design Principles

* **Separation of Concerns** – Each tier has a distinct responsibility.
* **Dependency Injection** – Uses ASP.NET Core's IoC container throughout the solution.
* **Modular Design** – Isolated components and services to allow future expansion.
* **Clean Architecture** – Business logic is kept independent from UI and data access logic.
* **AutoMapper** – Streamlined model mapping between layers.

---

## 1. Application/Presentation Tier

### 📦 Projects:

* `TennisLodge.Web`
* `TennisLodge.Web.ViewModels`
* `TennisLodge.Web.Infrastructure`

### 🎯 Purpose:

* Entry point of the application.
* Handles HTTP requests, routing, middleware, and filters.
* Prepares data for views and APIs (UI logic).
* Hosts the naming context and dependency injection setup.

### 🔗 Dependencies:

* Communicates with the Services Tier via injected services.
* Consumes Shared and ViewModel definitions.

### 📝 Notes:

* This tier can be extended with multiple frontends (e.g., Web, Mobile, API).

---

## 2. Services Tier

### 📦 Projects:

* `TennisLodge.Services.Core`
* `TennisLodge.Services.AutoMapping`
* `TennisLodge.Services.Common`

### 🎯 Purpose:

* Contains core business logic and orchestration.
* Maps entities to view models and vice versa.
* Implements reusable business rules and helpers.

### 🔗 Dependencies:

* Calls Data Tier repositories.
* Uses Shared Tier utilities.

### 📝 Notes:

* Designed for reusability across multiple Presentation Tiers.
* Follows Inversion of Control for future flexibility with different data sources.

---

## 3. Data Tier

### 📦 Projects:

* `TennisLodge.Data`
* `TennisLodge.Data.Models`
* `TennisLodge.Data.Common`

### 🎯 Purpose:

* Manages persistence logic and database access.
* Defines Entity Framework Core models and configurations.
* Provides base repository patterns and unit of work implementations.

### 🔗 Dependencies:

* Depends on EF Core and possibly third-party DB providers.
* Provides data to the Services Tier.

### 📝 Notes:

* Future extensions can include multiple DbContexts or isolated data sources.

---

## 4. Cross-Cutting / Shared Tier

### 📦 Projects:

* `TennisLodge.Common`

### 🎯 Purpose:

* Hosts utilities, constants, enums, and global helpers.
* Introduces shared concerns like logging or error handling.

### 🔗 Dependencies:

* Referenced across all other tiers.

### 📝 Notes:

* Centralized place for code reuse and consistent logic.

---

## 5. Testing Tier

### 📦 Projects:

* `TennisLodge.Web.Tests`
* `TennisLodge.Services.Core.Tests`
* `TennisLodge.IntegrationTests`

### 🎯 Purpose:

* Contains unit and integration tests.
* Verifies application behavior across layers.

### 🔗 Dependencies:

* References components under test.
* Uses NUnit or any other preferred test framework.

---

## 📁 Solution Structure (Folder Overview)

```
TennisLodge/
├── Web/                    → Controllers, views, middleware (MVC entry point)
├── Web.ViewModels/         → Data transfer objects for UI (ViewModels)
├── Web.Infrastructure/     → Filters, extensions, middlewares used in Web project
├── Web.Tests/              → Unit tests for Web logic
├── Services.Core/          → Business services, interfaces, logic
├── Services.AutoMapping/   → AutoMapper profiles and configuration
├── Services.Common/        → Service helpers, validation, and shared logic
├── Services.Core.Tests/    → Unit tests for service layer
├── Data/                   → DbContext, seeders, EF Core configuration
├── Data.Models/            → Entity classes using code-first
├── Data.Common/            → Base repositories, specifications, shared DB logic
├── Common/                 → Enums, constants, helper classes
├── IntegrationTests/       → Cross-tier integration tests
```

---

## 📌 Future Considerations

* API Tier for mobile apps.
* Microservice separation if scale demands.
* Multiple database support with segregated Data Tiers.
* Domain Events and CQRS for complex business rules.

---

This architecture ensures TennisLodge is prepared for future growth while remaining maintainable and developer-friendly.
