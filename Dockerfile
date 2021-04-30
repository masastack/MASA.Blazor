FROM proget-hz.lonsid.cn/masa-images/library/dotnet:6.0
WORKDIR /app
COPY . .
RUN git clone -b develop https://github.com/BlazorComponent/BlazorComponent.git && dotnet build src
ENTRYPOINT ["./src/Doc/MASA.Blazor.Doc.Server/bin/Debug/net6.0/MASA.Blazor.Doc.Server.dll"]
