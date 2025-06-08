FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG PROJECT
COPY . .
RUN dotnet restore "$PROJECT/$PROJECT.csproj"
RUN dotnet publish "$PROJECT/$PROJECT.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ARG PROJECT
ENV PROJECT=$PROJECT
ENTRYPOINT ["sh", "-c", "dotnet $PROJECT.dll"]