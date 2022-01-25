Pre requiriments:

1 - Visual Studio or Vs Code
2 - Nodejs
3 - MSSQL instance (To use as database, it dont need to me on your machine)

How to run the application:

1 - Open the project on Visual Studio

2 - Change the connection string "SqlConnectionString" on appsettings.json. This connection string will be used by EF. The database don't need to exists when you run in debug mode, the EF will create it automtically.

3 - Run the application on Debug mode

4 - In the login page, user the credentials
	adm
	123

5 - You can use the API to GET, PUT or POST new users and reports. If you did it, you will be able to see the diffence in the reports on the home page.


The API routes are:

GET User/{id} -> Get some user by ID
GET User/GetAll -> Return all users from database
GET User/Authenticate Authenticate some user using login and password. It requires two parameters, "login" and "password"
POST User/  -> Create a new user, it require a parameter "password" and the body should be:

{
	"Id" : 0,
	"Login": "xxxx",
	"Name": "xxxx",
	"Agee": 19
}

PUT  User/  -> Create a new user, the body should be the same as the insert
DELETE User/{ID} Delete user by ID

GET Report/{id} -> Get some report by ID
GET Report/GetAll -> Return all reports from database
GET Report/Execute/{id} -> Execute the query from the specified report and return the result
POST Report/  -> Create a new Report, the body should be:

{
	"Id" : 0,
	"Name": "xxxx",
	"Query": "xxxx",
	"Enabled": True
}

PUT  Report/  -> Create a new Report, the body should be the same as the insert
DELETE Report/{ID} Delete report by ID


