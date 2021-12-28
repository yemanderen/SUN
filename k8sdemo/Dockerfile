#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["k8sdemo/k8sdemo.csproj", "k8sdemo/"]
RUN dotnet restore "k8sdemo/k8sdemo.csproj"
COPY . .
WORKDIR "/src/k8sdemo"
RUN dotnet build "k8sdemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "k8sdemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "k8sdemo.dll"]