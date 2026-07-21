# Meeting Room Booking

A simple meeting room booking application built as part of a technical coding assignment.

The application demonstrates a clean ASP.NET Core Web API backend together with an Angular frontend for managing meeting room bookings.

---

## Features

* View available meeting rooms
* View existing bookings
* Create new bookings
* Update existing bookings
* Delete bookings
* Prevent overlapping bookings for the same room
* Server-side request validation
* Centralized exception handling
* User-friendly API error responses

---

## Technology Stack

### Backend

* ASP.NET Core 8 Web API
* Entity Framework Core
* SQLite
* Swagger

### Frontend

* Angular
* TypeScript
* Angular Signals
* Reactive Forms
* HttpClient

---

## Project Structure

```
MeetingRoomBooking
│
├── backend
│   └── MeetingRoomBooking.Api
│
└── frontend
    └── meeting-room-booking-ui
```

---

## Architecture

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

The backend follows a simple layered architecture where:

* Controllers handle HTTP requests and responses.
* Services implement business logic.
* Entity Framework Core manages persistence.

Business rules remain centralized in the service layer while controllers stay lightweight.

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

The frontend uses Angular standalone components, Signals for state management, Reactive Forms for validation, and HttpClient for communication with the backend API.

---

## Business Rules

The application enforces the following rules:

* A booking must reference an existing meeting room.
* The booking start time must be earlier than the end time.
* A meeting room cannot have overlapping bookings.
* Back-to-back bookings are allowed.

---

## API Endpoints

| Method | Endpoint             | Description                |
| ------ | -------------------- | -------------------------- |
| GET    | `/api/rooms`         | Retrieve all meeting rooms |
| GET    | `/api/bookings`      | Retrieve all bookings      |
| GET    | `/api/bookings/{id}` | Retrieve a booking         |
| POST   | `/api/bookings`      | Create a booking           |
| PUT    | `/api/bookings/{id}` | Update a booking           |
| DELETE | `/api/bookings/{id}` | Delete a booking           |

---

## Running the Project

### Backend

```bash
cd backend/MeetingRoomBooking.Api

dotnet restore

dotnet ef database update

dotnet run
```

Swagger is available after the application starts:

```
https://localhost:<port>/swagger
```

---

### Frontend

```bash
cd frontend/meeting-room-booking-ui

npm install

ng serve
```

The Angular application runs on:

```
http://localhost:4200
```

---

## Design Trade-offs

This solution intentionally focuses on delivering a clean, maintainable implementation within the expected time constraints of the assignment.

To avoid unnecessary complexity, the following architectural patterns were intentionally omitted:

* CQRS
* MediatR
* Repository pattern
* AutoMapper
* Domain-Driven Design

Instead, the solution emphasizes:

* Clear separation of concerns
* Thin controllers
* Centralized business logic
* Consistent error handling
* Readable and maintainable code

---

## Future Improvements

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

## Author

**Valentine Obodoechi**
