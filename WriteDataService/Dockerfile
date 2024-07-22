# Use the official .NET SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore dependencies
COPY WriteDataService/*.csproj ./
RUN dotnet restore

# Copy the rest of the files and build
COPY WriteDataService/. ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port the app will run on
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "WriteDataService.dll"]
