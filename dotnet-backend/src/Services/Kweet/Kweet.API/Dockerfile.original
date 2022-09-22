#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Kweet/Kweet.API/Kweet.API.csproj", "src/Services/Kweet/Kweet.API/"]
COPY ["src/Services/Kweet/Kweet.Application/Kweet.Application.csproj", "src/Services/Kweet/Kweet.Application/"]
COPY ["src/Services/Kweet/Kweet.Domain/Kweet.Domain.csproj", "src/Services/Kweet/Kweet.Domain/"]
COPY ["src/Services/Kweet/Kweet.Infrastructure/Kweet.Infrastructure.csproj", "src/Services/Kweet/Kweet.Infrastructure/"]
RUN dotnet restore "src/Services/Kweet/Kweet.API/Kweet.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Kweet/Kweet.API"
RUN dotnet build "Kweet.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kweet.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kweet.API.dll"]