# ChatApp

A real-time chat application built with ASP.NET Core, SignalR, and Clean Architecture principles, supporting both private and group messaging.

## Overview

ChatApp is a real-time messaging application that enables authenticated users to communicate instantly through both private and group conversations. The application uses JWT Authentication to secure REST APIs and SignalR connections while persisting data in SQL Server using Entity Framework Core.

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

### Private Chat

* Real-Time Private Messaging with SignalR
* User-to-User Messaging
* Conversation History Between Users
* Online Users Tracking
* Connection Lifecycle Handling

### Group Chat

* Create Chat Groups
* Delete Chat Groups
* Add Members to Groups
* Remove Members from Groups
* Retrieve User Groups
* Retrieve Group Members
* Join Group Conversations
* Real-Time Group Messaging
* Group Message Persistence

### Data Persistence

* Store Private Messages in SQL Server
* Store Group Messages in SQL Server
* Retrieve Conversation History
* Entity Framework Core Migrations

### Architecture

* Clean Architecture
* Repository Pattern
* Dependency Injection
* Separation of Concerns
* SignalR Connection Management

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

### Design

* Clean Architecture
* Repository Pattern
* Dependency Injection

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

* REST Controllers
* SignalR Hub
* Authentication Configuration

#### Application Layer

* DTOs
* Business Services

#### Domain Layer

* Entities
* Repository Contracts

#### Infrastructure Layer

* Entity Framework Core
* Repository Implementations
* SQL Server
* SignalR Connection Management

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

### Messages

#### Get Conversation Between Two Users

```http
GET /api/Message/conversation/{userId}
```

---

### Groups

#### Create Group

```http
POST /api/Group
```

#### Get User Groups

```http
GET /api/Group/my
```

#### Get Group Details

```http
GET /api/Group/{groupId}
```

#### Delete Group

```http
DELETE /api/Group/{groupId}
```

#### Add Member

```http
POST /api/Group/{groupId}/members
```

#### Remove Member

```http
DELETE /api/Group/{groupId}/members/{memberId}
```

#### Get Group Members

```http
GET /api/Group/{groupId}/members
```

---

## SignalR Hub

### Hub Endpoint

```http
/Chat
```

### Hub Methods

#### Private Chat

```text
SendMessage(receiverId, message)
```

#### Group Chat

```text
JoinGroup(groupId)

SendGroupMessage(groupId, message)
```

### Events

#### Private Messages

```text
ReceiveMessage(username, message)
```

#### Group Messages

```text
ReceiveGroupMessage(userId, username, message, groupId)
```

#### Online Users

```text
OnlineUsersUpdated(users)
```

---

## Authentication

JWT Bearer Authentication secures:

* REST API Endpoints
* SignalR Connections

Each authenticated user receives a JWT token after login.

---

## Database

SQL Server using Entity Framework Core (Code-First).

### User

* Id
* UserName
* Email
* PasswordHash
* CreatedAt

### Message

* Id
* Content
* SenderId
* ReceiverId
* GroupId
* SentAt

### ChatGroup

* Id
* Name
* OwnerId

### GroupMember

* UserId
* GroupId

---

## Author

**Ziad Abdo**

GitHub: https://github.com/ziad-abdo96
