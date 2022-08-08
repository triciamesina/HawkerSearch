# Hawker Search

This solution contains both the Data Loader and Hawker Search Web UI projects.

## Set up project

### Prerequisites

This project requires .NET 6.0 SDK.

### EF Migrations

The project creates the SQL tables using Entity Framework Code First approach.
To run the Entity Framework migrations and update the database, run the following dotnet cli commands from the DataLoad.Console project directory:

```bash
dotnet ef migrations add InitialCreate -p ./HawkerSearch.Data/DataLoad.Domain.csproj -s ./DataLoad.Console/DataLoad.Console.csproj
dotnet ef database update  -p ./HawkerSearch.Data/DataLoad.Domain.csproj -s ./DataLoad.Console/DataLoad.Console.csproj
```

### Data Loader

Once the database tables are created, the Data Loader project can be run to populate the Hawkers table.
To run the Data Loader, run the following dotnet cli commands from the DataLoad.Console project directory:

```bash
dotnet run
```

## Project Structure

The repository is divided into 5 projects. Below is a brief explanation of what each project is responsible for and its components:

### DataLoad.Application

This project contains the application logic for the Data Loader application. This project references the [NetTopologySuite.IO.GeoJSON](https://nettopologysuite.github.io/NetTopologySuite/) package to parse the Geojson raw data into a Spatial Model.
- `DataLoader` is the entry point and contains controls the flow of the application. The DataLoader class depends on the Domain project (Repository) in order to make the database operations.
- `Transformer` contains the FileReader and Transformer classes which reads the raw Geojson data and transform into a Domain object.
- `Mapper` contains the logic to transform the Feature class as extracted from the Geojson input data into a Hawker Domain object.

### DataLoad.Console

This project is the main .NET Core Console Application that runs the Data Loader. This project has a reference to the DataLoader.Application project for running the DataLoader.LoadData method.

### HawkerSearch.Web

This project is the ASP.NET Core MVC Web Application that serves as the User Interface. The starting URL of the project is configured to http://localhost:5000/search .
- `Services` contains the main UI application logic and handles the calls to the Domain layer (Repository). [NetTopologySuite](https://nettopologysuite.github.io/NetTopologySuite/) Nuget package is used to make the Spatial data operations.
- `Models` contains the View Models for the UI. [AutoMapper](https://automapper.org/) is used to map the Domain Entity to the UI View Model.
- `Controllers` contains the SearchController which is the main MVC Controller for the Application.

### HawkerSearch.Domain

This project contains the Entities and Migrations created by Entity Framework. This project depends on the Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite nuget package to migrate and read the Spatial data from the Geojson input into SQL objects - [Spatial Data in Entity Framework](https://docs.microsoft.com/en-us/ef/core/modeling/spatial). This project follows the Repository pattern and contains all the database operations such as queries, insert, update etc.

### HawkerSearch.UnitTests

This project contains Unit Tests for both the DataLoader and HawkerSearch projects. This project uses Xunit to run the tests and Moq library to create fake objects.
