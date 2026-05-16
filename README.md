# CineReserve – Real-Time Movie Ticket & Seat Booking Platform

## Overview

CineReserve is a full-stack real-time movie ticket booking application developed using Angular, ASP.NET Core Web API, and SQL Server.

The platform allows users to:

* Login securely using JWT Authentication
* Browse movies dynamically
* View showtimes
* Select seats visually
* Book tickets in real time
* View booking history

Admins can:

* Add new movies
* Add showtimes
* Manage cinema content

---

# Tech Stack

## Frontend

* Angular 21
* TypeScript
* HTML5
* CSS3
* RxJS
* Angular Router
* Angular HttpClient

## Backend

* ASP.NET Core Web API
* C#
* Entity Framework Core
* JWT Authentication

## Database

* SQL Server

---

# Features

## User Features

* JWT Login Authentication
* Dynamic Movie Listing
* Showtime Selection
* Interactive Seat Booking
* VIP Seat Pricing
* Booking History
* Responsive UI

## Admin Features

* Add Movies
* Add Showtimes
* View Existing Movies
* Manage Cinema Content

---

# Project Architecture

```text
Angular Frontend
       ↓
HTTP Requests (HttpClient)
       ↓
ASP.NET Core Web API
       ↓
Entity Framework Core
       ↓
SQL Server Database
```

---

# Frontend Structure

```text
src/app
 ├── pages
 ├── components
 ├── services
 ├── guards
 ├── interceptors
 ├── models
 └── app.routes.ts
```

---

# Backend Structure

```text
Controllers
DTOs
Models
Data
```

---

# Main APIs

## Authentication

```http
POST /api/Auth/login
```

## Movies

```http
GET /api/Movies
POST /api/Movies
```

## Showtimes

```http
GET /api/Showtimes
POST /api/Showtimes
```

## Bookings

```http
POST /api/Bookings
GET /api/Bookings/user/{userId}
```

---

# Database Tables

* Users
* Movies
* Showtimes
* Bookings
* TicketDetails

---

# JWT Authentication Flow

```text
User Login
    ↓
Backend Validates Credentials
    ↓
JWT Token Generated
    ↓
Angular Stores Token
    ↓
Protected Routes Enabled
```

---

# Seat Booking Flow

```text
User Selects Showtime
        ↓
Select Seats
        ↓
Angular Sends Booking DTO
        ↓
ASP.NET Core API Validates Seats
        ↓
SQL Server Stores Booking
        ↓
Booking History Updated
```

---

# Installation & Setup

## Frontend Setup

```bash
cd frontend/cinereserve-angular
npm install
ng serve
```

Frontend runs on:

```text
http://localhost:4200
```

---

## Backend Setup

```bash
cd backend/CineReserveAPI
 dotnet restore
 dotnet run
```

Backend Swagger:

```text
https://localhost:7151/swagger
```

---

# SQL Server Configuration

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=CineReserveDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

# Important Angular Concepts Used

* Standalone Components
* Angular Routing
* Route Guards
* Interceptors
* Dependency Injection
* RxJS Streams
* HttpClient API Integration
* Two-Way Data Binding

---

# Important ASP.NET Core Concepts Used

* REST APIs
* Entity Framework Core
* JWT Authentication
* Dependency Injection
* DTOs
* Swagger API Testing
* CORS Configuration

---

# Future Enhancements

* Razorpay Payment Integration
* SignalR Real-Time Seat Locking
* QR Ticket Generation
* Email Notifications
* Admin Analytics Dashboard
* Movie Search & Filters

---

# Author

Developed by Sangamesh

---

# Conclusion

CineReserve demonstrates complete full-stack application development using Angular, ASP.NET Core Web API, and SQL Server with secure authentication, real-time booking workflow, responsive UI, and database integration.
