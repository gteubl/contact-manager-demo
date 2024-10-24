FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContactManagerDemo.RestApi/ContactManagerDemo.RestApi.csproj", "ContactManagerDemo.RestApi/"]
COPY ["ContactManagerDemo.Infrastructure/ContactManagerDemo.Infrastructure.csproj", "ContactManagerDemo.Infrastructure/"]
COPY ["ContactManagerDemo.Domain/ContactManagerDemo.Domain.csproj", "ContactManagerDemo.Domain/"]
RUN dotnet restore "ContactManagerDemo.RestApi/ContactManagerDemo.RestApi.csproj"
COPY . .
WORKDIR "/src/ContactManagerDemo.RestApi"
RUN dotnet build "ContactManagerDemo.RestApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ContactManagerDemo.RestApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactManagerDemo.RestApi.dll"]
