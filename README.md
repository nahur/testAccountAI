# ASP.NET Core Accounting System

A comprehensive, full-featured accounting software built with ASP.NET Core using a layered architecture pattern.

## 🎯 Features

### Core Modules
- ✅ **Chart of Accounts** - Hierarchical account structure with account types
- ✅ **General Ledger** - Double-entry journal entries with automatic validation
- ✅ **Invoicing** - Customer invoices with line items and tax calculation
- ✅ **Expense Tracking** - Expense management with categories and attachment support
- ✅ **Bank Reconciliation** - Match bank statements with ledger entries
- ✅ **Financial Reports** - Trial Balance, Balance Sheet, P&L Statements
- ✅ **Multi-Currency** - Support for multiple currencies with exchange rates
- ✅ **Customer & Vendor Management** - Contact management system
- ✅ **Payment Tracking** - Record and track payments

## 🏗️ Architecture

The project follows a **Layered Architecture** pattern:

```
Presentation Layer (MVC)
    ↓
Application Layer (Services & DTOs)
    ↓
Domain Layer (Entities & Interfaces)
    ↓
Infrastructure Layer (Database & Repositories)
```

### Project Structure

```
AccountingSystem/
├── src/
│   ├── AccountingSystem.Domain/              # Business entities & interfaces
│   │   ├── Entities/
│   │   │   ├── ChartOfAccount.cs
│   │   │   ├── JournalEntry.cs
│   │   │   ├── Invoice.cs
│   │   │   ├── Expense.cs
│   │   │   ├── BankAccount.cs
│   │   │   ├── Customer.cs
│   │   │   ├── Vendor.cs
│   │   │   ├── Payment.cs
│   │   │   ├── Currency.cs
│   │   │   └── ExchangeRate.cs
│   │   └── Interfaces/
│   │       └── IRepository.cs
│   ├── AccountingSystem.Application/         # Services & DTOs
│   │   ├── DTOs/
│   │   │   └── EntityDTOs.cs
│   │   ├── Services/
│   │   │   ├── ChartOfAccountService.cs
│   │   │   └── JournalEntryService.cs
│   │   └── Interfaces/
│   ├── AccountingSystem.Infrastructure/      # Database & Repositories
│   │   ├── Data/
│   │   │   ├── AccountingDbContext.cs
│   │   │   └── Migrations/
│   │   └── Repositories/
│   │       └── Repository.cs
│   └── AccountingSystem.Presentation/        # MVC UI
│       ├── Controllers/
│       │   ├── HomeController.cs
│       │   ├── ChartOfAccountsController.cs
│       │   └── JournalEntriesController.cs
│       ├── Views/
│       │   ├── Shared/
│       │   │   └── _Layout.cshtml
│       │   ├── Home/
│       │   ├── ChartOfAccounts/
│       │   └── JournalEntries/
│       ├── appsettings.json
│       ├── Program.cs
│       └── AccountingSystem.Presentation.csproj
├── tests/
│   └── AccountingSystem.Tests/
│       └── ChartOfAccountServiceTests.cs
├── AccountingSystem.sln
└── README.md
```

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server (with Entity Framework Core)
- **Frontend**: ASP.NET MVC with Razor Views
- **Testing**: XUnit with Moq
- **Architecture Pattern**: Layered Architecture
- **Data Access**: Generic Repository Pattern

## 📋 Prerequisites

- .NET 8 SDK or higher
- SQL Server (Express, Developer, or LocalDB)
- Visual Studio 2022 (Community Edition) or Visual Studio Code
- Git

## 🚀 Getting Started

### 1. Clone Repository

```bash
git clone https://github.com/nahur/testAccountAI.git
cd testAccountAI
```

### 2. Configure Database

Edit `src/AccountingSystem.Presentation/appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AccountingDB;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

**Common connection strings:**
- LocalDB: `Server=(localdb)\mssqllocaldb;Database=AccountingDB;Integrated Security=true;`
- Express: `Server=.\SQLEXPRESS;Database=AccountingDB;Integrated Security=true;`
- Named Instance: `Server=SERVERNAME\INSTANCENAME;Database=AccountingDB;Integrated Security=true;`

### 3. Restore NuGet Packages

```bash
dotnet restore
```

### 4. Create Database & Run Migrations

```bash
dotnet ef database update -p src/AccountingSystem.Infrastructure -s src/AccountingSystem.Presentation
```

### 5. Run the Application

```bash
cd src/AccountingSystem.Presentation
dotnet run
```

The application will start at `https://localhost:5001`

## 🧪 Running Tests

```bash
cd tests/AccountingSystem.Tests
dotnet test
```

## 📊 Key Features Explained

### Chart of Accounts
- Hierarchical account structure (parent-child relationships)
- Account types: Asset, Liability, Equity, Revenue, Expense
- Account codes for easy reference
- Balance calculations

### Journal Entries
- Double-entry bookkeeping enforcement
- Automatic debit/credit validation
- Entry posting to General Ledger
- Transaction history tracking

### Invoicing
- Create and manage invoices
- Line items with tax calculation
- Payment status tracking
- Invoice numbering

### Financial Reports
- **Trial Balance** - Verification of debit/credit balance
- **Balance Sheet** - Assets, Liabilities, Equity snapshot
- **P&L Statement** - Income and expenses for period

## 📁 File Structure

Each layer contains:

**Domain Layer:**
- Entity classes representing business concepts
- Repository interfaces defining data access contracts
- Business rules and validations

**Application Layer:**
- DTOs for data transfer
- Business logic services
- Service interfaces

**Infrastructure Layer:**
- Entity Framework DbContext
- Repository implementations
- Database migrations

**Presentation Layer:**
- MVC Controllers handling HTTP requests
- Razor views for UI rendering
- Bootstrap styling

## 🔄 Data Flow

```
User Request
    ↓
Controller (Presentation)
    ↓
Service (Application)
    ↓
Repository (Infrastructure)
    ↓
Database (SQL Server)
```

## 🔐 Security Considerations

- Input validation on all user inputs
- Model state validation
- SQL injection prevention through Entity Framework
- CSRF protection through ASP.NET Core middleware

*Note: Authentication/Authorization layer should be added for production use*

## 📝 Future Enhancements

- [ ] Authentication & Authorization (ASP.NET Core Identity)
- [ ] Multi-tenant support
- [ ] Advanced reporting with charts
- [ ] Bank API integration
- [ ] Email notifications
- [ ] Audit logging
- [ ] API layer (REST endpoints)
- [ ] Automated reconciliation

## 📞 Support

For issues or questions, please create a GitHub issue in the repository.

## 📄 License

MIT License - feel free to use this project for learning and development.

---

**Built with ❤️ using ASP.NET Core**