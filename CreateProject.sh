#!/bin/bash

# Nome da solução
SOLUTION_NAME="AuthenticationApp"

# Pastas e projetos
DOMAIN_PROJECT="${SOLUTION_NAME}.Domain"
APPLICATION_PROJECT="${SOLUTION_NAME}.Application"
INFRA_PROJECT="${SOLUTION_NAME}.Infrastructure"
WEBAPI_PROJECT="${SOLUTION_NAME}.WebApi"
SHARED_PROJECT="${SOLUTION_NAME}.SharedKernel"

# Cria a solução
dotnet new sln -n $SOLUTION_NAME

# Cria os projetos
dotnet new classlib -n $DOMAIN_PROJECT
dotnet new classlib -n $APPLICATION_PROJECT
dotnet new classlib -n $INFRA_PROJECT
dotnet new webapi -n $WEBAPI_PROJECT
dotnet new classlib -n $SHARED_PROJECT

# Adiciona os projetos à solução
dotnet sln add $DOMAIN_PROJECT/$DOMAIN_PROJECT.csproj
dotnet sln add $APPLICATION_PROJECT/$APPLICATION_PROJECT.csproj
dotnet sln add $INFRA_PROJECT/$INFRA_PROJECT.csproj
dotnet sln add $WEBAPI_PROJECT/$WEBAPI_PROJECT.csproj
dotnet sln add $SHARED_PROJECT/$SHARED_PROJECT.csproj

# Adiciona referências de projetos
dotnet add $APPLICATION_PROJECT/$APPLICATION_PROJECT.csproj reference $DOMAIN_PROJECT/$DOMAIN_PROJECT.csproj
dotnet add $INFRA_PROJECT/$INFRA_PROJECT.csproj reference $DOMAIN_PROJECT/$DOMAIN_PROJECT.csproj
dotnet add $WEBAPI_PROJECT/$WEBAPI_PROJECT.csproj reference $APPLICATION_PROJECT/$APPLICATION_PROJECT.csproj
dotnet add $WEBAPI_PROJECT/$WEBAPI_PROJECT.csproj reference $INFRA_PROJECT/$INFRA_PROJECT.csproj

# Dependências essenciais
# EF Core e provider PostgreSQL
dotnet add $INFRA_PROJECT/$INFRA_PROJECT.csproj package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add $INFRA_PROJECT/$INFRA_PROJECT.csproj package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.0

# FluentValidation (para DTOs e validação de aplicação)
dotnet add $APPLICATION_PROJECT/$APPLICATION_PROJECT.csproj package FluentValidation --version 11.0.0

# AutoMapper (mapeamento DTO -> Entity)
dotnet add $APPLICATION_PROJECT/$APPLICATION_PROJECT.csproj package AutoMapper --version 13.0.0
dotnet add $WEBAPI_PROJECT/$WEBAPI_PROJECT.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection --version 13.0.0

# Swashbuckle (Swagger) para WebApi
dotnet add $WEBAPI_PROJECT/$WEBAPI_PROJECT.csproj package Swashbuckle.AspNetCore --version 6.7.0

# Opcional: pacote de mediador CQRS
dotnet add $APPLICATION_PROJECT/$APPLICATION_PROJECT.csproj package MediatR --version 12.0.0
dotnet add $WEBAPI_PROJECT/$WEBAPI_PROJECT.csproj package MediatR.Extensions.Microsoft.DependencyInjection --version 12.0.0

# Criação de pastas padrão do DDD

# Domain
mkdir -p $DOMAIN_PROJECT/Entities
mkdir -p $DOMAIN_PROJECT/ValueObjects
mkdir -p $DOMAIN_PROJECT/Repositories
mkdir -p $DOMAIN_PROJECT/Services
mkdir -p $DOMAIN_PROJECT/Events
mkdir -p $DOMAIN_PROJECT/Enums
mkdir -p $DOMAIN_PROJECT/Exceptions
mkdir -p $DOMAIN_PROJECT/Aggregates

# Application
mkdir -p $APPLICATION_PROJECT/Interfaces
mkdir -p $APPLICATION_PROJECT/Services
mkdir -p $APPLICATION_PROJECT/DTOs
mkdir -p $APPLICATION_PROJECT/Validators
mkdir -p $APPLICATION_PROJECT/Commands
mkdir -p $APPLICATION_PROJECT/Queries

# Infrastructure
mkdir -p $INFRA_PROJECT/Persistence
mkdir -p $INFRA_PROJECT/Services
mkdir -p $INFRA_PROJECT/Configurations
mkdir -p $INFRA_PROJECT/Messaging

# WebApi
mkdir -p $WEBAPI_PROJECT/Controllers
mkdir -p $WEBAPI_PROJECT/Models
mkdir -p $WEBAPI_PROJECT/Middlewares
mkdir -p $WEBAPI_PROJECT/Filters
mkdir -p $WEBAPI_PROJECT/Extensions

# SharedKernel
mkdir -p $SHARED_PROJECT/Helpers
mkdir -p $SHARED_PROJECT/Extensions
mkdir -p $SHARED_PROJECT/Constants
mkdir -p $SHARED_PROJECT/BaseClasses

echo "🎉 Solução $SOLUTION_NAME criada com sucesso!"
