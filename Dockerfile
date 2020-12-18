FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

## Copy the sln
COPY quoters_server.sln .

## Copy the csproj files
COPY src/Api/Api.csproj ./src/Api/
COPY src/Services/Services.csproj ./src/Services/
COPY src/Repository/Repository.csproj ./src/Repository/
COPY src/Models/Models.csproj ./src/Models/

## Copy the test files
COPY tests/ModelsTest/ModelsTest.csproj ./tests/ModelsTest/
COPY tests/ServicesTest/ServicesTest.csproj ./tests/ServicesTest/

## Restore all projects
RUN dotnet restore

## Copy everything else and build the app
COPY src/Api/. ./src/Api/
COPY src/Services/. ./src/Services/
COPY src/Repository/. ./src/Repository/
COPY src/Models/. ./src/Models/

COPY tests/ModelsTest/. ./tests/ModelsTest/
COPY tests/ServicesTest/. ./tests/ServicesTest/

WORKDIR /app/src/Api
RUN dotnet publish --no-restore -c Release -o out

## Build the runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/src/Api/out ./
ENTRYPOINT [ "dotnet", "Api.dll" ]