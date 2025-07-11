# Learner – Personal Learning Tracker

A simple ASP.NET Core Web API to track your learning journey — subjects, entries (like notes or todos), and time spent. Built with PostgreSQL and EF Core.

## Setup Instructions

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- PostgreSQL installed and running
- Docker (optional)

---

### 1. Create PostgreSQL Database

---

### 2. Set Connection String

Create `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=yourhost;Port=yourport;Database=yourdbname;Username=dbusername;Password=yourpassword"
  }
}
```

---

### 3. Apply Migrations

```bash
dotnet ef database update
```

---

### 4. Run the App

```bash
dotnet run
```

Navigate to: [http://localhost:5145/swagger](http://localhost:5145/swagger)

---

## Docker Support

Build the container:

```bash
docker build -t learner .
```

Run it (replace with your connection string):

```bash
docker run -p 8080:80 -e learner
```
