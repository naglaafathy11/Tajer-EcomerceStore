<div align="center">

# 🛒 Tajer ECommerceStore

### Full-Stack E-Commerce Platform built with ASP.NET Core 10 MVC

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-EF%20Core-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Identity](https://img.shields.io/badge/ASP.NET%20Identity-Auth-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)

*A production-ready e-commerce platform featuring a clean 3-Tier Architecture, full Admin Dashboard, Role-Based Authorization, and a rich customer shopping experience.*

[Demo](#) · [Report Bug](#) · [Request Feature](#)

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [ERD & Database Design](#-erd--database-design)
- [Features](#-features)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Screenshots](#-screenshots)
- [Business Logic](#-business-logic)
- [API & ViewModels](#-api--viewmodels)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)

---

## 🌟 Overview

**ECommerceStore** is a complete, production-grade e-commerce web application developed using **ASP.NET Core 9 MVC** following a strict **3-Tier Architecture** (PL → BLL → DAL). It provides a seamless shopping experience for customers and a powerful management panel for admins.

The platform supports everything from product browsing, wishlist management, a coupon system, and order tracking — to a full Admin Dashboard with revenue analytics, order lifecycle management, and user control.

---

## 🛠 Tech Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | ASP.NET Core 9 MVC |
| **ORM** | Entity Framework Core (Code-First) |
| **Database** | SQL Server |
| **Authentication** | ASP.NET Core Identity |
| **Object Mapping** | AutoMapper |
| **Frontend** | Razor Views, Bootstrap, Chart.js, Toastr |
| **Architecture** | 3-Tier (PL / BLL / DAL) + Repository Pattern + Service Layer |

---

## 🏗 Architecture

This project is organized into **3 separate class library projects** within one solution, each with a single responsibility:

```
ECommerceStore (Solution)
├── ECommerceStore.PL        ← ASP.NET Core MVC (Entry Point)
│   ├── Controllers/
│   ├── Views/
│   ├── ViewModels/
│   └── Areas/Admin/
│
├── ECommerceStore.BLL       ← Business Logic Layer
│   ├── DTOs/
│   ├── ServiceAbstractions/
│   └── ServicesImplementations/
│
└── ECommerceStore.DAL       ← Data Access Layer
    ├── Entities/
    ├── Data/                ← AppDbContext
    └── Repos/               ← Generic + Specific Repositories
```

### Layer Responsibilities

| Project | Type | Responsibility |
|---------|------|----------------|
| `ECommerceStore.PL` | ASP.NET Core MVC | Controllers, Views, ViewModels — entry point |
| `ECommerceStore.BLL` | Class Library | DTOs, Service Interfaces, Service Implementations, AutoMapper |
| `ECommerceStore.DAL` | Class Library | Entities, DbContext, Repositories, Migrations |

### Project References

```
PL  ──references──▶  BLL  ──references──▶  DAL
```

> Each layer only knows about the layer directly below it. `DAL` has no external references.

---

## 🗃 ERD & Database Design

### Entity Relationships

```
ApplicationUser (IdentityUser)
    │
    ├──[1:M]──▶ Orders
    │               └──[1:M]──▶ OrderItems ──▶ Product
    │
    ├──[1:M]──▶ ShoppingCartItems ──▶ Product
    │
    ├──[1:M]──▶ Reviews ──▶ Product
    │
    └──[1:M]──▶ Wishlists ──▶ Product

Category ──[1:M]──▶ Products
```

### BaseEntity (inherited by all entities except ApplicationUser)

| Property | Type | Notes |
|----------|------|-------|
| `Id` | `int` | Primary Key — auto increment |
| `Name` | `string` | Common identifier |
| `CreatedAt` | `DateTime` | Set automatically on creation |
| `UpdatedAt` | `DateTime?` | Nullable — set on update |

### Core Entities

<details>
<summary><strong>📦 Product</strong></summary>

| Property | Type | Notes |
|----------|------|-------|
| `Id, Name, CreatedAt, UpdatedAt` | — | From BaseEntity |
| `Description` | `string` | Product description |
| `Price` | `decimal` | Original price |
| `DiscountPrice` | `decimal?` | Nullable — price after discount |
| `StockQuantity` | `int` | Available stock |
| `CategoryId` | `int` | FK → Category |
| `ImageUrl` | `string` | Single product image |
| `IsActive` | `bool` | Show/hide product |
| `SKU` | `string` | Unique product code |

</details>

<details>
<summary><strong>🛍 Order</strong></summary>

| Property | Type | Notes |
|----------|------|-------|
| `UserId` | `string` | FK → ApplicationUser |
| `Status` | `Enum` | Pending / Processing / Shipped / Delivered / Cancelled |
| `TotalAmount` | `decimal` | Final total after discount |
| `ShippingStreet` | `string` | Street address |
| `ShippingCity` | `string` | City |
| `ShippingCountry` | `string` | Country |
| `PaymentMethod` | `Enum` | CreditCard / Cash |
| `PaymentStatus` | `Enum` | Pending / Paid / Failed / Refunded |
| `TrackingNumber` | `string?` | Nullable — set when Shipped |
| `Notes` | `string?` | Optional customer notes |

</details>

<details>
<summary><strong>🎟 Coupon</strong></summary>

| Property | Type | Notes |
|----------|------|-------|
| `Code` | `string` | Unique coupon code |
| `DiscountType` | `Enum` | Percentage / Fixed |
| `DiscountValue` | `decimal` | Amount or percentage |
| `MinOrderAmount` | `decimal` | Minimum cart value to apply |
| `ExpiryDate` | `DateTime` | Expiry date |
| `IsActive` | `bool` | Enable/disable coupon |

</details>

<details>
<summary><strong>⭐ Review</strong></summary>

| Property | Type | Notes |
|----------|------|-------|
| `ProductId` | `int` | FK → Product |
| `UserId` | `string` | FK → ApplicationUser |
| `Rating` | `int` | 1 to 5 |
| `Comment` | `string` | Review text |
| `IsApproved` | `bool` | Admin must approve before showing |

</details>

---

## ✨ Features

### 🛒 Customer Features

- **Product Browsing** — Grid layout with search, filtering by category/price/rating, sorting, and pagination (12 per page)
- **Shopping Cart** — Persistent database cart with stock validation, quantity management, and real-time total calculation
- **Coupon System** — Apply discount codes (Percentage or Fixed) with validation rules
- **Checkout** — Shipping form, payment method selection, order summary sidebar
- **Order Tracking** — Full order history with status badges and tracking numbers
- **Wishlist** — Toggle-based wishlist with direct add-to-cart from wishlist page
- **Product Reviews** — Rating system (1–5 stars) with admin approval workflow
- **User Profile** — Edit personal info and upload profile picture
- **Role-Based Auth** — Secure access with ASP.NET Core Identity (Admin / Customer roles)

### 🔧 Admin Features

- **Dashboard** — Revenue stats, order counts, new users, top 5 products table, and 30-day sales chart (Chart.js)
- **Product Management** — Full CRUD with image upload, SKU, stock control, discount pricing
- **Category Management** — CRUD with display order and active/inactive toggle
- **Order Management** — View all orders, filter by status, change order status, add tracking numbers
- **User Management** — View all users, block/unblock accounts
- **Coupon Management** — Full CRUD for discount codes
- **Review Moderation** — Approve or reject customer reviews before they go live

---

## 📁 Project Structure

```
ECommerceStore.DAL/
├── Entities/
│   ├── BaseEntity.cs
│   ├── ApplicationUser.cs
│   ├── Category.cs
│   ├── Product.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── ShoppingCartItem.cs
│   ├── Review.cs
│   ├── Coupon.cs
│   └── Wishlist.cs
├── Data/
│   └── AppDbContext.cs
└── Repos/
    ├── IGenericRepository.cs
    ├── GenericRepository.cs
    └── [Entity-specific Repos]

ECommerceStore.BLL/
├── DTOs/
├── ServiceAbstractions/
│   ├── IProductService.cs
│   ├── ICartService.cs
│   ├── IOrderService.cs
│   └── [Other Interfaces]
└── ServicesImplementations/

ECommerceStore.PL/
├── Controllers/
├── Views/
├── ViewModels/
└── Areas/
    └── Admin/
        ├── Controllers/
        └── Views/
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or full instance)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with C# extension

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/ECommerceStore.git
   cd ECommerceStore
   ```

2. **Configure the connection string**

   Open `ECommerceStore.PL/appsettings.json` and update:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceStoreDB;Trusted_Connection=True;"
     }
   }
   ```

3. **Apply migrations and seed data**

   Set `ECommerceStore.PL` as the startup project, then in Package Manager Console:
   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

4. **Run the application**
   ```bash
   dotnet run --project ECommerceStore.PL
   ```

5. **Access the app**
   - Customer: `https://localhost:5001`
   - Admin Panel: `https://localhost:5001/Admin`

### Default Admin Credentials (Seeded)

```
Email:    admin@ecommerce.com
Password: Admin@123
```

---

## 🔄 Business Logic

### Order Placement Flow (9 Steps)

```
1. Validate cart is not empty
2. Check each product IsActive = true and StockQuantity >= ordered quantity
3. If stock fails → stop entire process, return error
4. Apply coupon if provided → recalculate total
5. Create Order record (Status: Pending, PaymentStatus: Pending)
6. Create OrderItems → freeze UnitPrice at purchase time
7. Reduce StockQuantity for each product
8. Delete all cart items for this user
9. Redirect to Checkout/Confirmation
```

### Order Status Machine

```
[Pending] ──Admin──▶ [Processing] ──Admin──▶ [Shipped] ──Admin──▶ [Delivered]
    │                                              │
    └────── Customer (Pending only) ───────────────┘
    └────── Admin (up to Shipped) ─────────────────┘
                        ▼
                  [Cancelled] → Stock Restored
```

### Cart Total Calculation

```
Unit Price  = DiscountPrice ?? Price
Subtotal    = Σ (UnitPrice × Quantity)
Discount    = Percentage: Subtotal × (Value / 100)
              Fixed:      DiscountValue
Total       = max(Subtotal - Discount, 0)
```

### Review Rules

- User must have at least one **Delivered** order containing the product
- One review per user per product
- Reviews start as `IsApproved = false` — invisible until admin approves
- Average rating is computed from **approved reviews only**

---

## 📊 NuGet Packages

| Project | Packages |
|---------|---------|
| **DAL** | `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools`, `Microsoft.AspNetCore.Identity.EntityFrameworkCore` |
| **BLL** | `AutoMapper` |
| **PL** | `Microsoft.EntityFrameworkCore.Design` |

---

## 🗺 Roadmap

- [ ] Stripe / PayPal payment gateway integration
- [ ] Email notifications on order status change
- [ ] Product image gallery (multiple images per product)
- [ ] Advanced analytics with date range filtering
- [ ] REST API for mobile client
- [ ] Docker support

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create your feature branch: `git checkout -b feature/AmazingFeature`
3. Commit your changes: `git commit -m 'Add some AmazingFeature'`
4. Push to the branch: `git push origin feature/AmazingFeature`
5. Open a Pull Request

---

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.

---

<div align="center">

**Built with ❤️ using ASP.NET Core 10**

[![GitHub stars](https://img.shields.io/github/stars/your-username/ECommerceStore?style=social)](https://github.com/your-username/ECommerceStore)
[![GitHub forks](https://img.shields.io/github/forks/your-username/ECommerceStore?style=social)](https://github.com/your-username/ECommerceStore)

</div>
