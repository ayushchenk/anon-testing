#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AnonTesting.API/AnonTesting.API.csproj", "AnonTesting.API/"]
RUN dotnet restore "AnonTesting.API/AnonTesting.API.csproj"
COPY . .
WORKDIR "/src/AnonTesting.API"
RUN dotnet build "AnonTesting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnonTesting.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnonTesting.API.dll"]