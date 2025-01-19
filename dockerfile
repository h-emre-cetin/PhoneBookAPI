FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file
COPY *.sln .

# Copy project files
COPY src/PhoneBookAPI.Core/*.csproj ./src/PhoneBookAPI.Core/
COPY src/PhoneBookAPI.Infrastructure/*.csproj ./src/PhoneBookAPI.Infrastructure/
COPY src/PhoneBookAPI.Application/*.csproj ./src/PhoneBookAPI.Application/
COPY src/PhoneBookAPI.API/*.csproj ./src/PhoneBookAPI.API/
COPY tests/PhoneBookAPI.UnitTests/*.csproj ./tests/PhoneBookAPI.UnitTests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY src/ ./src/
COPY tests/ ./tests/

# Build and publish
RUN dotnet publish -c Release -o out src/PhoneBookAPI.API/PhoneBookAPI.API.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "PhoneBookAPI.API.dll"]