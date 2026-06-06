# ChatApp

A real-time chat application built with ASP.NET Core and SignalR following Clean Architecture principles.

## Overview

ChatApp is a real-time messaging application that allows authenticated users to communicate instantly using SignalR. The application uses JWT Authentication for securing APIs and SignalR connections and persists chat messages in SQL Server using Entity Framework Core.

---

## Features

### Authentication & Authorization

- User Registration
- User Login
- Password Hashing with BCrypt
- JWT Authentication
- Protected API Endpoints
- Protected SignalR Hub

### Real-Time Communication

- Real-Time Messaging with SignalR
- Online Users Tracking
- Connection Lifecycle Handling
- Message Broadcasting

### Data Persistence

- Store Messages in SQL Server
- Retrieve Chat History
- Entity Framework Core Migrations

### Architecture

- Clean Architecture
- Repository Pattern
- Dependency Injection
- Separation of Concerns

---

## Technologies

### Backend

- ASP.NET Core Web API
- SignalR
- Entity Framework Core
- SQL Server

### Security

- JWT Bearer Authentication
- BCrypt Password Hashing

### Tools

- Swagger / OpenAPI
- Git & GitHub

---

## Project Structure

```text
ChatApp.API
│
├── Controllers
├── Hubs
├── Program.cs
│
ChatApp.Application
│
├── DTOs
├── Services
│
ChatApp.Domain
│
├── Entities
├── Interfaces
│
ChatApp.Infrastructure
│
├── Data
├── Repositories
├── Migrations
```

---

## Architecture Flow

```text
API Layer
    ↓
Application Layer
    ↓
Domain Layer
    ↑
Infrastructure Layer
```

### Responsibilities

#### API Layer

- Controllers
- SignalR Hub
- Authentication Configuration

#### Application Layer

- DTOs
- Business Services
- JWT Service

#### Domain Layer

- Entities
- Repository Contracts

#### Infrastructure Layer

- Entity Framework Core
- Database Access
- Repository Implementations

---

## API Endpoints

### Authentication

#### Register

```http
POST /api/Auth/register
```

#### Login

```http
POST /api/Auth/login
```

Returns:

```json
{
  "token": "JWT_TOKEN"
}
```

---

### Messages

#### Get Chat History

```http
GET /api/Message
```

---

## SignalR Hub

### Hub Endpoint

```http
/Chat
```

### Events

#### ReceiveMessage

```text
ReceiveMessage(username, message)
```

#### OnlineUsersUpdated

```text
OnlineUsersUpdated(users)
```

---

## Authentication

JWT Bearer Authentication is used to secure:

- API Endpoints
- SignalR Connections

Each authenticated user receives a JWT token after login.

---

## Database

SQL Server with Entity Framework Core Code-First approach.

Main Entities:

### User

- Id
- UserName
- Email
- PasswordHash
- CreatedAt

### Message

- Id
- Content
- SenderId
- SenderName
- SentAt

---

## Future Improvements

- Private Chat
- Chat Rooms
- Group Messaging
- Typing Indicator
- Read Receipts
- Refresh Tokens
- Message Editing
- Message Deletion

---

## Screenshots

Add screenshots here:

### Swagger

![Swagger](images/swagger.png)

### Chat Page

![Chat](images/chat.png)

---

## Author

**Ziad Abdo**

GitHub: https://github.com/ziad-abdo96