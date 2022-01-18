# .NET 6 API SQL Database

This folder contains scripts to either use Docker to run SQL Server or execute on an instance of SQL already running

The [create-dotnet6-api.sql](./db-scripts/create-dotnet6-api.sql) script is used to initialize the database and insert records.

## Docker Usage

There are [environment variables](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-configure-environment-variables) that can be used to configure the container and the database that it will present:

-   `ACCEPT_EULA`: Set the ACCEPT_EULA variable to any value to confirm your acceptance of the End-User Licensing Agreement. Required setting for the SQL Server image.
-   `MSSQL_SA_PASSWORD`: Configure the SA user password.
-   `MSSQL_PID`: Set the SQL Server edition or product key.
-   `MSSQL_TCP_PORT`: Configure the TCP port that SQL Server listens on.

### Docker Commands:

Build Container

> docker build -t ms-sql-2019:dev .

Run Container

> docker run --name dotnet6api-sql -p 1433:1433 -d ms-sql-2019:dev

Stop Container

> docker kill dotnet6api-sql

Remove Container

> docker rm dotnet6api-sql

Remove Image

> docker rmi ms-sql-2019:dev
