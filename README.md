# DnaLabApi

REST API for managing DNA sample tracking in a biological analysis laboratory. Built with ASP.NET Core and MongoDB.

## What it does

Laboratories receive biological samples from donors and need to track them through the analysis process — from collection to archival. This API handles that workflow, including sample registration, status tracking, and secure access for analysts.

## Tech Stack

- **ASP.NET Core 10** — Web API framework
- **MongoDB** — Database
- **Docker** — Containerization
- **JWT** — Authentication

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Run MongoDB

```bash
docker run -d --name mongo -p 27017:27017 mongo
```

### Clone and run

```bash
git clone https://github.com/yourusername/DnaLabApi.git
cd DnaLabApi
dotnet user-secrets set "ConnectionStrings:MongoDb" "mongodb://localhost:27017"
dotnet user-secrets set "JwtSettings:SecretKey" "your-secret-key-minimum-32-characters"
dotnet run
```

API will be available at `http://localhost:5243/scalar`

## Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/auth/login` | Get a JWT token |

### Samples
All endpoints require a valid JWT token in the `Authorization: Bearer <token>` header.

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/samples` | Get all samples |
| GET | `/samples/{id}` | Get a sample by ID |
| POST | `/samples` | Register a new sample |
| PUT | `/samples/{id}` | Update a sample |
| DELETE | `/samples/{id}` | Delete a sample |

### Health
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/health` | Check API and database status |

## Sample Data

```json
{
  "donorName": "John Doe",
  "donorAge": 35,
  "donorSex": "Male",
  "sampleType": "Blood"
}
```

**Sample types:** `Blood`, `Hair`, `Saliva`, `Urine`

**Sample statuses:** `Collected`, `InProcess`, `Analyzed`, `Archived`

## Run with Docker

```bash
# Create a shared network
docker network create dnalab-network
docker network connect dnalab-network mongo

# Build and run the API
docker build -t dnalab-api .
docker run -d --name dnalab-api -p 8080:8080 \
  --network dnalab-network \
  -e JwtSettings__SecretKey="your-secret-key-minimum-32-characters" \
  dnalab-api
```

API will be available at `http://localhost:8080`
