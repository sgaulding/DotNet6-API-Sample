# DotNet6-API-Sample

An .NET 6 API sample implemenation of a user service.

## Deployment

This application follows a continuous integration approach whereby any changes to the default branch main will result in a new build to the target platform. This application uses GitHub Actions to build and unit test the configured target platform.

## Local Devleopment

Either use a SQL Server instance or a Docker container to set up the back-end data store. See [Database Readme](./database/mssql/readme.md) for more information.

### DotNet Steps

* Install the .NET 6 SDK from the official Microsoft website.
* Clone the repository to your local machine.
* Open the solution file in Visual Studio Code or your preferred IDE.
* Restore the NuGet packages by running the following command in the terminal:
  * `> dotnet restore`
* Set up the database by following the instructions in the Database Readme.
* Install self signed certs
  * `> dotnet dev-certs https`
* Build the solution by running the following command in the terminal:
  * `> dotnet build`
* Run the API by running the following command in the terminal:
  * `> dotnet run`
* The API should now be running on
  * <https://localhost:7200>
  * <http://localhost:5200>

### Testing

* Can use the Swagger / OpenAPI to test API or use other tools
  * <https://localhost:7200/swagger/index.html>
  * <http://localhost:5200/swagger/index.html>
