# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /app
# COPY . .
# RUN dotnet restore APP/APP.csproj
# RUN dotnet publish APP/APP.csproj -c Release -o out

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# WORKDIR /app
# COPY --from=build /app/out . 
# EXPOSE 5212
# ENTRYPOINT ["dotnet", "APP.dll"]



# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . .

# Restore and build
RUN dotnet restore APP/APP.csproj
RUN dotnet build APP/APP.csproj -c Release

# Copy the JSON file next to the publish output
COPY APP/Infrastructure/Seeding/categories_dictionary.json ./APP/categories_dictionary.json

# Run tests
RUN dotnet test Cognitive.UnitTests/Cognitive.UnitTests.csproj --no-build --verbosity normal

# Publish the app
RUN dotnet publish APP/APP.csproj -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the output from build stage
COPY --from=build /app/out .

# Copy the JSON file to the runtime folder
COPY --from=build /app/APP/categories_dictionary.json ./categories_dictionary.json

EXPOSE 5212
ENTRYPOINT ["dotnet", "APP.dll"]