FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY Src/Application/Application.csproj ./Src/Application/Application.csproj
COPY Src/Domain/Domain.csproj ./Src/Domain/Domain.csproj
COPY Src/Persistence/Persistence.csproj ./Src/Persistence/Persistence.csproj
COPY Src/WebAPI/WebAPI.csproj ./Src/WebAPI/WebAPI.csproj
COPY Tests/Application.UnitTests/Application.UnitTests.csproj ./Tests/Application.UnitTests/Application.UnitTests.csproj
COPY Tests/WebAPI.IntegrationTests/WebAPI.IntegrationTests.csproj ./Tests/WebAPI.IntegrationTests/WebAPI.IntegrationTests.csproj

# Copy everything else and build
COPY . ./
WORKDIR /app/Src/WebAPI
RUN dotnet publish -c Release -o out -r linux-x64 -f netcoreapp3.0

# Test Application
WORKDIR /app/Tests/Application.UnitTests
RUN dotnet test
# Test WebAPI
WORKDIR /app/Tests/WebAPI.IntegrationTests
RUN dotnet test

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/Src/WebAPI/out .

ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Ofx.Battleship.WebAPI.dll"]
