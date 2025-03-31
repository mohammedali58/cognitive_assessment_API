
# Cognitive Assessment API

## Overview

This Cognitive Assessment API provides endpoints to  perform cognitive score calculations. Built using .NET 8, PostgreSQL, and Entity Framework Core, it follows clean architecture principles to ensure maintainability and scalability.

## Technologies

- .NET 8
- Entity Framework Core
- PostgreSQL
- Docker
- xUnit (for Unit Testing)

## Features

- RESTful endpoints for managing cognitive journals
- Cognitive assessment scoring based on customizable scoring logic
- Global exception handling and robust error responses
- Dockerized application and testing environment

## Prerequisites

- Docker & Docker Compose

## Setup Instructions

### Clone the Repository

```bash
git clone https://github.com/mohammedali58/Cognitive_assesment_API.git
cd Cognitive_Assessment
```

### Running the Application (Docker)

Ensure Docker is running.

```bash
docker compose up --build
```

Application runs at:

```
http://localhost:5212
```

### Running Unit Tests (Docker)

Run unit tests using Docker Compose:

```bash
docker compose -f docker-compose.test.yml up --build
```

Test results will be displayed in the console.

## API Endpoints

Example endpoints:

- Get Journals:
  ```
  GET /journals/{id}/score
  ```

- Create a new Journal:
  ```
  POST /journals
  ```


## Architecture

Follows clean architecture principles:
- **Core:** Domain models and logic
- **Application:** Use-cases, services, and interfaces
- **Infrastructure:** Data access (EF Core) and external integrations
- **API:** Presentation layer (controllers, middlewares)

## Exception Handling

The application includes a global exception handling middleware providing clear JSON-formatted error responses:

```json
{
  "error": "Error message here",
  "statusCode": 400
}
```


