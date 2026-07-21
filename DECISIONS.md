# Technical Decisions

This document briefly explains the architectural and technical decisions made while implementing the Meeting Room Booking application.

## Goal

The objective of this solution is not to build a production-ready booking platform, but to deliver a clean, maintainable, 
and well-structured solution that satisfies the assignment requirements within the expected time frame (2–4 hours).

The focus has been on simplicity, readability, and clear separation of responsibilities.

---

# Technology Choices

## Backend

### ASP.NET Core 8 Web API

I chose ASP.NET Core because it is the framework I have the most professional experience with. 
It provides excellent support for REST APIs, dependency injection, validation, and testing.

---

### Entity Framework Core

Entity Framework Core was selected because it provides:

* Simple data access
* Strong integration with ASP.NET Core
* Migrations support
* Fast development for small to medium applications

For this assignment I intentionally avoided introducing a Repository pattern, as EF Core already provides repository-like functionality through `DbContext`.

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

The application uses:

* Standalone Components
* Reactive Forms
* Signals
* Angular HttpClient

---

## Architecture

The application follows a simple layered architecture.

### Backend

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

This keeps controllers thin and business logic centralized.

---

### Frontend

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

Validation is performed on both the client and the server.

## Client-side

Angular Reactive Forms validate:

* Required fields
* Minimum and maximum lengths
* Invalid input before submission

This provides immediate feedback to the user.

## Server-side

The API validates business rules, including:

* Room existence
* Valid booking period
* Prevention of overlapping bookings

Server-side validation ensures data integrity even if the frontend is bypassed.

---

# Business Rule

The most important business rule is that a meeting room cannot be booked if another booking already exists for the same room during the requested time period.

If an overlap is detected, the API returns an appropriate HTTP error together with a clear error message that is displayed by the frontend.

---

# Error Handling

The API returns meaningful HTTP status codes together with descriptive messages.

Examples include:

* Invalid input
* Unknown room
* Booking conflicts

The frontend displays these messages directly to the user instead of generic error dialogs.

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

My primary objective was to produce a solution that is easy to understand, easy to maintain, and straightforward to discuss during a technical review.

Where trade-offs were necessary, I consistently preferred simplicity and readability over unnecessary abstraction.
