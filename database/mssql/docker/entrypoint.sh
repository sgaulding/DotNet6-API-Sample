echo "Start and Initialize SQL Server"

# Run Microsoft SQl Server and initialization script (at the same time)
/docker-entrypoint-initdb/initialize-db.sh & /opt/mssql/bin/sqlservr
