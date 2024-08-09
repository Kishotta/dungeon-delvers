FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
#COPY ./Directory.Build.props /src/Directory.Build.props
WORKDIR /src
#COPY ["Api/DungeonDelvers.Api/DungeonDelvers.Api.csproj", "Api/DungeonDelvers.Api/"]
COPY . .
RUN dotnet restore "Api/DungeonDelvers.Api/DungeonDelvers.Api.csproj"
WORKDIR "/src/Api/DungeonDelvers.Api"
RUN dotnet build "DungeonDelvers.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DungeonDelvers.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DungeonDelvers.Api.dll"]
