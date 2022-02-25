FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 55419
EXPOSE 44398


# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["YoutubeApi/YoutubeApi.csproj", "YoutubeApi/"]
RUN dotnet restore "YoutubeApi/YoutubeApi.csproj"
COPY . .
WORKDIR "/src/YoutubeApi"
RUN dotnet build "YoutubeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YoutubeApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YoutubeApi.dll"]
