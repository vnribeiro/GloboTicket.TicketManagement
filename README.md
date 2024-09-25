# GloboTicket Ticket Management

## Overview

GloboTicket Ticket Management is a comprehensive Event Management System built using clean architecture and the latest technologies and best practices in the .NET ecosystem. The system allows users to manage events, categories, and send email notifications. The architecture is designed with a focus on clean code, separation of concerns, and scalability.

## Technologies Used

- **.NET 8**: Leveraging the latest features and improvements in the .NET ecosystem.
- **Entity Framework Core**: For data access and Object-Relational Mapping (ORM).
- **MediatR**: Implements the mediator pattern for handling commands and queries.
- **FluentValidation**: Provides a fluent interface for building strongly-typed validation rules.
- **AutoMapper**: Simplifies the mapping of DTOs to domain models and vice versa.
- **Moq**: A mocking library used in unit tests to create mock objects.
- **xUnit**: A testing framework for writing unit tests.
- **Shouldly**: An assertion library that offers more readable and expressive assertions.
- **ASP.NET Core**: For building the web API and handling HTTP requests and responses.
- **Microsoft.Extensions.Logging**: Provides logging capabilities throughout the application.
- **SendGrid**: Used for sending emails within the application.

## Project Structure

This project is organized into several layers following the principles of clean architecture, each with a specific responsibility:

- **Domain**: Contains the core entities and business logic of the application.
- **Application**: Contains the application logic, including commands, queries, validators, and mappings.
- **Persistence**: Handles data access, including database context, migrations, and repository implementations.
- **Identity**: Manages user authentication and authorization, including identity management, roles, and claims.
- **Infrastructure**: Contains implementations for email services, third-party integrations, and other infrastructure concerns.
- **UI**: The front-end application layer that interacts with the API, providing a user interface for managing events and categories.
- **API**: The entry point of the application, exposing endpoints for clients to interact with.

## Key Features

- **Event Management**: Create, update, and manage events with ease.
- **Category Management**: Create and manage event categories.
- **Email Notifications**: Send email notifications when new events are created.
- **Validation**: Ensures that all input data is validated before processing.
- **User Authentication**: Secure authentication and authorization using ASP.NET Core Identity.
- **Unit Testing**: Comprehensive unit tests to ensure the reliability and correctness of the application.
