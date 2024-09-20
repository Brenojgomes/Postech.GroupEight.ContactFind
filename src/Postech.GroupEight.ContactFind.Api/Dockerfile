# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["Postech.GroupEight.ContactFind/Postech.GroupEight.ContactFind.csproj", "Postech.GroupEight.ContactFind/"]
COPY ["Postech.GroupEight.ContactFind.Application/Postech.GroupEight.ContactFind.Application.csproj", "Postech.GroupEight.ContactFind.Application/"]
COPY ["Postech.GroupEight.ContactFind.Core/Postech.GroupEight.ContactFind.Core.csproj", "Postech.GroupEight.ContactFind.Core/"]
COPY ["Postech.GroupEight.ContactFind.Infrastructure/Postech.GroupEight.ContactFind.Infrastructure.csproj", "Postech.GroupEight.ContactFind.Infrastructure/"]
COPY ["Postech.GroupEight.ContactFind.UnitTests/Postech.GroupEight.ContactFind.UnitTests.csproj", "Postech.GroupEight.ContactFind.UnitTests/"]

RUN dotnet restore "Postech.GroupEight.ContactFind/Postech.GroupEight.ContactFind.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/Postech.GroupEight.ContactFind"
RUN dotnet build "Postech.GroupEight.ContactFind.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Postech.GroupEight.ContactFind.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.GroupEight.ContactFind.dll"]