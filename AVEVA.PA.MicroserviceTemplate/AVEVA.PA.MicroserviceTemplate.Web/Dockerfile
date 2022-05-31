#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AVEVA.PA.MicroserviceTemplate.Web/AVEVA.PA.MicroserviceTemplate.Web.csproj", "AVEVA.PA.MicroserviceTemplate.Web/"]
RUN dotnet restore "AVEVA.PA.MicroserviceTemplate.Web/AVEVA.PA.MicroserviceTemplate.Web.csproj"
COPY . .
WORKDIR "/src/AVEVA.PA.MicroserviceTemplate.Web"
RUN dotnet build "AVEVA.PA.MicroserviceTemplate.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AVEVA.PA.MicroserviceTemplate.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AVEVA.PA.MicroserviceTemplate.Web.dll"]