echo "Wait 30 seconds to be sure that SQL Server came up"
sleep 30s

# Note: make sure that your password matches what is in the Dockerfile
echo "Run the setup script to create the DB and the schema in the DB"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -d master -i "/docker-entrypoint-initdb/create-dotnet6-api.sql"
