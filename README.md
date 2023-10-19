# 🐶 Dogs House 🏠

![.NET](https://img.shields.io/badge/.NET-7.0-6c3c94) ![Docker](https://img.shields.io/badge/Docker-288ce4) ![MS SQL](https://img.shields.io/badge/Microsoft_SQL_Server-c42c24)

This is a fun little *Web Api* app made as a **test task** for *Junior .Net Developer* position.

## Installation

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) - to host the application.
### How to install and run
0. Open your command-line interface (CLI). Choose a *path* where you want to install the project with `cd`
1. Clone the repository by `git clone https://github.com/Alexander-Riabovol/DogsHouse.git`
2. Change your working directory to the one of solution by `cd DogsHouse`.
3. Make sure that your `Docker` is running. Containerize the application by `docker-compose build`. <small>The initial <b>build</b> will require a certain amount of time</small>.
4. Run the application by `docker-compose up -d`. <small>It will take a couple of seconds</small>.

## Features

###### GET /ping
`curl -X GET http://localhost:5171/ping` returns the following message: `"Dogshouseservice.Version1.0.1"`
###### GET /dogs
The endpoint that allows querying dogs: `http://localhost:5171/dogs`
You can add query parameters `attribute` & `order` for sorting; and `pageNumber` & `pageSize` for pagination. Sorting and pagination can be used simultaneously.
Example:
```
curl -X GET http://localhost:5171/dogs -v
curl -X GET http://localhost:5171/dogs?attribute=weight&order=desc -v
curl -X GET http://localhost:5171/dogs?pageNumber=3&pageSize=10 -v
```
###### POST /dog
The endpoint that allows creating dogs: `http://localhost:5171/dog`
Example:
```
curl -X POST http://localhost:5171/dog -v
	 -H "Content-Type: application/json"
	 -d "{"name": "Doggy", "color": "red", "tail_length": 173, "weight": 33}"
```
###### Rate Limiter
There is a setting that says how many *requests per second* the service can handle. In case there are more incoming requests, the application will return HTTP status code "**429**TooManyRequests".
By default, you can only send 1 request per 1 second, but you can change this in the `appsettings.json`, by modifying `AllowedRequestsPerSecond` property, which is pretty self-explanatory.

##### Used frameworks:
 ![MediatR](https://img.shields.io/badge/MediatR-2596be) ![Mapster](https://img.shields.io/badge/Mapster-ffbc34) ![Fluent Validation](https://img.shields.io/badge/Fluent_Validation-ff0404) ![Fluent Assertions](https://img.shields.io/badge/Fluent_Assertions-f01c24) ![Moq](https://img.shields.io/badge/Moq-f8b804) ![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-1874a4)

*Thank you for your time :)*