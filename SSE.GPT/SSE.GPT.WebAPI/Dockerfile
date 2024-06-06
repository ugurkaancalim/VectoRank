#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SSE.GPT/SSE.GPT.WebAPI/SSE.GPT.WebAPI.csproj", "SSE.GPT/SSE.GPT.WebAPI/"]
RUN dotnet restore "./SSE.GPT/SSE.GPT.WebAPI/./SSE.GPT.WebAPI.csproj"
COPY . .
WORKDIR "/src/SSE.GPT/SSE.GPT.WebAPI"
RUN dotnet build "./SSE.GPT.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SSE.GPT.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSE.GPT.WebAPI.dll"]