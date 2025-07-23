# 🎾 TennisLodge

**TennisLodge** is a scalable ASP.NET Core web application that unifies tennis tournament calendars from various federations and enables players to find accommodation with local families or other players during tournament events.

This project follows a **clean multitier architecture**, ensuring scalability, testability, and separation of concerns. It was created as the final project for the `ASP.NET Advanced – June 2025` course at SoftUni.

---

## 🚀 Getting Started

### Clone the repository

```bash
git clone https://github.com/KristianaStefanova/TennisLodge.git
cd TennisLodge
```

### Restore dependencies

```bash
dotnet restore
```

### Apply database migrations

```bash
dotnet ef database update
```

### Run the application

```bash
dotnet run
```

### Open in browser

```
http://localhost:5000
```

---

## 🧩 Key Features

- 🗓 **Tournament Browser** – Filter tournaments by category, date, and location
- 🏠 **Accommodation System** – Hosts offer lodging to players; request/approval system included
- 👥 **Role-based Identity** – Players, Hosts, and Admins with custom profiles

---

## 🏛️ Architecture

TennisLodge follows a layered multitier architecture. For full technical breakdown, see [ARCHITECTURE.md](ARCHITECTURE.md).

| Tier          | Projects                                             |
|---------------|------------------------------------------------------|
| Presentation  | Web, Web.ViewModels, Web.Infrastructure              |
| Services      | Services.Core, Services.AutoMapping, Services.Common |
| Data          | Data, Data.Models, Data.Common                       |
| Shared        | Common                                               |
| Tests         | Web.Tests, Services.Tests, IntegrationTests          |

---

## 🧪 Testing

Unit and integration tests are organized under the `Tests` folder.

- `Web.Tests` – Test web layer logic and controllers
- `Services.Core.Tests` – Test business logic
- `IntegrationTests` – End-to-end tests

---

## 📄 License Notice

This project is intended solely for educational and evaluation purposes.  
No license is granted for use, modification, distribution, or commercial exploitation of this code without explicit permission from the author.

---

## 📧 Contact

📩 christiana.stefanova@gmail.com  
🌐 GitHub: https://github.com/KristianaStefanova/TennisLodge
