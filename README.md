# 🛡️ BlockedCountry API

BlockedCountry is a clean, layered ASP.NET Core Web API project designed to manage country-based IP blocking using in-memory storage. It demonstrates solid architecture, clean code practices, and integrations with external services for IP geolocation.

---

## 🚀 Features

- 🔐 Block or unblock countries permanently
- ⏳ Temporarily block countries for a custom duration
- 🌍 Lookup country info by IP using `ipgeolocation.io`
- 📵 Check if an IP is currently blocked
- 📄 Log blocked access attempts (with IP, time, user agent, etc.)
- 🔄 Paginated results for blocked countries and attempt logs
- 📦 In-memory repository — no database required

---

## 🧱 Architecture
The project follows **Clean Architecture** principles with the following layers:

```
BlockedCountry
├── BlockedCountry.API # Startup project (controllers)
├── BlockedCountry.Application # Services, interfaces, business logic
├── BlockedCountry.Domain # Core entities (e.g., TheBlockedCountry, BlockedAttempt)
├── BlockedCountry.Contracts # DTOs (data transfer objects)
├── BlockedCountry.Infrastructure # Repositories, IP services, DI
```
## 📂 Technologies

- ASP.NET Core Web API (.NET 8)
- Dependency Injection
- HttpClient & IConfiguration
- Clean Architecture
- In-memory repositories
- Swagger (OpenAPI)

---

## 🧪 API Testing

All endpoints were tested manually using **Postman**.  
Each request was validated for:

- Input validation (valid and invalid data)
- Correct HTTP status codes (`200`, `400`, `404`, `409`)
- Functional logic (e.g., block/unblock, check-block, lookup)
- Edge cases (missing fields, invalid IPs, wrong country codes)
  
## ⚙️ Setup Instructions

1. **Clone the repository**  
   ```bash
   git clone https://github.com/your-username/BlockedCountry.git
   cd BlockedCountry
