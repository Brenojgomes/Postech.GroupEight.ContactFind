FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5266

ENV ASPNETCORE_URLS=http://+:5266

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Postech.GroupEight.ContactFind.Api/Postech.GroupEight.ContactFind.Api.csproj", "src/Postech.GroupEight.ContactFind.Api/"]
RUN dotnet restore "src/Postech.GroupEight.ContactFind.Api/Postech.GroupEight.ContactFind.Api.csproj"
COPY . .
WORKDIR "/src/src/Postech.GroupEight.ContactFind.Api"
RUN dotnet build "Postech.GroupEight.ContactFind.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Postech.GroupEight.ContactFind.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.GroupEight.ContactFind.Api.dll"]
