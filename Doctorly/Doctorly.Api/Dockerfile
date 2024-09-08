FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Doctorly.Api/Doctorly.Api.csproj", "Doctorly.Api/"]
RUN dotnet restore "Doctorly.Api/Doctorly.Api.csproj"
COPY . .
WORKDIR "/src/Doctorly.Api"
RUN dotnet build "Doctorly.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Doctorly.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Doctorly.Api.dll"]
