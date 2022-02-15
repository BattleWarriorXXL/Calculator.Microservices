FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Calculator.Microservices.Operations.Pow/Calculator.Microservices.Operations.Pow.csproj", "Calculator.Microservices.Operations.Pow/"]
COPY ["Calculator.Microservices.Shared.Kafka/Calculator.Microservices.Shared.Kafka.csproj", "Calculator.Microservices.Shared.Kafka/"]
COPY ["Calculator.Microservices.Shared.RabbitMQ/Calculator.Microservices.Shared.RabbitMQ.csproj", "Calculator.Microservices.Shared.RabbitMQ/"]
COPY ["Calculator.Microservices.Shared.Extensions/Calculator.Microservices.Shared.Extensions.csproj", "Calculator.Microservices.Shared.Extensions/"]
COPY ["Calculator.Microservices.Shared.Library/Calculator.Microservices.Shared.Library.csproj", "Calculator.Microservices.Shared.Library/"]
COPY ["Calculator.Microservices.Shared.IntegrationEvents/Calculator.Microservices.Shared.IntegrationEvents.csproj", "Calculator.Microservices.Shared.IntegrationEvents/"]


RUN dotnet restore "Calculator.Microservices.Operations.Pow/Calculator.Microservices.Operations.Pow.csproj"

COPY . .

WORKDIR "/src/Calculator.Microservices.Operations.Pow"

RUN dotnet build "Calculator.Microservices.Operations.Pow.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calculator.Microservices.Operations.Pow.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calculator.Microservices.Operations.Pow.dll"]