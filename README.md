The project is built using .NET Core 8.

MariaDB 10.3 is used as the database engine, with Entity Framework Core for data access.

Automatic database creation and migrations are supported and handled on application startup.

The frontend is implemented using Razor Pages.

The original test task is included in the project repository.

Environment variables for mariadb:

      "MYSQL_ROOT_PASSWORD=1234",
      
			"MYSQL_USER=user",
   
			"MYSQL_PASSWORD=userpass"
