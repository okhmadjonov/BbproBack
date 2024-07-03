FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 4430


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bbpro.Api/Bbpro.Api.csproj", "Bbpro.Api/"]
RUN dotnet restore "./Bbpro.Api/./Bbpro.Api.csproj"
COPY . .
WORKDIR "/src/Bbpro.Api"
RUN dotnet build "./Bbpro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build




FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Bbpro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bbpro.Api.dll"]
