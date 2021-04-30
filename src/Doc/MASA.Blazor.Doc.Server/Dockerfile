FROM proget-hz.lonsid.cn/masa-images/library/dotnet:6.0
WORKDIR /app
COPY . .
COPY /root/.nuget/packages/antdesign/0.8.0/staticwebassets/ /root/.nuget/packages/antdesign/0.8.0/staticwebassets/
ENTRYPOINT ["dotnet", "MASA.Blazor.Doc.Server.dll"]
