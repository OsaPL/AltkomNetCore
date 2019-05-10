FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["utcAltkomDevices.WebService/utcAltkomDevices.WebService.csproj", "utcAltkomDevices.WebService/"]
COPY ["utcAltkomDevices.FakeServices/utcAltkomDevices.FakeServices.csproj", "utcAltkomDevices.FakeServices/"]
COPY ["utcAltkomDevices.IServices/utcAltkomDevices.IServices.csproj", "utcAltkomDevices.IServices/"]
COPY ["utcAltkomDevices.Models/utcAltkomDevices.Models.csproj", "utcAltkomDevices.Models/"]
RUN dotnet restore "utcAltkomDevices.WebService/utcAltkomDevices.WebService.csproj"
COPY . .
WORKDIR "/src/utcAltkomDevices.WebService"
RUN dotnet build "utcAltkomDevices.WebService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "utcAltkomDevices.WebService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "utcAltkomDevices.WebService.dll"]