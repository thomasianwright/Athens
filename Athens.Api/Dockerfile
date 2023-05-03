FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Athens.Api/Athens.Api.csproj", "Athens.Api/"]
COPY ["Athens.Core/Athens.Core.csproj", "Athens.Core/"]
COPY ["Athens.Abstractions/Athens.Abstractions.csproj", "Athens.Abstractions/"]
RUN dotnet restore "Athens.Api/Athens.Api.csproj"
COPY . .
WORKDIR "/src/Athens.Api"
RUN dotnet build "Athens.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Athens.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Athens.Api.dll"]
