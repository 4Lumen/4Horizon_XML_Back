# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set environment variable for ASP.NET Core environment (optional, could be Development/Production)
ENV ASPNETCORE_ENVIRONMENT=Development 

# Set the working directory inside the container
WORKDIR /app

# Copy only the .csproj files and restore the dependencies first (this helps leverage Docker caching)
COPY *.csproj ./ 
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application in Release mode and output to the 'out' folder
RUN dotnet publish -c Release -o out

# Add debug command to list contents of the out directory
RUN ls -la /app/out  # This will display the contents of 'out' during the build phase

# Use the official .NET runtime image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out ./ 

# Expose the port your application will listen on (default ASP.NET Core is 80, using 8080 here)
EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "4LumenBackEnd.dll"]
