FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 55081
EXPOSE 44397

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Site/ClubNet.WebSite/ClubNet.WebSite.csproj Site/ClubNet.WebSite/
RUN dotnet restore Site/ClubNet.WebSite/ClubNet.WebSite.csproj
COPY . .
WORKDIR /src/Site/ClubNet.WebSite
RUN dotnet build ClubNet.WebSite.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ClubNet.WebSite.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ClubNet.WebSite.dll"]
