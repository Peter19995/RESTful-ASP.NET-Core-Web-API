# ASP.NET Core Web API Project

This project is based on the concepts and techniques covered in the [ASP.NET Core Web API course](https://www.udemy.com/course/restful-api-with-asp-dot-net-core-web-api/?couponCode=DNM_MAY2023). It demonstrates the development of a RESTful web service using ASP.NET Core, focusing on building and consuming APIs effectively.
<img width="488" alt="viall api" src="https://github.com/Peter19995/RESTful-ASP.NET-Core-Web-API/assets/55706749/88d796ca-4c44-4c11-86d0-57978e707e78">


## Project Overview

The main objective of this project is to showcase the implementation of a robust and scalable RESTful API using ASP.NET Core Web API. It covers various key aspects of API development, including request handling, data persistence, versioning, and documentation. The project incorporates the repository pattern along with Entity Framework for seamless interaction with a database. It also contains MVC project used to demonistrate on how to consume the API. 

## Features

- **API Design**: The project emphasizes the principles of RESTful API design, ensuring that endpoints are structured in a logical and intuitive manner.
- **HTTP Requests**: It supports the four main HTTP methods: GET, POST, PUT, and DELETE, allowing users to perform different operations on the resources exposed by the API.
- **Repository Pattern**: The project implements the repository pattern, which provides a separation between the data access logic and the business logic, making the code more maintainable and testable.
- **Entity Framework Integration**: It integrates Entity Framework to handle data persistence. The code-first approach is utilized, allowing the database schema to be generated based on the domain models and migrations.
- **Versioning**: The API incorporates versioning to support backward compatibility. This ensures that clients can continue to use older versions of the API even as it evolves and introduces new features.
- **Documentation**: The project demonstrates how to document an API effectively. Clear and concise documentation is crucial for developers who want to understand and consume the API easily.

## Getting Started

To get started with this project, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution in visual Studio.
3. Ensure that you have the necessary dependencies and tools installed, such as the .NET Core SDK.
4. Build the solution to restore the NuGet packages and compile the code.
5. Configure the database connection string in the `appsettings.json` file to point to your desired database server.
6. Run the database migrations to create the necessary tables in the database.
7. Start the API by running the project. The API will be hosted locally, and you can access it using the provided endpoints.

## Additional Resources

For further information and guidance, refer to the following resources:

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Documentation](https://docs.microsoft.com/ef/core)
- [ASP.NET Core Web API Tutorial](https://docs.microsoft.com/aspnet/core/web-api/index?view=aspnetcore-3.1)

Feel free to explore the project and enhance it further based on your needs and understanding of ASP.NET Core Web API. Happy coding!
