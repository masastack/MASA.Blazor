# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
# RUN curl -fsSL https://deb.nodesource.com/setup_current.x | bash - && apt-get install -y nodejs
# RUN apt-get update && apt-get install -y libfontconfig1

FROM registry.cn-hangzhou.aliyuncs.com/masa/dotnet_sdk:7.0_nodejs17_libfontconfig1
# FROM registry.cn-hangzhou.aliyuncs.com/masa/dotnet_sdk:6.0.100_nodejs16_libfontconfig1
# FROM registry.cn-hangzhou.aliyuncs.com/masa/dotnet_sdk:6.0.100-preview.7-nodejs14.16.1
# RUN apt-get update && apt-get install -y libfontconfig1
ENV LANG="zh_CN.UTF-8"
ENV LANGUAGE="zh_CN:zh"
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
WORKDIR /app
COPY . .
RUN dotnet build docs/Masa.Docs.Server -c Release
ENTRYPOINT ["dotnet","./docs/Masa.Docs.Server/bin/Release/net7.0/Masa.Docs.Server.dll"]
