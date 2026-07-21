# Meeting Room Booking

A simple meeting room booking application built with **ASP.NET Core 8 Web API** and **Angular**.

The application allows users to create, update, delete and view meeting room bookings while preventing overlapping bookings for the same meeting room.

---

## Features

* View meeting rooms
* View existing bookings
* Create bookings
* Update bookings
* Delete bookings
* Prevent overlapping bookings
* Server-side validation
* Centralized exception handling

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

## Running the Application

### Backend

```bash
cd backend/MeetingRoomBooking.Api
dotnet restore
dotnet ef database update
dotnet run
```

Swagger:

```
https://localhost:<port>/swagger
```

### Frontend

```bash
cd frontend/meeting-room-booking-ui
npm install
ng serve
```

Application:

```
http://localhost:4200
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

### Frontend

```
Components
    ↓
Services
    ↓
REST API
```

Business logic is implemented in the service layer while controllers remain lightweight.

---

## Business Rules

* A booking must reference an existing room.
* Start time must be earlier than end time.
* Rooms cannot have overlapping bookings.
* Back-to-back bookings are allowed.

---

## Future Improvements

* Unit and integration tests
* Docker support
* CI/CD pipeline
* Authentication and authorization
* Calendar view and search
* Azure deployment

---

## Author

**Valentine Obodoechi**
