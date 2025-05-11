# 🛒 E-Commerce API Website

This is a fully functional **E-Commerce API** built with **ASP.NET Core** using **Clean Architecture**, **SOLID principles**, and **Design Patterns**. The project supports key e-commerce features, including user authentication, product management, order processing, and payment integration via **Stripe**.

## 🚀 Live API (Swagger UI)
Explore the API directly through Swagger:  
🔗 [https://my-e-commerce.runasp.net/swagger/index.html](https://my-e-commerce.runasp.net/swagger/index.html)

---

## 📦 Features

- ✅ **Clean Architecture**: Structured by domain, application, infrastructure, and API layers.
- ✅ **SOLID Principles** and **Design Patterns** applied throughout the solution.
- ✅ **JWT Authentication** for secure login and role-based access.
- ✅ **Stripe Payment Gateway** integration.
- ✅ **Redis Caching** to improve performance on frequently accessed data.
- ✅ **Entity Framework Core** with code-first migrations and SQL Server.
- ✅ **Swagger Documentation** for easy testing and exploration.
- ✅ Modular and scalable structure prepared for future microservices or extensions.

---

## 🧱 Tech Stack

- **Backend**: ASP.NET Core Web API
- **Authentication**: JWT Bearer Tokens
- **Caching**: Redis
- **Database**: SQL Server with EF Core
- **Payments**: Stripe
- **API Testing/Docs**: Swagger / Swashbuckle
- **Architecture**: Clean Architecture

---

## 📁 Project Structure

/ECommerce.API → Entry point (Controllers, Middleware, Swagger setup)
/ECommerce.Application → Business rules (Use Cases, Interfaces)
/ECommerce.Domain → Core domain models and enums
/ECommerce.Infrastructure→ External concerns (EF Core, Redis, Stripe, Repositories)
/ECommerce.Persistence → Database configuration and migrations

markdown
Copy
Edit

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Redis](https://redis.io/)
- [Stripe Account](https://stripe.com)

### Clone the repository

```bash
git clone https://github.com/your-username/ecommerce-api.git
cd ecommerce-api
Set up environment variables
Create a appsettings.Development.json and provide:

json
Copy
Edit
{
  "ConnectionStrings": {
    "DefaultConnection": "your_sql_connection"
  },
  "Jwt": {
    "Key": "your_jwt_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience",
    "DurationInMinutes": 60
  },
  "Stripe": {
    "SecretKey": "your_stripe_secret",
    "PublishableKey": "your_stripe_public"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  }
}
Run the project
bash
Copy
Edit
dotnet build
dotnet ef database update
dotnet run --project ECommerce.API
Visit https://localhost:5001/swagger or the live link.

🔧 Roadmap
 Implement CI/CD with GitHub Actions or Azure DevOps

 Add unit and integration tests

 Deploy to Azure or AWS with Docker

 Add role-based authorization

 Add email confirmation and reset password flow

🤝 Contributing
Contributions are welcome! Please open an issue first to discuss what you’d like to add or change.

📄 License
This project is licensed under the MIT License.

🙏 Acknowledgments
Thanks to the .NET community and documentation around Clean Architecture, SOLID design, and Stripe API integration.

yaml
Copy
Edit

---

Let me know if you’d like to include badges (build, license, etc.) or a sample Postman collection!
