# ChatApp

A real-time private chat application built with ASP.NET Core, SignalR, and Clean Architecture principles.

## Overview

ChatApp is a real-time messaging application that enables authenticated users to communicate instantly through private conversations. The application uses JWT Authentication to secure APIs and SignalR connections while persisting messages in SQL Server using Entity Framework Core.

---

## Features

### Authentication & Authorization

* User Registration
* User Login
* Password Hashing with BCrypt
* JWT Authentication
* JWT Authorization
* Protected API Endpoints
* Protected SignalR Hub

### Real-Time Communication

* Real-Time Private Messaging with SignalR
* Online Users Tracking
* Connection Lifecycle Handling
* User-to-User Messaging
* Conversation History Between Users

### Data Persistence

* Store Messages in SQL Server
* Retrieve Conversation History
* Entity Framework Core Migrations

### Architecture

* Clean Architecture
* Repository Pattern
* Dependency Injection
* Separation of Concerns

---

## Technologies

### Backend

* ASP.NET Core 8 Web API
* SignalR
* Entity Framework Core
* SQL Server

### Security

* JWT Bearer Authentication
* BCrypt Password Hashing

### Tools

* Swagger / OpenAPI
* Git & GitHub

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

* Controllers
* SignalR Hub
* Authentication Configuration

#### Application Layer

* DTOs
* Business Services
* JWT Services

#### Domain Layer

* Entities
* Repository Contracts

#### Infrastructure Layer

* Entity Framework Core
* Database Access
* Repository Implementations

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

#### Get All Messages

```http
GET /api/Message
```

#### Get Conversation Between Two Users

```http
GET /api/Message/conversation/{userId}
```

---

### Users

#### Get Current User

```http
GET /api/Users/me
```

#### Get All Users

```http
GET /api/Users
```

---

## SignalR Hub

### Hub Endpoint

```http
/Chat
```

### Hub Methods

#### SendMessage

```text
SendMessage(receiverId, message)
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

* API Endpoints
* SignalR Connections

Each authenticated user receives a JWT token after login.

---

## Database

SQL Server using Entity Framework Core Code-First approach.

### User Entity

* Id
* UserName
* Email
* PasswordHash
* CreatedAt

### Message Entity

* Id
* Content
* SenderId
* ReceiverId
* SenderName
* SentAt

---

## Future Improvements

* Group Chat
* Typing Indicator
* Read Receipts
* Refresh Tokens
* Message Editing
* Message Deletion
* User Online Status API

---

## Author

**Ziad Abdo**

GitHub:
https://github.com/ziad-abdo96
