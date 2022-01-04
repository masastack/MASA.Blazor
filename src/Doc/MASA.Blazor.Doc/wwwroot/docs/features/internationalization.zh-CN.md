---
order: 5
title: 国际化多语言 (i18n)
---

MASA Blazor 未来将支持组件的语言国际化（i18n）。 让您在引导应用程序时，您可以使用 current 选项指定可用的区域和当前活动的区域。

## 语言支持

目前MASA Blazor只支持简体中文、English，后续即将提供支持下列语言：

* **af** - Afrikaans (Afrikaans)
* **ar** - Arabic (اللغة العربية)
* **az** - Azerbaijani (Azərbaycan)
* **bg** - Bulgarian (български)
* **ca** - Catalan (català)
* **ckb** - Central Kurdish (کوردی)
* **cs** - Czech (čeština)
* **de** - German (Deutsch)
* **el** - Greek (Ελληνικά)
* **en** - English
* **es** - Spanish (Español)
* **et** - Estonian (eesti)
* **fa** - Persian (فارسی)
* **fi** - Finnish (suomi)
* **fr** - French (Français)
* **he** - Hebrew (עברית)
* **hr** - Croatian (hrvatski jezik)
* **hu** - Hungarian (magyar)
* **id** - Indonesian (Indonesian)
* **it** - Italian (Italiano)
* **ja** - Japanese (日本語)
* **ko** - Korean (한국어)
* **lt** - Lithuanian (lietuvių kalba)
* **lv** - Latvian (latviešu valoda)
* **nl** - Dutch (Nederlands)
* **no** - Norwegian (Norsk)
* **pl** - Polish (język polski)
* **pt - Portuguese (Português)
* **ro** - Romanian (Română) 
* **ru** - Russian (Русский)
* **sk** - Slovak (slovenčina)
* **sl** - Slovene (slovenski jezik)
* **srCyrl** - Serbian (српски језик)
* **srLatn** - Serbian (srpski jezik)
* **sv** - Swedish (svenska)
* **th** - Thai (ไทย)
* **tr** - Turkish (Türkçe)
* **uk** - Ukrainian (Українська)
* **vi** - Vietnamese (Tiếng Việt)
* **zhHant** - Chinese (正體中文)

## 使用MASA Blazor已内置支持的I18n功能

<br/>

### 在Blazor Server项目中支持MasaI18n

<br/>

- 添加服务依赖I18n:

```c#
services.AddMasaI18nForServer(languageDirectory:"{i18n local directory path}");
```

- `i18n local directory path`为放置i18n资源文件的文件夹物理路径。例如,您在`wwwroot/i18n`路径下放置了i18n资源文件，则代码写为`services.AddMasaI18nForServer(languageDirectory:"wwwroot/i18n");`。

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - en-US.json
    - zh-CN.json
```

- i18n资源文件格式如下：

    - zh-CN.json

    ```
    {
      "Home": "首页",
      "Docs": "文档",
      "Blog": "博客",
      "Team": "团队",
      "Search": "搜索",
    }
    ```

    - en-US.json

    ```
    {
        "Home": "Home",
        "Docs": "Docs",
        "Blog": "Blog",
        "Team": "Team",
        "Search": "Search",
    }
    ```

- I18n使用示例

 ```c#
 @inject I18n I18n

void Example()
{
    I18n.SetLang("zh-CN");//将语言切换成zh-CN
    var home = I18n.T("Home");//获取键值Home对应语言的值，此方法调用将返回"首页";
}
```
### 如果您想在浏览器端保存用户的i18n语言配置来达到每次用户访问都可以使用之前的语言配置效果，则可增加如下操作

<br/>

- 添加 MasaI18n 中间件：

```c#
app.UseMasaI18nForServer();
```

- 在`_Host.cshtml`中为`App.razor`组件添加`I18nConfig`参数

```c#
@inject I18nConfig I18nConfig

<component type="typeof(App)" param-I18nConfig="@I18nConfig" render-mode="ServerPrerendered" />
```

- 在`App.razor`组件中同步`I18nConfig`数据（在您访问blazor项目时，由于http请求是在建立blazor连接之前就已经Response（如果您在App.razor设置的是ServerPrerendered预呈现，则此次http请求会执行一次呈现Blazor的代码，将会顺带Response静态的视图给客户端,在blazor建立SignalR连接后服务端会主动再次呈现一次），所以建立blazor后容器创建的实例与http请求时创建的不是同一个实例（注意：不包含预呈现，预呈现时将会是同一个实例），因此需要两边的实例同步下数据）

```c#
@inject I18n I18n
@inject I18nConfig ScopI18nConfig

[Parameter]
public I18nConfig I18nConfig { get; set; }

protected override void OnInitialized()
{
    ScopI18nConfig.Bind(I18nConfig);
    I18n.SetLang(I18nConfig.Language);
}
```

- 当用户切换语言时，将值赋值给`I18nConfig.Language`。比如用户将语言设置为en-US：

```c#
I18nConfig.Language = "en-US";
```

### 在Blazor WebAssembly项目中支持MasaI18n

<br/>

- 由于Blazor WebAssembly代码在浏览器端执行，所以需要使用http请求来读取i18n资源文件，program.cs增加代码如下：

```c#
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasm(builder.HostEnvironment.BaseAddress, "{i18n config file path}");
```

- `i18n config file path` 为i18n配置文件物理路径。例如,您在`wwwroot/i18n`路径下放置了i18n配置文件，则代码写为`services.AddMasaI18nForWasm(builder.HostEnvironment.BaseAddress,"i18n/languageConfig.json")`。

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - languageConfig.json
    - en-US.json
    - zh-CN.json
```

- `languageConfig.json`配置文件格式如下

```
{
  "DefaultLanguage": "zh-CN",
  "Languages": [
    {
      "Value": "zh-CN",
      "FilePath": "_content/MASA.Blazor.Doc/locale/zh-CN.json"
    },
    {
      "Value": "en-US",
      "FilePath": "_content/MASA.Blazor.Doc/locale/en-US.json"
    }
  ]
}
```

### 如果您想在浏览器端保存用户的i18n语言配置来达到每次用户访问都可以使用之前的语言配置效果，program.cs代码改为如下：

<br/>

```c#
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasm(builder.HostEnvironment.BaseAddress, "{i18n config file path}");

var host = builder.Build();
builder.RootComponents.Add(typeof(App), "#app", host.Services.GetMasaI18nParameter());
await host.Services.UseMasaI18nForWasm();
await host.RunAsync();
```

- 当用户切换语言时，将值赋值给`I18nConfig.Language`。

```c#
@inject I18nConfig 18nConfig

void SwitchLanguage(string language)
{
    I18nConfig.Language = language;
}
```
