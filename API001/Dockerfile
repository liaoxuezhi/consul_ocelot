FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5001

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY API001.csproj API001/
RUN dotnet restore API001/API001.csproj
WORKDIR /src/API001
COPY . .
RUN dotnet build API001.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish API001.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API001.dll"]
