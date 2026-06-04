# ChatApp

A real-time chat application built using ASP.NET Core and SignalR.

## Features

- User Registration
- User Login
- JWT Authentication
- JWT Authorization
- Real-Time Messaging with SignalR
- Online Users Tracking
- Message Persistence
- Chat History API
- Clean Architecture
- Repository Pattern

## Technologies

- ASP.NET Core Web API
- SignalR
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- BCrypt
- Swagger

## Architecture

```text
ChatApp.API
      ↓
ChatApp.Application
      ↓
ChatApp.Domain
      ↑
ChatApp.Infrastructure
```

## API Endpoints

### Authentication

```http
POST /api/Auth/register
POST /api/Auth/login
```

### Messages

```http
GET /api/Message
```

## SignalR Hub

```http
/chat
```

## Future Improvements

- Private Chat
- Chat Rooms
- Typing Indicator
- Read Receipts
- Refresh Tokens

## Author

Ziad Abdo