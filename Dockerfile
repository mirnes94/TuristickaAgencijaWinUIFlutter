FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .

FROM build AS publish
RUN dotnet publish "TuristickaAgencija.WebAPI" -c Release -o /app
FROM base AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000/tcp

COPY --from=publish /app .

ENTRYPOINT ["dotnet", "TuristickaAgencija.WebAPI.dll"]

