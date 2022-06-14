#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/web/mvc/SE.WebApp.MVC/SE.WebApp.MVC.csproj", "src/web/mvc/SE.WebApp.MVC/"]
RUN dotnet restore "src/web/mvc/SE.WebApp.MVC/SE.WebApp.MVC.csproj"
COPY . .
WORKDIR "/src/src/web/mvc/SE.WebApp.MVC"
RUN dotnet build "SE.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SE.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SE.WebApp.MVC.dll"]