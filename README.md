# 🎓 Course Catalog Management System

A full-stack Course Catalog Management System built using **ASP.NET Core Web API (.NET 10)** and **Angular 20** following **Clean Architecture** principles.

The application supports three different user roles:

- 👨‍💼 Admin
- 👨‍🏫 Instructor
- 👨‍🎓 Student

Each role has its own dashboard and features.

---

# 🚀 Features

## 🔐 Authentication & Authorization

- JWT Authentication
- Role-Based Authorization
- Secure Login & Registration
- Password Hashing
- Protected API Endpoints

---

## 👨‍💼 Admin

- Dashboard
- View all users
- Create Instructor accounts
- View statistics
  - Total Users
  - Total Students
  - Total Instructors
  - Total Courses
  - Total Enrollments

---

## 👨‍🏫 Instructor

- Dashboard
- Create Courses
- Edit Courses
- Delete Courses
- View Own Courses
- View Students Enrolled in each Course
- Dashboard Statistics
  - My Courses
  - Total Students
  - Total Enrollments

---

## 👨‍🎓 Student

- Dashboard
- Browse Available Courses
- Search Courses
- Enroll in Courses
- Drop Courses
- Re-enroll into Dropped Courses
- View My Enrollments
- Dashboard Statistics
  - Enrolled Courses
  - Available Courses

---

# 🏗️ Architecture

Backend follows **Clean Architecture**

```
CourseCatalogAPI
│
├── API
├── Application
├── Domain
└── Infrastructure
```

### Layers

### API

- Controllers
- Middleware
- Authentication
- Dependency Injection

### Application

- DTOs
- Services
- Interfaces
- Validators
- AutoMapper Profiles
- Business Logic

### Domain

- Entities
- Enums
- Base Classes

### Infrastructure

- EF Core
- Repositories
- Database Context
- Migrations
- Seed Data

---

# 🛠 Tech Stack

## Backend

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- FluentValidation

---

## Frontend

- Angular 20
- Angular Material
- Tailwind CSS
- RxJS
- Signals
- Standalone Components

---

## Database

- SQL Server

---

# 📂 Project Structure

```
course-catalog-management-system
│
├── CourseCatalogAPI
│
│   ├── src
│   │
│   ├── CourseCatalog.API
│   ├── CourseCatalog.Application
│   ├── CourseCatalog.Domain
│   └── CourseCatalog.Infrastructure
│
└── course-catalog-ui
```

---

# 📸 Application Screens

## Login

- Secure JWT Login

## Register

- Student Registration

## Admin Dashboard

- Statistics Cards
- User Management
- Create Instructor

## Instructor Dashboard

- Dashboard Statistics
- My Courses
- Course Management
- Student List

## Student Dashboard

- Dashboard Statistics
- Browse Courses
- Enrollments

---

# ⚙️ Backend Setup

## Clone Repository

```bash
git clone https://github.com/YOUR_USERNAME/course-catalog-management-system.git
```

Go to backend

```bash
cd CourseCatalogAPI
```

Restore packages

```bash
dotnet restore
```

Update Database

```bash
dotnet ef database update \
--project src/CourseCatalog.Infrastructure \
--startup-project src/CourseCatalog.API
```

Run API

```bash
dotnet run --project src/CourseCatalog.API
```

Swagger

```
https://localhost:7247/swagger
```

---

# ⚙️ Frontend Setup

Go to frontend

```bash
cd course-catalog-ui
```

Install packages

```bash
npm install
```

Run Angular

```bash
ng serve
```

Application

```
http://localhost:4200
```

---

# 🔑 Default Accounts

## Admin

```
Email:
admin@coursecatalog.com

Password:
Admin@123
```

---

## Student

Register a new student using the Registration page.

---

## Instructor

Create an instructor using the Admin dashboard.

---

# 🧩 Key Functionalities

- JWT Authentication
- Role-Based Authorization
- Clean Architecture
- Repository Pattern
- Dependency Injection
- Fluent Validation
- AutoMapper
- Global Exception Middleware
- CRUD Operations
- Dashboard Analytics
- Course Search
- Enrollment Management
- Soft Delete Enrollment
- Re-enrollment Support

---

# 📚 Learning Outcomes

This project demonstrates:

- Clean Architecture
- ASP.NET Core Web API
- Angular Standalone Components
- Entity Framework Core
- SQL Server
- JWT Authentication
- Repository Pattern
- Dependency Injection
- AutoMapper
- Fluent Validation
- Angular Signals
- Angular Material
- Tailwind CSS
- REST APIs

---

# 📈 Future Improvements

- Email Notifications
- Course Images
- Pagination
- File Uploads
- Course Reviews
- Certificates
- Attendance Tracking
- Unit Testing
- Docker Support
- Azure Deployment
- CI/CD Pipeline

---

# 👨‍💻 Author

**Rushikesh Kanapuram**

GitHub:
https://github.com/Rushikeshkanapuram

LinkedIn:
(Add your LinkedIn profile)

---

# ⭐ If you like this project

Please consider giving it a ⭐ on GitHub.
