#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/WS.Web.Host/WS.Web.Host.csproj", "src/WS.Web.Host/"]
COPY ["src/WS.Web.Core/WS.Web.Core.csproj", "src/WS.Web.Core/"]
COPY ["src/WS.Application/WS.Application.csproj", "src/WS.Application/"]
COPY ["src/WS.Application.Shared/WS.Application.Shared.csproj", "src/WS.Application.Shared/"]
COPY ["src/WS.Core.Shared/WS.Core.Shared.csproj", "src/WS.Core.Shared/"]
COPY ["src/WS.Core/WS.Core.csproj", "src/WS.Core/"]
COPY ["src/WS.EntityFrameworkCore/WS.EntityFrameworkCore.csproj", "src/WS.EntityFrameworkCore/"]
COPY ["src/WS.GraphQL/WS.GraphQL.csproj", "src/WS.GraphQL/"]
RUN dotnet restore "src/WS.Web.Host/WS.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/WS.Web.Host"
RUN dotnet build "WS.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WS.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WS.Web.Host.dll"]
