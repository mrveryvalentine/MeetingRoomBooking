# Technical Decisions

## Overview

This solution was designed to deliver a clean, maintainable implementation that satisfies the assignment requirements while keeping the architecture simple and easy to understand.

---

## Technology Choices

### ASP.NET Core 8

Chosen for its strong support for REST APIs, dependency injection, model validation, and middleware.

### Entity Framework Core

Used for data access and migrations. A Repository pattern was intentionally omitted since EF Core already provides repository-like capabilities through `DbContext`.

### SQLite

Selected because it requires no server installation and makes the solution easy to run locally.

### Angular

Implemented using modern Angular features including Standalone Components, Signals, Reactive Forms, and HttpClient.

---

## Architecture

The backend follows a simple layered architecture:

```
Controllers
    ↓
Services
    ↓
Entity Framework Core
    ↓
SQLite
```

Business logic is centralized in the service layer, keeping controllers focused on HTTP request handling.

---

## Validation

Validation is performed at two levels:

* Request validation using Data Annotations.
* Business validation in the service layer (room existence, valid booking times, overlapping bookings).

---

## Error Handling

A centralized `GlobalExceptionHandler` provides consistent API responses for application-specific exceptions such as:

* Invalid booking
* Room not found
* Booking not found
* Booking conflict

---

## Future Improvements

* Unit and integration tests
* Docker support
* CI/CD pipeline
* Calendar view
* Search and filtering
* Azure deployment

---

## Summary

The solution emphasizes readability, maintainability, and separation of concerns while implementing the required business rules in a straightforward and pragmatic way.