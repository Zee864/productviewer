
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=Zee864_productviewer) &nbsp; [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=bugs)](https://sonarcloud.io/dashboard?id=Zee864_productviewer) &nbsp; [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=ncloc)](https://sonarcloud.io/dashboard?id=Zee864_productviewer) &nbsp; &nbsp; [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=Zee864_productviewer)
# Product Viewer

This is a simple application that allows you to view products retrieved from an API along with their details.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

You will need to have the following installed on your machine:
Node.js (https://nodejs.org/en/) - JavaScript runtime environment that executes JavaScript code outside of a browser
NPM (https://www.npmjs.com/) - Package manager for JavaScript
Asp.Net Core (https://www.microsoft.com/net/download) - Framework for building web applications and services with .NET and C#

### Installing

A step by step series of examples that tell you how to get a development env running

Clone the repository to your local machine

```
git clone
```

The project is an asp.net solution that contains two projects, ProductViewer.Core and ProductViewer.Tests. The ProductViewer.Core project is the main project that contains the front end and back end code. The ProductViewer.Tests project contains the unit tests for the ProductViewer project.
The ProductViewer project is a .Net Core application that uses the React framework for the front end.

## Dependencies

To install the dependencies for the project, run the following command in the ProductViewer.Core/ClientApp directory, where the package.json file is located:

```
npm install --legacy-peer-deps
npm run prestart

```

The first command installs the dependencies for the project. The second command runs the prestart script which creates an https certificate for the application.
**Note** - The nuget packages should be restored automatically when you open the solution in Visual Studio. If they are not, you can restore them manually by right clicking on the solution in the Solution Explorer and selecting "Restore NuGet Packages".

## Running the application

After installing the dependencies, you can run the application by pressing F5 in Visual Studio. This will run the application in debug mode. You can also run the application by running the following command in the ProductViewer.Core directory:

```
dotnet watch run
```

The application will initially run on port 7032 while it configures the https certificate and launches the SPA. If you encounter a privacy warning, you can click on the "Advanced" link and then click on the "Proceed to localhost (unsafe)" link.
The application will then launch the SPA on port 44490. You can access the application by navigating to https://localhost:44490 in your browser.

## Running the tests

To run the unit tests, you can run the following command in the ProductViewer.Tests directory:

```
dotnet test
```

To view the test coverage, you can run the following command in the ProductViewer.Tests directory:

```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

This will generate a coverage.xml file in the ProductViewer.Tests directory. You can open this file in Visual Studio to view the test coverage.

## Built With

* [React](https://reactjs.org/) - The web framework used
* MUI (https://material-ui.com/) - React components that implement Google's Material Design
* Material-Table (https://material-table.com/#/) - React components for data tables
* [NPM](https://www.npmjs.com/) - Dependency Management
* [Asp.Net Core](https://www.microsoft.com/net/download) - Framework for building web applications and services with .NET and C#
* Rider - IDE used for development

# How have each of the non-functional requirements been addressed?  

## Maintainability, Structure & Readability

- Dependency Injection has been used to inject the services into the controllers. This allows for the services to be easily mocked for testing and allows for the services to be easily swapped out for different implementations.
- Interfaces have been used to define the services. This allows for the services to be easily mocked for testing and allows for the services to be easily swapped out for different implementations.
- Annotations have been used to define the routes for the controllers. This allows for the routes to be easily changed and allows for the routes to be easily documented.
- File and naming conventions have been used to make the code more readable and easier to navigate.
- Code has been abstracted into separate methods to make the code more readable and easier to navigate.
- JsDoc and XML comments have been used to document the code.
- The code has been formatted using the default Rider formatting settings.
- The code has been linted using the default Rider linting settings.

## Reliability & Error Handling

- The application has been tested using unit tests. The unit tests have been written using the NUnit framework and the Moq framework. The unit tests have been written to test the services and the controllers.
- Logging has been implemented using the Serilog framework. The logs are written to a file in the logs directory.
- Try/catch blocks have been used to catch any exceptions that are thrown by the services and controllers. The exceptions are logged and an error response is returned to the client.

## Standards & Extensibility

- IOC principles have been used to allow for the services to be easily mocked for testing and allows for the services to be easily swapped out for different implementations.
- Explicit Dependency principle has been used to allow methods and classes to explicitly define their dependencies. This allows for identification of dependencies and allows for the dependencies to be injected via the constructor.
- SOLID principles was used to ensure that the code is easily maintainable and extensible.
- DRY principle has been used to allow code to be written using reusable methods.
- Single responsibility principle has been used to logically separate the code into separate classes and methods allowing extensibility. 
- The application has been written using the .Net Core framework. This allows the application to be run on multiple platforms.
- The application has been written using the React framework. This allows the application to be easily extended to include more features.
- The application has been written using the MUI framework which allows for the application to be easily styled using Google's Material Design.
- The application has been written using the Material-Table framework allowing for the implementation of a data table with sorting, filtering and pagination.
- MVC has been used to separate the front end and back end code. This allows the front end and back end code to be easily extended to include more features.

## Efficiency & Performance

- Response compression has been enabled to reduce the size of the response. This allows for the response to be sent more quickly and reduces the bandwidth required to send the response. 
- Response catching is used to reduce the number of requests made to the API allowing for greater efficiency and performance.
- Asynchronous programming has been used where possible to allow the application to run processes in parallel leading to greater efficiency and performance.
- Inline methods have been used to remove the need for the compiler to create a delegate for the method. This allows better performance as there is no overhead in terms of memory allocation or preserving method call information in the memory. 
- HttpClient has been reused by passing it into the services via dependency injection. This allows for the HttpClient to be reused and reduces the overhead of creating a new HttpClient for each request.

## Testability & Automated Testing

- Unit tests have been created with a 100% code coverage.
- All of the services have been mocked using the Moq framework. This allows for the services to be easily tested without having to make any external API calls.
- Tests have been automated using the NUnit framework. This allows for the tests to be run automatically when the code is built.
- The tests have been written to test the services and the controllers.
- The tests have been written to test the happy path and the unhappy path as well as to test the error handling and logging.
- Interface have been used to mock the services allowing for the services to be easily swapped out for different implementations.
- Dependency Injection has been used to inject the services into the controllers. This allows for the services to be easily mocked for testing and allows for the services to be easily swapped out for different implementations.
- All dependencies have been passed into the classes via the constructor. This allows for the dependencies to be easily mocked for testing.