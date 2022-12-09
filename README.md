
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=Zee864_productviewer) &nbsp; [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=bugs)](https://sonarcloud.io/dashboard?id=Zee864_productviewer) &nbsp; [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=coverage)](https://sonarcloud.io/dashboard?id=Zee864_productviewer) &nbsp; [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=ncloc)](https://sonarcloud.io/dashboard?id=Zee864_productviewer) &nbsp; [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=alert_status)](https://sonarcloud.io/dashboard?id=Zee864_productviewer) &nbsp; [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Zee864_productviewer&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=Zee864_productviewer)
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
