FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish ./Rc.Services.Notifications.Api -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS https://*:443
ENTRYPOINT dotnet Rc.Services.Notifications.Api.dll