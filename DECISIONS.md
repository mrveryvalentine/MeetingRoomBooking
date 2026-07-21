# Technical Decisions

This document explains the architectural and technical decisions made while implementing the Meeting Room Booking application.

## Goal

The objective of this solution was not to build a production-ready booking platform, but to deliver a clean, maintainable, and well-structured solution that satisfies the assignment requirements within the expected implementation time (approximately 2–4 hours).

The focus has been on simplicity, readability, separation of concerns, and implementing the required business rules correctly.

---

# Technology Choices

## Backend

### ASP.NET Core 8 Web API

I chose ASP.NET Core because it is the framework I have the most professional experience with. It provides excellent support for REST APIs, dependency injection, model validation, middleware, and testing.

---

### Entity Framework Core

Entity Framework Core was selected because it provides:

* Simple data access
* Strong integration with ASP.NET Core
* Migration support
* Fast development for small to medium applications

For this assignment, I intentionally avoided introducing a Repository pattern, as EF Core already provides repository-like functionality through `DbContext`.

Entity configuration and seed data are organized using `IEntityTypeConfiguration<T>` implementations to keep the `DbContext` focused on persistence.

---

### SQLite

SQLite was chosen because it:

* Requires no server installation
* Is easy to clone and run
* Works well with EF Core
* Keeps the setup simple

For a larger production system, SQL Server or PostgreSQL would likely be a better choice.

---

## Frontend

### Angular

Angular was selected because it was specifically mentioned as a preferred framework in the assignment.

The frontend uses:

* Standalone Components
* Reactive Forms
* Signals
* Angular HttpClient

---

# Architecture

The application follows a simple layered architecture.

## Backend

```
Controllers
    ↓
Services
    ↓
Entity Framework Core
    ↓
SQLite
```

Responsibilities are separated as follows:

* Controllers handle HTTP requests and responses.
* Services contain business logic.
* Entity Framework Core manages persistence.

This keeps controllers thin while centralizing business rules inside the service layer.

---

## Frontend

```
Pages
    ↓
Components
    ↓
Services
    ↓
REST API
```

Responsibilities are separated as follows:

* Pages coordinate application state.
* Components focus on presentation and user interaction.
* Services communicate with the backend API.

This keeps UI components reusable and easier to maintain.

---

# Validation Strategy

Validation is performed at multiple layers.

## Request Validation

The API uses ASP.NET Core model validation through Data Annotations to validate incoming requests before they reach the service layer.

Examples include:

* Required fields
* Maximum string lengths
* Invalid request payloads

---

## Business Validation

Business rules are implemented inside the service layer, including:

* Room existence
* Valid booking period
* Prevention of overlapping bookings

Keeping business validation inside the service layer ensures these rules are enforced regardless of the client consuming the API.

---

# Business Rule

The primary business rule is that a meeting room cannot be booked if another booking already exists for the same room during the requested time period.

The overlap check allows back-to-back meetings while preventing any intersecting booking periods.

---

# Error Handling

The application uses a centralized Global Exception Handler based on ASP.NET Core's `IExceptionHandler`.

This approach keeps controllers focused on request handling while providing consistent HTTP responses across the API.

Custom exceptions include:

* `InvalidBookingException`
* `RoomNotFoundException`
* `BookingNotFoundException`
* `BookingConflictException`

These exceptions are translated into appropriate HTTP status codes such as:

* 400 Bad Request
* 404 Not Found
* 409 Conflict

This provides a consistent experience for API consumers and simplifies frontend error handling.

---

# Scope Decisions

To keep the solution aligned with the expected implementation time, I intentionally did **not** include:

* Authentication or authorization
* CQRS
* MediatR
* Repository pattern
* AutoMapper
* Domain-Driven Design
* Microservices

These patterns are valuable in larger systems but would add unnecessary complexity to this assignment.

---

# If I Had More Time

Given additional time, I would consider adding:

* Unit tests
* Integration tests
* Calendar view
* Search and filtering
* Docker support
* CI/CD pipeline
* Role-based authentication
* Azure deployment

---

# Summary

The primary objective was to produce a solution that is easy to understand, easy to maintain, and straightforward to discuss during a technical review.

Where trade-offs were necessary, I consistently preferred simplicity and readability over unnecessary abstraction while ensuring the core business requirements were implemented correctly.
