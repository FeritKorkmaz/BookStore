# Book Store

## This project includes a basic Web API application developed using .NET 6 and Entity Framework. The application is designed to create and manage books, authors, categories and users. It supports HTTP POST, GET, PUT, DELETE requests to add, delete, update and list existing products. User registration and token-based authentication are also available. In addition, a simple interface has been created on the front-end using HTML, CSS, and JavaScript. The frontend uses only book endpoints. The application is designed to perform basic CRUD (Create, Read, Update, Delete) operations and includes API endpoints and frontend components necessary to perform these operations. Authentication is not active. If you want to use it, it must be enabled in the controller section.

## Getting Started

1- Clone the project
2- Open with vs code
3- Fulfill the Prerequisites
4- To run the backend, run cd WebApi and then dotnet watch run commands in the terminal.
5- To operate the front-end and back-end together, simply press the F5 key.

### Prerequisites

1- Install .NET SDK Version 6.0.16

-Extensions

2- .NET Core Test Explorer v0.7.8

3- .NET Core Tools v0.0.4

4- .NET Install Tool for Extension Authors v1.7.1

these must be installed

## Running the tests

.NET Core Test Explorer to run tests
You can install the v0.7.8 extension and run the tests from there.

### Break down into end to end tests

Explain what these tests test and why

```
The tests are written to check that the validations we apply during processing work by sending failed data and to test that crud processes work when we send successful data. All tests are unit tests.
```

## Built With

-All libraries are used from nuget gallery. If you see any missing in WebApi.csproj file, download it. you can find it below with versions

1- AutoMapper (12.0.1)

2- AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1)

3- FluentValidation (11.6.0)

4- Microsoft.AspNetCore.Authentication.JwtBearer (6.0.21)

5- Microsoft.EntityFrameworkCore (7.0.9)

6- Microsoft.EntityFrameworkCore.InMemory (7.0.9)

7- Swashbuckle.AspNetCore (6.2.3)

8- Newtonsoft.Json (13.0.3)

## Author

- **Ferit Korkmaz** - [ferit42](https://github.com/ferit42)
