FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["OcelotGateway/OcelotGateway.csproj", "OcelotGateway/"]
RUN dotnet restore "OcelotGateway/OcelotGateway.csproj"
COPY . .
WORKDIR "/src/OcelotGateway"
RUN dotnet build "OcelotGateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OcelotGateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OcelotGateway.dll"]