# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["PaymentSystem.API/PaymentSystem.API.csproj", "PaymentSystem.API/"]
COPY ["PaymentSystem.Application/PaymentSystem.Application.csproj", "PaymentSystem.Application/"]
COPY ["PaymentSystem.Domain/PaymentSystem.Domain.csproj", "PaymentSystem.Domain/"]
COPY ["PaymentSystem.Infrastructure/PaymentSystem.Infrastructure.csproj", "PaymentSystem.Infrastructure/"]
COPY ["PaymentSystem.Persistence/PaymentSystem.Persistence.csproj", "PaymentSystem.Persistence/"]

RUN dotnet restore "PaymentSystem.API/PaymentSystem.API.csproj"

COPY . .
WORKDIR /src/PaymentSystem.API
RUN dotnet publish -c Release -o /app/publish


# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PaymentSystem.API.dll"]