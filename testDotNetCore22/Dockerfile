FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src 
COPY . .
RUN  dotnet build testDotNetCore22.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish testDotNetCore22.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS http://*:32081/
ENV ASPNETCORE_ENVIRONMENT test
ENV ASPNETCORE_PKG_VERSION 1.0.0
ENTRYPOINT ["dotnet", "testDotNetCore22.dll"]
