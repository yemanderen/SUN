#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["githubsample001/githubsample001.csproj", "githubsample001/"]
RUN dotnet restore "githubsample001/githubsample001.csproj"
COPY . .
WORKDIR "/src/githubsample001"
RUN dotnet build "githubsample001.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "githubsample001.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "githubsample001.dll"]