# Use the official .NET 10 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the fsproj and restore dependencies
COPY ["countdown.fsproj", "./"]
RUN dotnet restore "countdown.fsproj"

# Copy the rest of the source code and build the release
COPY . .
RUN dotnet build "countdown.fsproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "countdown.fsproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the lightweight ASP.NET runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port (Render automatically provides a PORT env var)
EXPOSE 8080
ENV DEFAULT_PORT=8080

ENTRYPOINT ["dotnet", "CountdownApp.dll"]
