FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Starter.API/Starter.API.csproj", "Starter.API/"]
RUN dotnet restore "Starter.API/Starter.API.csproj"
COPY . .
WORKDIR "/src/Starter.API"
RUN dotnet build "Starter.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Starter.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Starter.API.dll"]