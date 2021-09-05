# SAFE Template

This template can be used to generate a back-end application using the [SAFE Stack](https://safe-stack.github.io/). It was created using the dotnet [SAFE Template](https://safe-stack.github.io/docs/template-overview/). If you want to learn more about the template why not start with the [quick start](https://safe-stack.github.io/docs/quickstart/) guide?



## Install pre-requisites

You'll need to install the following pre-requisites in order to build SAFE applications

* [.NET Core SDK](https://www.microsoft.com/net/download) 5.0 or higher
* [Node LTS](https://nodejs.org/en/download/)

## Starting the application
You should have docker installed on your PC.

At first clone this repository to a folder on your local machine.

To set up the environment just start the command

```shell
repo root> docker-compose up -d
```
It will start the database to interact with the application and the PGAdmin if you need to interact with database.

### PgAdmin Access:
- Address: http://localhost:8000/browser/#
- e-mail: admin@admin.com
- password: admin
-
### PgAdmin Initial configurations:
- Address: db
- Username: postgres
- Password: docker


If you cou
Before you run the project **for the first time only** you must install dotnet "local tools" with this command:

```bash
dotnet tool restore
```

To concurrently run the server and the client components in watch mode use the following command:

```bash
dotnet run
```
## Migrations

You can firstly make changes on your model on file src/Server/Models/palladris.fs

After you can run command
```bash
dotnet ef migrations add YYY
```
where:
YYY - Some name to manage you what you've done.

So you can apply your changes to the Database with command:

```bash
dotnet ef database update
```
## Endpoints Routes

- Get all transactions
Method: GET
Endpoint: http://localhost:8085/api/IApi/GetTransactions

- Add a new transaction
Method: POST
Endpoint: http://localhost:8085/api/IApi/AddTransaction
JSON body example:

```json
[
	{
	"pairs": "USD/GBP",
	"provider": "provider 300",
	"price": 0.7218,
	"quantity": 10000000,
	"transactiondate": "2021-09-01"
	}
]
```

## SAFE Stack Documentation

If you want to know more about the full Azure Stack and all of it's components (including Azure) visit the official [SAFE documentation](https://safe-stack.github.io/docs/).

You will find more documentation about the used F# components at the following places:

* [Saturn](https://saturnframework.org/)
* [Fable](https://fable.io/docs/)
* [Elmish](https://elmish.github.io/elmish/)
