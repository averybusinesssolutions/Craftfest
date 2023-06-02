#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Craftfest.csproj", "."]
RUN dotnet restore "./Craftfest.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Craftfest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Craftfest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Craftfest.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Craftfest.dll