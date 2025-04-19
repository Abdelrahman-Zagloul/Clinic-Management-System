# Clinic Management System

## Project Description
The **Clinic Management System** is a console-based application designed to manage  the operations of a medical clinic. This system is built using **C#**, **Entity Framework Core**, and follows **SOLID Principles** to ensure clean, maintainable, and scalable code. This project simulates a real-life clinic system, with full support for **manager**, **doctors**, **receptionists**, **patients**, **appointments**, and more

#### This system supports the complete workflow of clinic management, including:

- Manager Control
- Doctor management
- Receptionist operations
- Patient registration and records
- Appointment booking and tracking
- Authentication & Authorization
- Role-based access and functionality

#### The project follows a **Layered Architecture** pattern, promoting separation of concerns between:

- **Presentation Layer** - Console Application
- **Business Logic Layer** - Service
- **Data Access Layer** - Repository
- **Models Layer** - Entity



## Features

####  Manager Control
- Full control over the entire system.
- Manage doctors and receptionists:
  - Add, update, and delete doctor accounts.
  - Add, update, and delete receptionist accounts.
- Generate well-formatted system reports:
  - Complete list of patients.
  - List of all registered doctors.
  - Overview of all appointments.
  - Summary of users by role (Managers, Doctors, Receptionists).
- Enforce role-based access control to ensure each user accesses only their permitted features.


#### Patient Management
- Add, update, and delete patient records.
- View detailed patient profiles and appointment history.

#### Appointment Management
- Schedule, update, and cancel appointments.
- View appointments for specific doctors or days.

#### Doctor Management
- Add, update, and manage doctor profiles.
- Assign and manage doctor schedules with available time slots.

#### Receptionist Management
- Add, update, and manage receptionist accounts.
- View and manage appointments assigned to receptionists.

#### User Management
- Secure login/logout system with authentication.
- Change password and view personal account details.

#### Reporting
- Generate clean, formatted reports for:
  - Patients
  - Doctors
  - Appointments
  - System users (Managers, Doctors, Receptionists)
    
## Technologies Used
  - **C#**: Main programming language.
  - **Entity Framework Core**: ORM for database management.
  - **LINQ**: For querying data efficiently.
  - **SQL Server**: Database used to store clinic data.
  - **SOLID Principles**: Applied to ensure maintainable and scalable code.
  - **Layered Architecture**: Used for better separation of concerns between the presentation, business logic, data access, and model layers.
