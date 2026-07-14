# Course Catalog Management System

A full-stack Course Catalog Management System developed using **ASP.NET Core Web API (.NET 10)** and **Angular 20**. The application follows **Clean Architecture** principles and provides role-based access for administrators, instructors, and students.

## Features

### Authentication
- JWT-based authentication
- Role-based authorization
- Secure login and registration
- Protected API endpoints

### Admin
- Dashboard with application statistics
- View all users
- Create instructor accounts

### Instructor
- Dashboard with course analytics
- Create, update and delete courses
- View assigned courses
- View enrolled students for each course

### Student
- Browse available courses
- Enroll in courses
- Drop enrolled courses
- Re-enroll in previously dropped courses
- View enrolled courses
- Dashboard with enrollment statistics

---

## Technology Stack

### Backend

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- FluentValidation

### Frontend

- Angular 20
- Angular Material
- Tailwind CSS
- RxJS
- Angular Signals
- Standalone Components

---

## Project Structure

```
course-catalog-management-system
│
├── CourseCatalogAPI
│   ├── CourseCatalog.API
│   ├── CourseCatalog.Application
│   ├── CourseCatalog.Domain
│   └── CourseCatalog.Infrastructure
│
└── course-catalog-ui
```

---

## Architecture

The backend is implemented using Clean Architecture.

```
Presentation (API)
        │
Application Layer
        │
Domain Layer
        │
Infrastructure Layer
```

The application separates business logic from data access, making it easier to maintain, test, and extend.

---

## Getting Started

### Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/course-catalog-management-system.git
```

---

## Backend

Navigate to the backend project.

```bash
cd CourseCatalogAPI
```

Restore packages.

```bash
dotnet restore
```

Apply database migrations.

```bash
dotnet ef database update --project src/CourseCatalog.Infrastructure --startup-project src/CourseCatalog.API
```

Run the API.

```bash
dotnet run --project src/CourseCatalog.API
```

Swagger will be available at

```
https://localhost:7247/swagger
```

---

## Frontend

Navigate to the Angular project.

```bash
cd course-catalog-ui
```

Install dependencies.

```bash
npm install
```

Run the application.

```bash
ng serve
```

The application will be available at

```
http://localhost:4200
```

---

## Application Modules

### Admin

- Dashboard
- User Management
- Create Instructor

### Instructor

- Dashboard
- Course Management
- Student Management

### Student

- Dashboard
- Browse Courses
- Enrollment Management

---

## Database

SQL Server is used as the primary database.

Main entities include:

- Users
- Courses
- Enrollments

---

## Key Concepts Demonstrated

- Clean Architecture
- Repository Pattern
- Dependency Injection
- JWT Authentication
- Role-Based Authorization
- Entity Framework Core
- RESTful API Design
- Angular Standalone Components
- Angular Signals
- Reactive Forms
- Global Exception Handling

---

## Future Enhancements

- Email notifications
- Pagination and filtering
- Docker support
- Azure deployment
- Unit and integration testing
- CI/CD pipeline
- Course image upload
- Student certificates

---


## Author

**Rushikesh Kanapuram**

GitHub: https://github.com/Rushikeshkanapuram

