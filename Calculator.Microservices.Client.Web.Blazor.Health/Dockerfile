FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Calculator.Microservices.Client.Web.Blazor.Health/Calculator.Microservices.Client.Web.Blazor.Health.csproj", "Calculator.Microservices.Client.Web.Blazor.Health/"]

RUN dotnet restore "Calculator.Microservices.Client.Web.Blazor.Health/Calculator.Microservices.Client.Web.Blazor.Health.csproj"

COPY . .

WORKDIR "/src/Calculator.Microservices.Client.Web.Blazor.Health"

RUN dotnet build "Calculator.Microservices.Client.Web.Blazor.Health.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calculator.Microservices.Client.Web.Blazor.Health.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calculator.Microservices.Client.Web.Blazor.Health.dll"]