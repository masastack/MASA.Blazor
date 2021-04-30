FROM proget-hz.lonsid.cn/masa-images/library/dotnet:6.0
WORKDIR /app
COPY . .
RUN dotnet build src
ENTRYPOINT ["dotnet","./src/Doc/MASA.Blazor.Doc.Server/bin/Debug/net6.0/MASA.Blazor.Doc.Server.dll"]
