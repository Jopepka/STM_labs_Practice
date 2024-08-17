#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Lab9/Backend/Presentation/Presentation.csproj", "src/Lab9/Backend/Presentation/"]
COPY ["src/Lab9/Backend/Application/Application.csproj", "src/Lab9/Backend/Application/"]
COPY ["src/Lab9/Backend/Domain/Domain.csproj", "src/Lab9/Backend/Domain/"]
COPY ["src/Lab9/Backend/Infrastructure/Infrastructure.csproj", "src/Lab9/Backend/Infrastructure/"]
RUN dotnet restore "./src/Lab9/Backend/Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/src/Lab9/Backend/Presentation"
RUN dotnet build "./Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll", "database", "update"]