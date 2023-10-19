#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DogsHouse.API/DogsHouse.API.csproj", "DogsHouse.API/"]
COPY ["DogsHouse.Application/DogsHouse.Application.csproj", "DogsHouse.Application/"]
COPY ["DogsHouse.Contracts/DogsHouse.Contracts.csproj", "DogsHouse.Contracts/"]
COPY ["DogsHouse.Domain/DogsHouse.Domain.csproj", "DogsHouse.Domain/"]
COPY ["DogsHouse.Infrastructure/DogsHouse.Infrastructure.csproj", "DogsHouse.Infrastructure/"]
RUN dotnet restore "DogsHouse.API/DogsHouse.API.csproj"
COPY . .
WORKDIR "/src/DogsHouse.API"
RUN dotnet build "DogsHouse.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DogsHouse.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DogsHouse.API.dll"]