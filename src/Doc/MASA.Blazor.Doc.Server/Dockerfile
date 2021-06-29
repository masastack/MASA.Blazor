#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Doc/MASA.Blazor.Doc.Server/MASA.Blazor.Doc.Server.csproj", "Doc/MASA.Blazor.Doc.Server/"]
COPY ["MASA.Blazor/MASA.Blazor.csproj", "MASA.Blazor/"]
COPY ["BlazorComponent/src/Component/BlazorComponent/BlazorComponent.csproj", "BlazorComponent/src/Component/BlazorComponent/"]
COPY ["Doc/MASA.Blazor.Doc/MASA.Blazor.Doc.csproj", "Doc/MASA.Blazor.Doc/"]
COPY ["BlazorComponent/src/Doc/BlazorComponent.Doc.CLI/BlazorComponent.Doc.CLI.csproj", "BlazorComponent/src/Doc/BlazorComponent.Doc.CLI/"]
RUN dotnet restore "Doc/MASA.Blazor.Doc.Server/MASA.Blazor.Doc.Server.csproj"
COPY . .
WORKDIR "/src/Doc/MASA.Blazor.Doc.Server"
RUN dotnet build "MASA.Blazor.Doc.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MASA.Blazor.Doc.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
ENTRYPOINT ["dotnet", "MASA.Blazor.Doc.Server.dll"]