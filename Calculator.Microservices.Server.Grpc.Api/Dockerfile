FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Calculator.Microservices.Server.Grpc.Api/Calculator.Microservices.Server.Grpc.Api.csproj", "Calculator.Microservices.Server.Grpc.Api/"]

RUN dotnet restore "Calculator.Microservices.Server.Grpc.Api/Calculator.Microservices.Server.Grpc.Api.csproj"

COPY . .

WORKDIR "/src/Calculator.Microservices.Server.Grpc.Api"

RUN dotnet build "Calculator.Microservices.Server.Grpc.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calculator.Microservices.Server.Grpc.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calculator.Microservices.Server.Grpc.Api.dll"]