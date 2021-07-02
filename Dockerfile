FROM proget-hz.lonsid.cn/masa-images/dotnet/sdk:6.0
WORKDIR /app
COPY . .
ENV LANG="zh_CN.UTF-8"
ENV LANGUAGE="zh_CN:zh"
RUN dotnet build src
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
ENTRYPOINT ["dotnet","./src/Doc/MASA.Blazor.Doc.Server/bin/Debug/net6.0/MASA.Blazor.Doc.Server.dll"]