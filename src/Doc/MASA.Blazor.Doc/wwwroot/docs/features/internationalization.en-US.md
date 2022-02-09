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
services.AddMasaI18nForServer("{i18n local directory path}");
```

- `i18n local directory path` is the physical path of the folder where i18n resource files are placed. For example, if you place the i18n resource file under the path of `wwwroot/i18n`, the code is written as `services.AddMasaI18nForServer("wwwroot/i18n");`.

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
      "$DefaultLanguage": "true",
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
> `$DefaultLanguage` is the preset key, you can set the current language as the default language

- I18n usage example

 ```c#
 @inject I18n I18n

void Example()
{
    I18n.SetLang("zh-CN");//Switch language to zh-CN
    var home = I18n.T("Home");//Get the value of the language corresponding to the key value Home, this method call will return "Home";
}
```

#### If you want to save the user's i18n language configuration on the browser side to achieve the effect of using the previous language configuration every time the user accesses, then do the following instead

<br/>

````c#
@inject I18nConfig I18nConfig
@inject I18n I18n

void Example()
{
    I18nConfig.Language = "en-US";//Switch the language to en-US
    var home = I18n.T("Home");//Get the value of the language corresponding to the key value Home, this method call will return "Home";
}
````

### Support MasaI18n in Blazor WebAssembly project

<br/>

- Since the Blazor WebAssembly code is executed on the browser side, it is necessary to use an http request to read the i18n resource file. The program.cs code is as follows:

```c#
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/{i18n directory api}");
```

- `i18n directory api` is the routing address of the folder where i18n resource files are placed. For example, if you place the i18n resource file under the path of `wwwroot/i18n`, the code is written as `await builder.Services.AddMasaI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/i18n")`.

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
[
  "zh-CN",
  "en-US"
]
```

> Note: `languageConfig.json` must be in the same directory as the i18n resource file

- For an example of using I18n, please refer to Blazor Server mode, the usage method is the same as Blazor Server mode