FROM mcr.microsoft.com/dotnet/sdk:8.0 as base
WORKDIR /app
EXPOSE 7000
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:7000

# Copy everything
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY  Video.Web/*.csproj  Video.Web/ 
COPY  Video.Infrastructure/*.csproj  Video.Infrastructure/ 
COPY  Video.Domain/*.csproj  Video.Domain/ 
COPY  Video.Application/*.csproj  Video.Application/ 

# Restore as distinct layers
RUN dotnet restore Video.Web/Video.Web.csproj

# Copy everything
COPY . ./ 

# Build and publish a release
WORKDIR "/src/Video.Web"
RUN dotnet build "Video.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Video.Web.csproj" -c Release -o /app

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Video.Web.dll", "--server.urls", "http://0.0.0.0:7000"]
