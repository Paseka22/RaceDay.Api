RaceDay Event Management Systems

1. Project Overview

RaceDay is an event management system developed using ASP.NET Core MVC and ASP.NET Core Web API. The system is designed to support event organisers and participants in managing and participating in sporting events.

2. Technologies Used

* C#
* ASP.NET Core 8
* ASP.NET Core MVC
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* HTML and CSS
* Bootstrap
* Docker

3. Solution Structure

The solution contains four projects:

* RaceDay.Api: Provides API functionality and database operations.
* RaceDay.Api.Tests: Contains automated tests for the API.
* RaceDay.Contracts: Contains shared contracts and data transfer objects.
* RaceDay.Mvc: Provides the web-based user interface.

4. Main Features

* User registration and login.
* Role-based access for organisers and participants.
* Event management.
* Category management.
* Participant enrolments.
* Results management.
* Participant event viewing.

5. Running the Application

1. Open the RaceDay solution in Visual Studio 2022.
2. Ensure the required database is configured.
3. Build the solution.
4. Run the RaceDay API.
5. Run the RaceDay MVC application.

6. Docker

The RaceDay MVC application includes a Dockerfile for containerisation.

Docker configuration and execution instructions will be provided as part of the deployment setup.

7. Developer

Developed as part of the PROG6212 Programming 2B practical assessment.