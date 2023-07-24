# .NET 6 API SQL Database

Use the [create-dotnet6-api.sql](./db-scripts/create-dotnet6-api.sql) script to initialize a SQL Server database and insert records.

## Run SQL Server using Docker

[Environment variables](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-configure-environment-variables) configure the container and the database that it will present:

Required setting for the SQL Server Docker image.

-   `ACCEPT_EULA`: Set the ACCEPT_EULA variable to any value to confirm your acceptance of the End-User Licensing Agreement.
-   `MSSQL_SA_PASSWORD`: Configure the SA user password.
-   `MSSQL_PID`: Set the SQL Server edition or product key.
-   `MSSQL_TCP_PORT`: Configure the TCP port that SQL Server listens on.

### Docker Commands

Build Container

> docker build -t dotnet6api-sql:dev .

Run Container

> docker run --name dotnet6api-sql -p 1433:1433 -d dotnet6api-sql:dev

Stop Container

> docker kill dotnet6api-sql

Remove Container

> docker rm dotnet6api-sql

Remove Image

> docker rmi dotnet6api-sql:dev

### Notes

* If you are using Docker Linux or Mac OSX, you may get a *permission denied* error when running the container. To fix you'll need to make the script files executable. To do this, run the following command in your local environment:
    * > chmod +x <script_filename>
    * Replace `<script_filename>` with the actual name of your script file.
* If running SQL Server on a Mac with M chip, you may get an error when running the container. To fix you can try the following:
    * > docker run --name dotnet6api-sql -p 1433:1433 -d --platform linux/amd64 dotnet6api-sql:dev
    * If using Docker Desktop try *experimental feature* `Use Rosetta for x86/amd64 emulation on Apple Silicon` under the *Features in development* section in the *Preferences* dialog.