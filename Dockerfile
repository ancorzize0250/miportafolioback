# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia primero el csproj para aprovechar cache
COPY *.sln ./
COPY miportafolio/*.csproj miportafolio/
RUN dotnet restore miportafolio/miportafolio.csproj

# Copia el resto del código
COPY . .
WORKDIR /src/miportafolio
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 10000

ENTRYPOINT ["dotnet", "miportafolio.dll"]