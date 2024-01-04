# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY . . 
RUN dotnet restore "./TODOApi/TODOApi.csproj"


# Publish the application
RUN dotnet publish "./TODOApi/TODOApi.csproj" -c Release -o /app --no-restore

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production
ENV AllowedOrigins=https://todo-production-3b06.up.railway.app

# Expose the port your application will run on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "TODOApi.dll"]