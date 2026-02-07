# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src



# Copy project files
COPY ["PaymentSystem.API/PaymentSystem.API.csproj", "PaymentSystem.API/"]
COPY ["PaymentSystem.Application/PaymentSystem.Application.csproj", "PaymentSystem.Application/"]
COPY ["PaymentSystem.Domain/PaymentSystem.Domain.csproj", "PaymentSystem.Domain/"]
COPY ["PaymentSystem.Infrastructure/PaymentSystem.Infrastructure.csproj", "PaymentSystem.Infrastructure/"]
COPY ["PaymentSystem.Persistence/PaymentSystem.Persistence.csproj", "PaymentSystem.Persistence/"]

# Now restore — global.json is present
RUN dotnet restore "PaymentSystem.API/PaymentSystem.API.csproj"

# Copy everything else
COPY . .

WORKDIR /src/PaymentSystem.API
RUN dotnet publish -c Release -o /app/publish