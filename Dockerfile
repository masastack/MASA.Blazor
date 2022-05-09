FROM registry.cn-hangzhou.aliyuncs.com/masa/dotnet_sdk:6.0.100_nodejs16 
# FROM registry.cn-hangzhou.aliyuncs.com/masa/dotnet_sdk:6.0.100-preview.7-nodejs14.16.1
RUN apt-get update && apt-get install -y libfontconfig1
ENV LANG="zh_CN.UTF-8"
ENV LANGUAGE="zh_CN:zh"
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
WORKDIR /app
COPY . .
RUN dotnet build src/Doc/Masa.Blazor.Doc.Server -c Release
ENTRYPOINT ["dotnet","./src/Doc/Masa.Blazor.Doc.Server/bin/Release/net6.0/Masa.Blazor.Doc.Server.dll"]

