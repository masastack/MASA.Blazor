---
order: 5
title: Internationalization (i18n)
---

MASA Blazor will support component language internationalization (i18n) in the future. When you boot the application, you can use the current option to specify the available area and the currently active area.

## Getting started

Currently MASA Blazor only supports Simplified Chinese (zhHans), and the following languages will be supported soon:

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

## Use the I18n function that MASA Blazor has built-in support

<br/>

### Support MasaI18n in Blazor Server project

<br/>

- Add service dependency I18n:

```c#
services.AddMasaI18nForServer(languageDirectory:"{i18n local directory path}");
```

- `i18n local directory path` is the physical path of the folder where i18n resource files are placed. For example, if you place an i18n resource file under the path of `wwwroot/i18n`, the code is written as `services.AddMasaI18nForServer(languageDirectory:"wwwroot/i18n");`.

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - en-US.json
    - zh-CN.json
```

- The i18n resource file format is as follows:

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

- I18n usage example

 ```c#
 @inject I18n I18n

void Example()
{
    I18n.SetLang("zh-CN");//Switch language to zh-CN
    var home = I18n.T("Home");//Get the value of the language corresponding to the key value Home, this method call will return "Home";
}
```

### If you want to save the user's i18n language configuration on the browser side so that the previous language configuration effect can be used every time the user visits, you can add the following operations

<br/>

- Add MasaI18n middleware:

```c#
app.UseMasaI18n();
```

- Add the `I18nConfig` parameter to the `App.razor` component in `_Host.cshtml`

```c#
@inject I18nConfig I18nConfig

<component type="typeof(App)" param-I18nConfig="@I18nConfig" render-mode="ServerPrerendered" />
```

- Synchronize the `I18nConfig` data in the `App.razor` component (when you access the blazor project, because the http request is the response before the blazor connection is established (if you set the ServerPrerendered pre-render in App.razor, this time The http request will execute the code that presents Blazor once, and the static view of the Response will be given to the client by the way. After the SignalR connection is established in blazor, the server will take the initiative to present it again), so the instance created by the container after the establishment of blazor is the same as the one created during the http request It is not the same instance (note: pre-rendering is not included, it will be the same instance during pre-rendering), so the data needs to be synchronized between the two instances)

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

- When the user switches languages, assign the value to `I18nConfig.Language`. For example, the user sets the language to en-US:

```c#
I18nConfig.Language = "en-US";
```

### Support MasaI18n in Blazor WebAssembly project

<br/>

- Since the Blazor WebAssembly code is executed on the browser side, it is necessary to use an http request to read the i18n resource file. The program.cs code is as follows:

```c#
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasm(builder.HostEnvironment.BaseAddress, "{i18n config file path}");
```

- `i18n config file path` is the physical path of the i18n configuration file. For example, if you place the i18n configuration file under the path of `wwwroot/i18n`, the code is written as `services.AddMasaI18nForWasm(builder.HostEnvironment.BaseAddress,"i18n/languageConfig.json")`.

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - languageConfig.json
    - en-US.json
    - zh-CN.json
```

- `languageConfig.json` configuration file format is as follows

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

### If you want to save the user's i18n language configuration on the browser side so that the previous language configuration effect can be used every time the user visits, the program.cs code is changed to the following:

<br/>

```c#
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasm(builder.HostEnvironment.BaseAddress, "{i18n config file path}");
builder.RootComponents.Add(typeof(App), "#app", await builder.Services.GetMasaI18nParameter());

await builder.Build().RunAsync();
```

- When the user switches languages, assign the value to `I18nConfig.Language`.

```c#
@inject I18nConfig 18nConfig

void SwitchLanguage(string language)
{
    I18nConfig.Language = language;
}
```