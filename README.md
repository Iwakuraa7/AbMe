# AbMe

A **full-stack** social service for keeping and sharing lists of your hobbies and interests — music, books, anime, manga and films. A **React** single-page client backed by a **C# / ASP.NET Core 8 REST API**, with a layered architecture, token-based auth, and a full unit-test suite.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![React](https://img.shields.io/badge/React-frontend-61DAFB?logo=react&logoColor=black)
![EF Core](https://img.shields.io/badge/EF%20Core-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![Tests](https://img.shields.io/badge/tests-xUnit-5C2D91)
![License](https://img.shields.io/badge/license-MIT-green)

---

## Features

- **React single-page frontend** that consumes the API for registration, login and managing hobby lists _(see `AbMe-frontend/react-frontend`)_.
- **JWT authentication** with a role model (`Admin`, `User`) built on ASP.NET Core Identity.
- **Resource-ownership checks** so users can only modify their own data.
- **Hobby management** across multiple categories (music, books, anime, manga, films) with one-to-many relationships to the user.
- **External catalogue search** — the client pulls real titles and artwork from third-party APIs (**Spotify**, **Google Books**, **TMDB** and **Jikan / MyAnimeList**) so you can search actual content and add it straight to your profile.
- **RESTful API** with clear, resource-oriented endpoints.
- **Layered architecture** with the **Repository pattern** for a clean separation between the API, business logic and data access.
- **Code-First database** with EF Core migrations.
- **Unit-tested** repositories and controllers.
- **Containerised** and ready for cloud deployment.

## Tech stack

| Layer | Technologies |
|-------|--------------|
| Language / runtime | C#, .NET 8 |
| API | ASP.NET Core 8 Web API |
| Auth | JWT, ASP.NET Core Identity, RBAC |
| Data access | Entity Framework Core (Code-First, migrations) |
| Database | SQL Server |
| External APIs | Spotify Web API, Google Books API, TMDB, Jikan (MyAnimeList) |
| Testing | xUnit, FakeItEasy, FluentAssertions, EF Core InMemory |
| DevOps | Docker, AWS (Lambda, Elastic Beanstalk) |
| Frontend | ReactJS + Vite _(in `AbMe-frontend/react-frontend`)_ |

## Architecture

The solution is split into focused projects:

```
AbMe.sln
├── AbMe-backend/              # ASP.NET Core 8 Web API (controllers, services, repositories, EF Core)
├── AbMe-backend.Tests/        # xUnit test project (repository + controller tests)
└── AbMe-frontend/
    └── react-frontend/        # React single-page client
```

The **React client** consumes the API over HTTP, attaching the JWT to authenticated requests. On the server, requests flow **Controller → Service → Repository → EF Core → SQL Server**. The Repository pattern keeps data-access logic isolated and makes the business layer easy to unit-test against an in-memory database.

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local instance, Express, or a container)
- _(optional)_ Docker

### Run the API locally

```bash
# 1. Clone
git clone https://github.com/Iwakuraa7/AbMe.git
cd AbMe

# 2. Configure the connection string and JWT settings
#    Edit AbMe-backend/appsettings.json (or use user-secrets):
#    - ConnectionStrings:DefaultConnection      -> your SQL Server
#    - JWT:Issuer / JWT:Audience / JWT:SigningKey -> token issuer, audience and signing key

# 3. Apply migrations to create the database
cd AbMe-backend
dotnet ef database update

# 4. Run
dotnet run
```

The API listens on `http://localhost:5078` (configured via the `Kestrel` section in `appsettings.json`). Swagger UI is available at `http://localhost:5078/swagger`.

### Run the tests

```bash
dotnet test
```

### Run the frontend

```bash
cd AbMe-frontend/react-frontend
npm install
npm run dev
```

- The frontend talks to the API at `http://localhost:5078`, which is hard-coded in the page components — no `.env` file is required for the backend URL.
- The external-API credentials (Spotify client ID/secret and Google Books API key) live in `src/contexts/UserContext.jsx`.

The client is served by Vite at `http://localhost:5173` and talks to the API started above.

### Run with Docker

```bash
# Build from the repo root (the Dockerfile lives in AbMe-backend/ and copies the whole solution):
docker build -f AbMe-backend/Dockerfile -t abme-api .
docker run -p 8080:8080 abme-api
```

Alternatively, the `docker-compose.yaml` at the repo root brings up the frontend and backend together:

```bash
docker compose up --build
# frontend -> http://localhost:3000   backend -> http://localhost:8080
```

## API overview

All routes are prefixed with `/api`. `JWT` in the **Auth** column means the endpoint reads the caller's identity from the bearer token; `Public` endpoints need no token.

### Account & users

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/account/register` | Register a new user, returns a JWT | Public |
| POST | `/api/account/login` | Authenticate, returns a JWT | Public |
| GET | `/api/account/user-search/{username}` | Search for users by username | Public |
| GET | `/api/account/user-hobby-data/{username}` | Get a user's full profile data across every hobby category | Public |

### Hobby categories

Each category is exposed under its own route and follows the same CRUD shape, where
`{category}` ∈ `anime`, `manga`, `literature` (books), `media` (films / TV), `music`:

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/{category}` | List every entry in a category | Public |
| GET | `/api/{category}/{username}` | List a specific user's entries | Public |
| POST | `/api/{category}/create` | Add an entry to your profile | JWT |
| DELETE | `/api/{category}/delete/{id}` | Remove one of your own entries | JWT |

### Profile colours

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/user-color` | Get the signed-in user's profile colour palette | JWT |
| POST | `/api/user-color/create` | Create the user's colour palette | JWT |
| PUT | `/api/user-color/update` | Update the user's colour palette | JWT |

## External integrations

When adding items to a profile, the React client searches third-party catalogues and stores the chosen result (title, artwork, etc.) via the AbMe API:

| Category | Provider | Used for |
|----------|----------|----------|
| Anime | Jikan (MyAnimeList) API | Searching anime titles & cover art |
| Manga | Jikan (MyAnimeList) API | Searching manga titles & cover art |
| Films / TV (`media`) | The Movie Database (TMDB) API | Searching movies & shows |
| Books (`literature`) | Google Books API | Searching books |
| Music | Spotify Web API | Searching albums, artists & tracks |

## Screenshots

A quick tour of the React client.

### Home &amp; user search

The landing page greets you with the AbMe logo and a search box for finding other users by username.

| Landing page | Search results |
|:---:|:---:|
| ![AbMe home page](https://cdn.imgchest.com/files/189626ed97e2.png) | ![Searching for other users](https://cdn.imgchest.com/files/4ec720afe087.png) |

### Profile dashboard

Each profile groups the user's saved items into hobby categories — **Music**, **Literature**, **Anime**, **Manga** and **Media**.

![Profile hobby dashboard](https://cdn.imgchest.com/files/a245ed4de507.png)

### Adding items to your profile

Search external catalogues (anime, music, books, …) and add titles straight to your profile.

| Add anime | Add music |
|:---:|:---:|
| ![Add anime to profile](https://cdn.imgchest.com/files/76696d481774.png) | ![Add music to profile](https://cdn.imgchest.com/files/189eb259c5d4.png) |


## Contact

**Erlan Esengeldiev** — Fullstack developer (C# / .NET · React)
GitHub: [@Iwakuraa7](https://github.com/Iwakuraa7) · Telegram: [@iw4kvra](https://t.me/iw4kvra) · erlanesengeldiev7@gmail.com
