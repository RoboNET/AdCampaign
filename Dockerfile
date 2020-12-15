FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /

# copy everything else and build app
COPY . ./
RUN dotnet restore
WORKDIR /AdCampaign.Web
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /AdCampaign.Web/out ./
ENTRYPOINT ["dotnet", "AdCampaign.Web.dll"]