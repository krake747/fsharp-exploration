FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PlotyApp/PlotyApp.fsproj", "PlotyApp/"]
RUN dotnet restore "PlotyApp/PlotyApp.fsproj"
COPY . .
WORKDIR "/src/PlotyApp"
RUN dotnet build "PlotyApp.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlotyApp.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlotyApp.dll"]
