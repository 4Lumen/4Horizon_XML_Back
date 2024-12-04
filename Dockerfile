# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copy project files and restore dependencies
COPY . .
RUN dotnet restore "./4LumenBackEnd.csproj" --disable-parallel

# Publish the application
RUN dotnet publish "./4LumenBackEnd.csproj" -c Release -o /app --no-restore

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy built files from the build stage
COPY --from=build /app ./

# Expose port
EXPOSE 5000
EXPOSE 5001

# Set the entry point
ENTRYPOINT ["dotnet", "4LumenBackEnd.dll"]
