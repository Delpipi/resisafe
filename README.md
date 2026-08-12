# ResiSafe

**Secured short-term rental & verification platform for Côte d'Ivoire.**

ResiSafe connects travelers (Guests) with verified property owners (Owners) through a secure booking system with a simulated escrow payment flow, aimed at reducing rental fraud common in the Ivorian market.

🔗 **Live application:** https://resisafe-app-abbqgcckczercyhw.francecentral-01.azurewebsites.net
📄 **User guide:** see `ResiSafe_User_Guide.md`

---

## Features

- **Role-based authentication** — Guest and Owner accounts via ASP.NET Core Identity
- **Property management (CRUD)** — create, publish, and browse listings
- **Slot-based booking** — Full day, Daytime (9 AM–5 PM), Nighttime (8 PM–9 AM)
- **Double-booking prevention** — server-side check before every reservation
- **Simulated escrow** — funds are marked "held" until check-in is validated
- **Check-in via code** — moves booking status from Pending to Completed
- **Owner dashboard** — overview of all incoming reservations

---

## Tech stack

| Component      | Technology                                                  |
| -------------- | ----------------------------------------------------------- |
| Framework      | ASP.NET Core Blazor (.NET 10), Interactive Server rendering |
| Authentication | ASP.NET Core Identity (Guest/Owner roles)                   |
| Data access    | Entity Framework Core                                       |
| Database       | Azure SQL Database (SQL Server)                             |
| Styling        | Bootstrap                                                   |
| Hosting        | Azure App Service (Linux)                                   |

---

## Project structure

```
resisafe/
├── Components/
│   ├── Account/                # Identity pages (Login, Register, account management)
│   ├── Layout/                 # NavMenu, MainLayout
│   └── Pages/
│       ├── PropertyPages/      # Create, Mine, Browse, Details
│       ├── BookingsPages/      # Mine (Guest reservations)
│       └── Owner/              # Dashboard
├── Data/
│   ├── ApplicationUser.cs
│   └── ApplicationDbContext.cs
├── Models/
│   ├── Property.cs
│   └── Booking.cs
├── Migrations/
└── Program.cs
```

---

## Running the project locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A local SQL Server instance or an accessible Azure SQL instance

### Steps

```bash
git clone https://github.com/Delpipi/resisafe.git
cd resisafe
dotnet restore
```

Set your connection string in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=resisafe-db;User ID=...;Password=...;Encrypt=True;"
  }
}
```

Apply migrations and run the app:

```bash
dotnet ef database update
dotnet run
```

The application will be available at the URL printed in the terminal (e.g. `https://localhost:7xxx`).

---

## Data model

- **ApplicationUser** _(inherits from IdentityUser)_ — user accounts, role (Guest or Owner) stored via `AspNetUserRoles`
- **Property** — listing published by an Owner (`OwnerId` foreign key)
- **Booking** — reservation linking a Guest to a Property, with status (`Pending`, `Confirmed`, `Completed`, `Cancelled`), slot type (`SlotType`), and a check-in code

---

## Deployment

The application is deployed on **Azure App Service** (Linux plan, .NET 10 runtime), backed by an **Azure SQL Database**. The production connection string is injected via the App Service configuration (environment variable) and is never committed to the repository.

```bash
dotnet publish -c Release -o ./publish
az webapp deploy --resource-group <resource-group> --name <app-name> --src-path deploy.zip --type zip
```

---

## Known limitations (out of MVP scope)

- Property images are referenced by URL rather than uploaded to cloud storage (Azure Blob/Cloudinary planned for v2)
- Payment (Mobile Money / Card) is fully simulated — no real transactions occur
- Check-in validation does not yet compare the entered code against the generated one (planned for v2)
- No in-app messaging or map integration (out of scope, see project proposal)

---

## Author

Project built as part of a .NET web application development course.

## License

Academic project — for educational use only.
