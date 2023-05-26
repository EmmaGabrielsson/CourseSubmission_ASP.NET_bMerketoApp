# ASP:NET bMerketo App

This project is an individual assignment that includes a frontend and backend implemented in ASP.NET Core MVC.

## Frontend

The frontend is built using ASP.NET Core MVC and follows a design template provided in the course materials. The primary focus is on functionality, with multiple views and partial views implemented to create the pages according to the design template.

**Key Features:**
- Implemented multiple views and partial views for the frontend.
- Validated form inputs using JavaScript and ModelState validation.
- Categorized products into "new," "popular," and "featured" sections.
- Implemented product detail pages with detailed information.

## Backend

The backend is developed using ASP.NET Core MVC and includes its own backend system. Entity Framework Core is used for communication with a relational database.

**Key Features:**
- Stored and managed data using Entity Framework Core and a local SQL Server database file.
- Normalized the database structure to 1st to 3rd normal form.
- Implemented user authentication and authorization using Identity.
- The first registered user is automatically designated as a system administrator.
- Created a backoffice system accessible only by the system administrator.
- Implemented role management and displayed user roles in the backoffice system.
- Handled a contact form and stored form submissions.

## Dependencies

The following dependencies are required for the project:

- Microsoft.AspNetCore.Identity.EntityFrameworkCore version 7.0.5
- Microsoft.AspNetCore.Identity.UI version 7.0.5
- Microsoft.EntityFrameworkCore.SqlServer version 7.0.5
- Microsoft.EntityFrameworkCore.Tools version 7.0.5

## Running the Project

To run the project, follow these steps:

1. Clone the repository to your local machine.
2. Install the required dependencies listed above.
3. Set up a local SQL Server database or update the connection strings in the project's configuration files to point to your desired databases. Here are two files used for two databases, one to handle products and one to handle users with identity.
4. Build the project to restore NuGet packages and compile the code.
5. Run the project using the "Run" or "Debug" option in your preferred development environment.
6. Access the application in your web browser using the provided URL or localhost address.


Happy visit and enjoy exploring the project!

