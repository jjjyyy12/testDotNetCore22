FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY testDotNetCore22/testDotNetCore22.csproj testDotNetCore22/
COPY . .
WORKDIR /src/testDotNetCore22
RUN  ls /src/testDotNetCore22 && dotnet build testDotNetCore22.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish testDotNetCore22.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "testDotNetCore22.dll"]
