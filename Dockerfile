# https://hub.docker.com/_/microsoft-dotnet

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY *.sln .
COPY MeControla.Core/*.csproj ./MeControla.Core/
COPY MeControla.Chat.Data/*.csproj ./MeControla.Chat.Data/
COPY MeControla.Chat.DataStorage/*.csproj ./MeControla.Chat.DataStorage/
COPY MeControla.Chat.Core/*.csproj ./MeControla.Chat.Core/
COPY MeControla.Chat.Tests/*.csproj ./MeControla.Chat.Tests/
COPY MeControla.Chat.Server/*.csproj ./MeControla.Chat.Server/
RUN dotnet restore
COPY . .
WORKDIR "/src/MeControla.Chat.Server"
RUN dotnet build "MeControla.Chat.Server.csproj" -c Release -o /app/build

# copy everything else and build app
FROM build AS publish
RUN dotnet publish "MeControla.Chat.Server.csproj" -c Release -o /app/publish

# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeControla.Chat.Server.dll"]
