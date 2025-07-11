FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

# Copy project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy source code
COPY . ./

# Set environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080

# Expose the port
EXPOSE 8080

# Run the application with hot reload
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080", "--environment", "Development"]
