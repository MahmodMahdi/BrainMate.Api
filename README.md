# 🧠 BrainMate.Api

## 📌 Overview

**BrainMate.Api** is a scalable and secure **ASP.NET Core 8 Web API** built using **Clean Architecture** and a **robust CQRS pattern**. The project is designed for enterprise-level applications with a strong focus on **maintainability**, **testability**, and **separation of concerns**.

The system supports **authentication & authorization using JWT**, **role and claim management**, **localization**, **image handling**, and is fully **Dockerized** with **Unit Testing** applied.

---

## 🏗 Architecture

The project follows **Clean Architecture** with a **modular CQRS-based design**, clearly separating responsibilities across multiple layers to ensure scalability, maintainability, and testability.

---

### 🔹 API Layer (Presentation)

Contains **8 dedicated API Controllers**, each responsible for a specific business domain:

* `AppUserController`
* `AuthController`
* `CaregiverController`
* `EventController`
* `FoodController`
* `MedicineController`
* `PatientController`
* `RelativesController`

**Responsibilities:**

* Expose RESTful endpoints
* Handle HTTP requests & responses
* Authorization & role-based access
* Delegate logic to Application layer via MediatR

---

### 🔹 Core Layer (Application + Domain Core)

This is the **heart of the system**, containing all business logic and application rules.

#### 1️⃣ Bases

* `Response<T>`
* `ResponseHandler`

Used to provide **consistent and unified API responses**.

#### 2️⃣ Behaviors

* Validation Behavior (Pipeline Behavior)
* Centralized request validation using **FluentValidation**

#### 3️⃣ Features (CQRS)

* Commands & Queries per endpoint
* Dedicated Handlers for each operation
* FluentValidation for Commands & Queries

#### 4️⃣ Mapping

* AutoMapper Profiles per Entity
* Clear separation between Entities & DTOs

#### 5️⃣ Middleware

* `ErrorHandlerMiddleware`
* Global exception handling

#### 6️⃣ Localization & Resources

* Resource files for **Arabic (ar)** and **English (en)**
* Localized responses & validation messages

#### 7️⃣ Wrappers

* Pagination Wrapper
* Standardized paging & metadata response

---

### 🔹 Data Layer (Domain + Shared Configurations)

Contains all **entities and shared domain-related configurations**.

* Base Entity with Localization Support
* Identity Entities (ASP.NET Core Identity Tables)
* Helpers:

  * Data Converters
  * Email Settings
  * JWT Settings
* JWT Authentication Responses
* Routing Helper Classes

---

### 🔹 Infrastructure Layer (Persistence)

Handles **data access and external concerns**.

* `ApplicationDbContext`
* Infrastructure Base:

  * Generic Repository Implementation
* Repository Interfaces
* Concrete Repository Implementations
* Database Migrations
* Data Seeding Classes
* Unit of Work Pattern

---

### 🔹 Service Layer

Encapsulates **business services and integrations**.

* Service Abstractions (Interfaces)
* Service Implementations
* Email Service
* Image Upload / Management Service
* Other domain-related services

---

### 🔹 Testing Layer (xUnit)

Ensures **code reliability and correctness** through automated tests.

* Core Entity Tests
* Service Layer Tests
* Test Models
* Pagination & Response Wrapper Tests

**Testing Principles:**

* AAA Pattern (Arrange / Act / Assert)
* Mocking dependencies
* Isolated and repeatable tests

---

## ⚙️ Technologies

* **ASP.NET Core 8**
* **Entity Framework Core (Code First)**
* **SQL Server**
* **JWT Authentication**
* **Docker & Docker Compose**
* **xUnit / Unit Testing**

---

## 🧩 Design Patterns & Principles

* Clean Architecture
* CQRS Pattern
* Mediator Pattern (MediatR)
* Repository Pattern
* Generic Repository
* Unit of Work
* Specification Pattern (Ready for Extension)
* SOLID Principles
* Dependency Injection
* Separation of Concerns

---

## 🔐 Security

* ASP.NET Core Identity
* JWT Authentication
* Role-based Authorization
* Claims-based Authorization
* Secure Password Hashing
* Reset Password via Email Verification Code

---

## 🌍 Features

* ✅ Authentication & Authorization
* ✅ JWT Token & Swagger Integration
* ✅ Role & Claims Management
* ✅ Localization (Arabic 🇪🇬 / English 🇺🇸)
* ✅ Pagination Schema (Reusable & Generic)
* ✅ Fluent Validation
* ✅ AutoMapper
* ✅ Data Annotations Configuration
* ✅ CORS Enabled
* ✅ Email Service (Reset Password & Notifications)
* ✅ Image Handling (Upload / Update / Delete)
* ✅ Global Exception Handling & Custom Error Responses
* ✅ API Response Wrapper (Consistent Response Structure)
* ✅ Logging & Monitoring Ready
* ✅ Clean & Versioned Endpoints Design

---

## 🧪 Testing Layer

The project includes a dedicated **Testing Layer** to ensure code quality and long-term maintainability.

### Testing Architecture

* Separate **Testing Project** aligned with Clean Architecture
* Tests are isolated from infrastructure concerns
* Uses **Arrange / Act / Assert (AAA)** pattern

### Covered Areas

* ✅ CQRS Handlers (Commands & Queries)
* ✅ Business Rules & Domain Logic
* ✅ Validation Logic (FluentValidation)
* ✅ Services (Email, Image Handling, etc.)
* ✅ Repository & Unit of Work behavior (mocked)

### Testing Tools

* xUnit
* Moq

---

## 🐳 Docker Support

The application is fully **Dockerized**:

* Multi-stage Dockerfile for optimized builds
* Ready for deployment in containerized environments
* Easy setup for local development & production

---

## 🚀 Getting Started

### Prerequisites

* .NET SDK 8
* Docker Desktop
* SQL Server

### Run Locally

```bash
dotnet restore
dotnet build
dotnet run
```

### Run with Docker

```bash
docker build -t brainmate.api .
docker run -p 8080:8080 brainmate.api
```
# Run the whole infrastructure (API + SQL Server + Logging)
docker-compose up -d
---

## 📖 API Documentation

* Swagger UI enabled
* JWT Authentication supported directly in Swagger

---

## 📬 Contact

**Author:** Mahmoud Amin
**Project:** BrainMate.Api

---

## ⭐ Notes

This project is designed as a **production-ready backend** and a strong foundation for scalable systems following modern .NET best practices.

Feel free to fork, contribute, or extend 🚀
