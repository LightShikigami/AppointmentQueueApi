# Appointment Queue API

## Project Overview

This ASP.NET Core Web API project provides a system to manage customer appointment queues for an agency. Customers can request appointments and receive token numbers. The system handles maximum appointment limits per day, skips weekends and public holidays, and provides endpoints to view appointment queues.

## Features

- Book a new appointment for a customer via a POST endpoint.
- Issue token numbers in the format YYYYMMDD-XX.
- Skip weekends and defined public holidays.
- Limit number of appointments per day (default: 3 ).
- List today's appointments via a GET endpoint.
- Token shifts to next day if quota full or holiday.
- In-memory database using Entity Framework Core.
- Reusable business logic via service layer.
- Swagger UI for interactive API documentation.
- Unit tests included using xUnit.

## Technologies Used

- ASP.NET Core 8.0 Web API
- Entity Framework Core
- xUnit (unit testing)
- Swashbuckle (Swagger)
- Dependency Injection (built-in)
- LINQ for data querying

## API Endpoints

- `POST /api/appointments` - Create new appointment
- `GET /api/appointments/today` - Get today's appointment queue
- `GET /api/appointments/tomorrow` - Get tomorrow's appointment queue

## Unit Tests

Included test cases:

- `CreateAppointment_ShouldAssignTokenNumber`
- `CreateAppointment_ShouldSkipWeekend`
- `CreateAppointment_ShouldShiftToNextDay_WhenQuotaExceeded`

## Getting Started

1.  Clone this repository and open the solution in Visual Studio.
2.  Restore NuGet packages: `dotnet restore`
3.  Build the application: `dotnet build`
4.  Run the Web API project: `dotnet run`
5.  Use Swagger UI or Postman to test endpoints.

## Notes

- `MaxPerDay` and holiday logic can be customized.
- Holiday list is currently hardcoded for demonstration purposes.

## Author

Salman
