FROM node:14.19-alpine3.15 AS client 
WORKDIR /app 
COPY ["Calculator.Microservices.Client.Web.Angular/ClientApp", "."]
RUN npm install 
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Calculator.Microservices.Client.Web.Angular/Calculator.Microservices.Client.Web.Angular.csproj", "Calculator.Microservices.Client.Web.Angular/"]

RUN dotnet restore "Calculator.Microservices.Client.Web.Angular/Calculator.Microservices.Client.Web.Angular.csproj"

COPY . .

WORKDIR "/src/Calculator.Microservices.Client.Web.Angular"

RUN dotnet build "Calculator.Microservices.Client.Web.Angular.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calculator.Microservices.Client.Web.Angular.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=client /app/dist /app/ClientApp

RUN ["/bin/sh", "-c", "apk add gettext"]

ENTRYPOINT ["/bin/sh", "-c", "envsubst < /app/ClientApp/assets/env.template.js > /app/ClientApp/assets/env.js && dotnet Calculator.Microservices.Client.Web.Angular.dll"]
