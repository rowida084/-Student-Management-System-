# Student Management System

## Overview

A console-based Student Management System built with **C#**, **.NET 8**, **Entity Framework Core**, **LINQ**, and **SQL Server**.

The project manages students, courses, instructors, and student enrollments.

---

## Technologies

* C#
* .NET 8
* Entity Framework Core 8
* SQL Server
* LINQ
* Async/Await

---

## Entities

### Student

* `StudentId` — Primary Key
* `FullName` — Required, maximum 150 characters
* `Email` — Required, maximum 150 characters, Unique
* `DateOfBirth`
* `EnrollmentDate`
* `Enrollments` — Navigation property

### Course

* `CourseId` — Primary Key
* `Title` — Required
* `Credits` — Required
* `Description` — Maximum 250 characters
* `Enrollments` — Navigation property
* `Instructors` — Navigation property

### Enrollment

`Enrollment` is an explicit join entity between `Student` and `Course`.

* `StudentId` — Foreign Key
* `CourseId` — Foreign Key
* `EnrollmentDate`
* `Grade` — Nullable
* `Student` — Navigation property
* `Course` — Navigation property

A composite primary key is used:

```csharp
HasKey(x => new { x.StudentId, x.CourseId });
```

This prevents the same student from being enrolled in the same course more than once.

### Instructor

* `InstructorId` — Primary Key
* `FullName`
* `Courses` — Navigation property

There is a **many-to-many relationship** between `Course` and `Instructor`.

---

## Relationships

```text
Student 1 ─────── * Enrollment * ─────── 1 Course

Course * ─────────────── * Instructor
```

* One Student can have many Enrollments.
* One Course can have many Enrollments.
* Each Enrollment belongs to one Student and one Course.
* A Course can have multiple Instructors.
* An Instructor can teach multiple Courses.

---

## Entity Configurations

Each entity has its own configuration class implementing:

```csharp
IEntityTypeConfiguration<T>
```

Configurations are placed in:

```text
Data/
└── Configurations/
    ├── StudentConfig.cs
    ├── CourseConfig.cs
    ├── EnrollmentConfig.cs
    └── InstructorConfig.cs
```

All configurations are applied automatically using:

```csharp
modelBuilder.ApplyConfigurationsFromAssembly(
    typeof(AppDbContext).Assembly
);
```

---

## CRUD Operations

Each main entity has asynchronous CRUD operations:

* Add
* Get By ID
* Update
* Delete

All database operations use EF Core asynchronous methods.

Examples:

```text
AddAsync()
FirstOrDefaultAsync()
ToListAsync()
SaveChangesAsync()
```

`.Result` and `.Wait()` are not used.

---

## StudentService

### Search Students By Name

Supports partial name matching.

```text
SearchByNameAsync(string name)
```

The search uses `Contains()` rather than an exact match.

### Student With Enrollments

Returns a student together with all their enrollments.

Eager loading is used with:

```csharp
Include(s => s.Enrollments)
```

`Include()` was chosen because the enrollments are known to be required when retrieving the student.

This also helps avoid the **N+1 query problem** associated with accessing related data separately for multiple entities.

---

## EnrollmentService

### Students in a Given Course

Takes a `courseId` and returns the students enrolled in that course.

```text
GetStudentsByCourseAsync(int courseId)
```

The query starts from `Enrollment` and uses the relationship to retrieve the students.

### Courses for a Given Student

Takes a `studentId` and returns the student's courses together with their grades.

```text
GetCoursesByStudentAsync(int studentId)
```

The query starts from `Enrollment` because the grade belongs to the enrollment.

### Average Grade Per Course

Calculates the average grade for a given course.

```text
GetAverageGradePerCourseAsync(int courseId)
```

`null` grades are ignored when calculating the average.

---

## AsNoTracking Decision

`AsNoTracking()` is used as a decision inside read methods, not as a separate method.

### Display-only reads

Use:

```csharp
AsNoTracking()
```

when the retrieved entities are only needed for displaying or reading data.

### Data that will be modified

Do not use:

```csharp
AsNoTracking()
```

when an entity will be modified afterward and saved using `SaveChangesAsync()`.

### Rule

```text
Display only      → AsNoTracking()
Read → Modify     → No AsNoTracking()
```

---

## Async Database Operations

All database operations are asynchronous.

### Used

```text
AddAsync()
FirstOrDefaultAsync()
SingleOrDefaultAsync()
ToListAsync()
AverageAsync()
SaveChangesAsync()
```

### Avoided

```text
.Result
.Wait()
```

This prevents blocking while waiting for database operations.

---

## Seed Data

`SeedData` provides initial sample data for:

* Students
* Courses
* Enrollments

The seed process first checks whether data already exists:

```csharp
if (context.Students.Any())
    return;
```

Students and courses are saved before creating enrollments so that their database-generated IDs are available.

```text
Students + Courses
        ↓
SaveChanges()
        ↓
Generated IDs
        ↓
Enrollments
        ↓
SaveChanges()
```

---

## Project Structure

```text
ConsoleApp5/
│
├── Data/
│   ├── AppDbContext.cs
│   ├── SeedData.cs
│   │
│   └── Configurations/
│       ├── StudentConfig.cs
│       ├── CourseConfig.cs
│       ├── EnrollmentConfig.cs
│       └── InstructorConfig.cs
│
├── Entities/
│   ├── Student.cs
│   ├── Course.cs
│   ├── Enrollment.cs
│   └── Instructor.cs
│
├── Services/
│   ├── StudentService.cs
│   ├── CourseService.cs
│   └── EnrollmentService.cs
│
├── Migrations/
│
├── appsettings.json
└── Program.cs
```

---

## Key Design Decisions

### Explicit Enrollment Entity

`Enrollment` is modeled as a separate entity because the Student-Course relationship contains additional data:

* Enrollment Date
* Grade

### Composite Primary Key

The combination of:

```text
StudentId + CourseId
```

uniquely identifies an enrollment.

### Eager Loading

`Include()` is used when related data is known to be required, helping avoid unnecessary lazy-loading queries and the N+1 problem.

### AsNoTracking

Read-only display operations use `AsNoTracking()` to avoid unnecessary change tracking.

### Async

Database operations use asynchronous EF Core methods throughout the application.
