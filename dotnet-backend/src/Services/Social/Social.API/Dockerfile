#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Social/Social.API/Social.API.csproj", "src/Services/Social/Social.API/"]
COPY ["src/Services/Social/Social.Infrastructure/Social.Infrastructure.csproj", "src/Services/Social/Social.Infrastructure/"]
COPY ["src/Services/Social/Social.Domain/Social.Domain.csproj", "src/Services/Social/Social.Domain/"]
COPY ["src/Services/Social/Social.Application/Social.Application.csproj", "src/Services/Social/Social.Application/"]
RUN dotnet restore "src/Services/Social/Social.API/Social.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Social/Social.API"
RUN dotnet build "Social.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Social.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Social.API.dll"]