FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY Ofx.Battleship.API/Battleship.API.csproj ./Ofx.Battleship.API/Battleship.API.csproj
COPY Ofx.Battleship.API.UnitTests/Battleship.API.UnitTests.csproj ./Ofx.Battleship.API.UnitTests/Battleship.API.UnitTests.csproj
COPY Ofx.Battleship.API.IntegrationTests/Battleship.API.IntegrationTests.csproj ./Ofx.Battleship.API.IntegrationTests/Battleship.API.IntegrationTests.csproj

# Copy everything else and build
COPY . ./
WORKDIR /app/Ofx.Battleship.API
RUN dotnet publish -c Release -o out -r linux-x64 -f netcoreapp3.0

# Test
WORKDIR /app/Ofx.Battleship.API.UnitTests
RUN dotnet test
WORKDIR /app/Ofx.Battleship.API.IntegrationTests
RUN dotnet test

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/Ofx.Battleship.API/out .

ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Ofx.Battleship.API.dll"]
