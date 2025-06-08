ARG PROJECT=RCE_Gateway
# Imagen base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagen de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG PROJECT
COPY ["${PROJECT}/${PROJECT}.csproj", "${PROJECT}/"]
RUN dotnet restore "${PROJECT}/${PROJECT}.csproj"

COPY . .
WORKDIR "/src/${PROJECT}"
RUN dotnet build "${PROJECT}.csproj" -c Release -o /app/build

# Publicación
FROM build AS publish
RUN dotnet publish "${PROJECT}.csproj" -c Release -o /app/publish

# Runtime final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ARG PROJECT
ENTRYPOINT ["dotnet", "${PROJECT}.dll"]