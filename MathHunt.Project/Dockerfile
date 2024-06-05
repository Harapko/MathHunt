FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MathHunt.Project/MathHunt.Project.csproj", "MathHunt.Project/"]
COPY ["MathHunt.Application/MathHunt.Application.csproj", "MathHunt.Application/"]
COPY ["MathHunt.Core/MathHunt.Core.csproj", "MathHunt.Core/"]
COPY ["MathHunt.DataAccess/MathHunt.DataAccess.csproj", "MathHunt.DataAccess/"]
RUN dotnet restore "MathHunt.Project/MathHunt.Project.csproj"
COPY . .
WORKDIR "/src/MathHunt.Project"
RUN dotnet build "MathHunt.Project.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MathHunt.Project.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "MathHunt.Project.dll"]
