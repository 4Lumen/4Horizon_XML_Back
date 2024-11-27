# Use the official .NET 9.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ENV ASPNETCORE_ENVIRONMENT=Development 

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET 9.0 runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory inside the container
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out .

# Expose the port your app runs on (default for ASP.NET Core is 80)
EXPOSE 80

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "4LumenBackEnd.exe"]
