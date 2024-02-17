FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CharacterManagement.Domain/CharacterManagement.Domain.csproj", "src/CharacterManagement.Domain/"]
COPY ["src/CharacterManagement.Infrastructure/CharacterManagement.Infrastructure.csproj", "src/CharacterManagement.Infrastructure/"]
COPY ["src/CharacterManagement.Infrastructure/CharacterManagement.Infrastructure.csproj", "src/CharacterManagement.Infrastructure/"]
COPY ["src/CharacterManagement.Presentation/CharacterManagement.Presentation.csproj", "src/CharacterManagement.Presentation/"]
COPY ["src/CharacterManagement.Api/CharacterManagement.Api.csproj", "src/CharacterManagement.Api/"]
RUN dotnet restore "src/CharacterManagement.Api/CharacterManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/CharacterManagement.Api"
RUN dotnet build "CharacterManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CharacterManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CharacterManagement.Api.dll"]
