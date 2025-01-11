FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .

FROM build AS publish
RUN dotnet publish "TuristickaAgencija.WebAPI" -c Release -o /app

FROM base AS final
WORKDIR /app

# Postavljanje ASP.NET Core URL-a
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000/tcp

# Kopiranje sadržaja iz direktorijuma publish
COPY --from=publish /app .

# Kopiranje potrebnih fajlova u kontejner
COPY ./TuristickaAgencija.WebAPI/TestImage /app/TestImage

ENTRYPOINT ["dotnet", "TuristickaAgencija.WebAPI.dll"]


