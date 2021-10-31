#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["web/WebAdmin/WebAdmin.csproj", "web/WebAdmin/"]
RUN dotnet restore "web/WebAdmin/WebAdmin.csproj"
COPY . .
WORKDIR "/src/web/WebAdmin"
RUN dotnet build "WebAdmin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAdmin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAdmin.dll"]