sleep 90s

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Your_password123 -d master -i /app/create.sql

if [ $? -eq 0 ]
then
    echo "create.sql completed"
else
    echo "cannot init database"
fi