# =========================
# Base (runtime)
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# =========================
# Build (SDK)
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia apenas os csproj primeiro para otimizar cache
COPY ["Vallora.TransactionService.Api/Vallora.TransactionService.Api.csproj", "Vallora.TransactionService.Api/"]
COPY ["Vallora.TransactionService.Application/Vallora.TransactionService.Application.csproj", "Vallora.TransactionService.Application/"]
COPY ["Vallora.TransactionService.Infrastructure/Vallora.TransactionService.Infrastructure.csproj", "Vallora.TransactionService.Infrastructure/"]
COPY ["Vallora.TransactionService.Domain/Vallora.TransactionService.Domain.csproj", "Vallora.TransactionService.Domain/"]

RUN dotnet restore "Vallora.TransactionService.Api/Vallora.TransactionService.Api.csproj"

# Copia o restante do código
COPY . .

# Remove pastas com backslash no nome (ex: bin\Debug) que quebram a expansão de glob do MSBuild no Linux (dotnet/sdk#10172)
RUN find /src -depth -type d -name '*\*' -exec rm -rf {} + 2>/dev/null || true

WORKDIR "/src/Vallora.TransactionService.Api"

# =========================
# Publish
# =========================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Vallora.TransactionService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore /p:UseAppHost=false

# =========================
# Final (runtime)
# =========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vallora.TransactionService.Api.dll"]
