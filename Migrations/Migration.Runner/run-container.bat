docker run --rm --name sql-server  -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Senha@123" -p 1433:1433 -d mcr.microsoft.com/mssql/server
