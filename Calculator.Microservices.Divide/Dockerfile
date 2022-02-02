FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Calculator.Microservices.Divide/Calculator.Microservices.Divide.csproj", "Calculator.Microservices.Divide/"]
COPY ["Calculator.Microservices.Shared.Kafka.Library/Calculator.Microservices.Shared.Kafka.Library.csproj", "Calculator.Microservices.Shared.Kafka.Library/"]
COPY ["Calculator.Microservices.Shared.Kafka.Topics/Calculator.Microservices.Shared.Kafka.Topics.csproj", "Calculator.Microservices.Shared.Kafka.Topics/"]
COPY ["Calculator.Microservices.Shared.Kafka.Commands/Calculator.Microservices.Shared.Kafka.Commands.csproj", "Calculator.Microservices.Shared.Kafka.Commands/"]

RUN dotnet restore "Calculator.Microservices.Divide/Calculator.Microservices.Divide.csproj"

COPY . .

WORKDIR "/src/Calculator.Microservices.Divide"

RUN dotnet build "Calculator.Microservices.Divide.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calculator.Microservices.Divide.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calculator.Microservices.Divide.dll"]