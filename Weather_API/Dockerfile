FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Weather_API/Weather_API.csproj", "Weather_API/"]
RUN dotnet restore "Weather_API/Weather_API.csproj"
COPY . .
WORKDIR "/src/Weather_API"
RUN dotnet build "Weather_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Weather_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weather_API.dll"]
