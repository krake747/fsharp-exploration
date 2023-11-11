FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ddd/BikeStore.App/BikeStore.App.fsproj", "ddd/BikeStore.App/"]
RUN dotnet restore "ddd/BikeStore.App/BikeStore.App.fsproj"
COPY . .
WORKDIR "/src/ddd/BikeStore.App"
RUN dotnet build "BikeStore.App.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BikeStore.App.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BikeStore.App.dll"]
