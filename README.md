#  QuoteKeeper API

QuoteKeeper is a RESTful API built using **ASP.NET Core** that allows users to manage books and quotes, as well as interact with quotes through a favorites system.

The project uses PostgreSQL as the database and follows a clean architecture approach.

---

##  Features

*  JWT Authentication & Authorization
*  Book Management (CRUD operations)
*  Quote Management
*  Favorite Quotes System
*  User-based actions
*  Entity Framework Core integration
*  PostgreSQL database with Entity Framework Core

---

##  Project Structure

* **Controllers** → Handle HTTP requests
* **Services** → Business logic (QuoteService, BookService, AuthService)
* **Models** → Database entities
* **DTOs** → Data transfer objects
* **Data** → DbContext and database configuration

---

##  Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* JWT Authentication
* SQL Database
* Dependency Injection

---
## Database

This project uses PostgreSQL as the main database.

* ORM: Entity Framework Core
* Migrations are used to manage database schema
* Relationships:
   * User ↔ Books (One-to-Many)
   * User ↔ Quotes (One-to-Many)
   * User ↔ FavoriteQuotes ↔ Quotes (Many-to-Many)

---

##  Authentication

The system uses JWT tokens for secure authentication.

Token contains:

* User ID
* Email
* First Name
* Last Name

---

##  Main Functionalities

###  Books

* Create a book (with validation for unique title and barcode)
* Get all books
* Get book by ID
* Update book
* Delete book

---

###  Quotes

* Create a quote (prevents duplicates)
* Get all quotes (including book and user info)
* Get favorite quotes by user
* Get users who favorited a quote

---

###  Favorites

* Add quote to favorites
* Remove quote from favorites
* Get favorite count per quote

---

##  Validation Rules

* Prevent duplicate quotes (same text + book + user)
* Ensure unique book title
* Ensure unique barcode
* Prevent duplicate favorites

---

##  Services Overview

### QuoteService

* Create quotes
* Manage favorites
* Retrieve quote data

### BookService

* Handle CRUD operations
* Validate unique fields

### AuthService

* Generate JWT tokens
* Verify passwords

---

## ▶️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/QuoteKeeper.git
```

### 2. Configure database

Update connection string in:

```
appsettings.json
```

### 3. Run migrations

```bash
dotnet ef database update
```

### 4. Run the project

```bash
dotnet run
```

---

##  API Testing
* Postman

---

##  Future Improvements

* Add pagination for quotes
* Add comments on quotes
* Improve error handling
* Add role-based authorization

---

##  Author

**Shams AlSaffar**

