FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/CurrencyExchangeRate.API/CurrencyExchangeRate.API.csproj", "src/CurrencyExchangeRate.API/"]
COPY ["src/CurrencyExchangeRate.Application/CurrencyExchangeRate.Application.csproj", "src/CurrencyExchangeRate.Application/"]
COPY ["src/CurrencyExchangeRate.Infrastructure/CurrencyExchangeRate.Infrastructure.csproj", "src/CurrencyExchangeRate.Infrastructure/"]
RUN dotnet restore "src/CurrencyExchangeRate.API/CurrencyExchangeRate.API.csproj"
COPY . .
WORKDIR "/src/src/CurrencyExchangeRate.API"
RUN dotnet build "CurrencyExchangeRate.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CurrencyExchangeRate.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyExchangeRate.API.dll"]
