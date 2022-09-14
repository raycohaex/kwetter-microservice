#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/UserTimeline/UserTimeline.API/UserTimeline.API.csproj", "src/Services/UserTimeline/UserTimeline.API/"]
COPY ["src/Services/Kweet/Kweet.Application/Kweet.Application.csproj", "src/Services/Kweet/Kweet.Application/"]
COPY ["src/Services/Kweet/Kweet.Domain/Kweet.Domain.csproj", "src/Services/Kweet/Kweet.Domain/"]
COPY ["src/Services/UserTimeline/UserTimeline.Application/UserTimeline.Application.csproj", "src/Services/UserTimeline/UserTimeline.Application/"]
COPY ["src/Services/UserTimeline/UserTimeline.Domain/UserTimeline.Domain.csproj", "src/Services/UserTimeline/UserTimeline.Domain/"]
COPY ["src/Services/UserTimeline/UserTimeline.Infrastructure/UserTimeline.Infrastructure.csproj", "src/Services/UserTimeline/UserTimeline.Infrastructure/"]
RUN dotnet restore "src/Services/UserTimeline/UserTimeline.API/UserTimeline.API.csproj"
COPY . .
WORKDIR "/src/src/Services/UserTimeline/UserTimeline.API"
RUN dotnet build "UserTimeline.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserTimeline.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserTimeline.API.dll"]