#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
 COPY ["CertIdentityTest.csproj", ""]
 RUN dotnet restore "./CertIdentityTest.csproj"
 COPY . .
 WORKDIR "/src/."
 RUN dotnet build "CertIdentityTest.csproj" -c Release -o /app/build
 FROM build AS publish
 RUN dotnet publish "CertIdentityTest.csproj" -c Release -o /app/publish
 FROM base AS final
 WORKDIR /app
 COPY --from=publish /app/publish .
 RUN --mount=type=secret,id=mysecret,dst=/run/secrets/mysecret cat /run/secrets/mysecret
 ENTRYPOINT ["dotnet", "CertIdentityTest.dll"]