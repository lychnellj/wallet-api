FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY SimpleWallet.sln ./
COPY src/SimpleWallet.Api/Wallet.Api.csproj src/SimpleWallet.Api/
COPY src/SimpleWallet.Application/Wallet.Application.csproj src/SimpleWallet.Application/
COPY src/SimpleWallet.Domain/Wallet.Domain.csproj src/SimpleWallet.Domain/
COPY src/SimpleWallet.Infrastructure/Wallet.Infrastructure.csproj src/SimpleWallet.Infrastructure/

RUN dotnet restore src/SimpleWallet.Api/Wallet.Api.csproj

COPY . .
RUN dotnet publish src/SimpleWallet.Api/Wallet.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Wallet.Api.dll"]

