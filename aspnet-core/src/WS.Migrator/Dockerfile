#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/WS.Migrator/WS.Migrator.csproj", "src/WS.Migrator/"]
COPY ["src/WS.EntityFrameworkCore/WS.EntityFrameworkCore.csproj", "src/WS.EntityFrameworkCore/"]
COPY ["src/WS.Core/WS.Core.csproj", "src/WS.Core/"]
COPY ["src/WS.Core.Shared/WS.Core.Shared.csproj", "src/WS.Core.Shared/"]
RUN dotnet restore "src/WS.Migrator/WS.Migrator.csproj"
COPY . .
WORKDIR "/src/src/WS.Migrator"
RUN dotnet build "WS.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WS.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WS.Migrator.dll", "-s"]
