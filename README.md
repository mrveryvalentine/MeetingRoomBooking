# Meeting Room Booking

A simple meeting room booking application built as part of a technical coding assignment.

The application allows users to:

- View existing meeting room bookings
- Create new bookings
- Edit existing bookings
- Delete bookings
- Prevent overlapping bookings for the same room
- Display meaningful validation and error messages

---

## Technology Stack

### Backend

- ASP.NET Core 8 Web API
- Entity Framework Core
- SQLite
- Swagger

### Frontend

- Angular
- TypeScript
- Angular Signals
- Reactive Forms

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

The backend follows a simple layered architecture.

```
Controllers
    ↓
Services
    ↓
Entity Framework Core
    ↓
SQLite
```

Business logic is implemented inside the service layer while controllers remain thin and focus on HTTP concerns.

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

The frontend uses Angular standalone components, Signals for state management, and Reactive Forms for validation.

---

## Business Rule

A meeting room cannot be booked if another booking already exists for the same room during the requested time period.

If an overlap occurs, the API returns an appropriate HTTP error together with a user-friendly message.

---

## Running the Project

### Backend

```bash
cd backend/MeetingRoomBooking.Api

dotnet restore

dotnet ef database update

dotnet run
```

### Frontend

```bash
cd frontend/meeting-room-booking-ui

npm install

ng serve
```

---

## Future Improvements

Given additional time, I would consider:

- Authentication and authorization
- Calendar view
- Search and filtering
- Unit and integration tests
- Docker support
- CI/CD pipeline
- Azure deployment

---

## Author

Valentine Obodoechi