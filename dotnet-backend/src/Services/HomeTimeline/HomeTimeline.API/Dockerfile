#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/HomeTimeline/HomeTimeline.API/HomeTimeline.API.csproj", "src/Services/HomeTimeline/HomeTimeline.API/"]
COPY ["src/Services/HomeTimeline/HomeTimeline.Infrastructure/HomeTimeline.Infrastructure.csproj", "src/Services/HomeTimeline/HomeTimeline.Infrastructure/"]
COPY ["src/Services/HomeTimeline/HomeTimeline.Application/HomeTimeline.Application.csproj", "src/Services/HomeTimeline/HomeTimeline.Application/"]
COPY ["src/Services/Kweet/Kweet.Domain/Kweet.Domain.csproj", "src/Services/Kweet/Kweet.Domain/"]
COPY ["src/Services/HomeTimeline/HomeTimeline.Domain/HomeTimeline.Domain.csproj", "src/Services/HomeTimeline/HomeTimeline.Domain/"]
COPY ["src/BuildingBlocks/Eventbus.Messages/Eventbus.Messages.csproj", "src/BuildingBlocks/Eventbus.Messages/"]
COPY ["src/Services/Kweet/Kweet.Application/Kweet.Application.csproj", "src/Services/Kweet/Kweet.Application/"]
RUN dotnet restore "src/Services/HomeTimeline/HomeTimeline.API/HomeTimeline.API.csproj"
COPY . .
WORKDIR "/src/src/Services/HomeTimeline/HomeTimeline.API"
RUN dotnet build "HomeTimeline.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeTimeline.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeTimeline.API.dll"]