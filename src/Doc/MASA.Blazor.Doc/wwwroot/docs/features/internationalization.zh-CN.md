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
services.AddMasaI18nForServer("{i18n local directory path}");
```

- `i18n local directory path`为放置i18n资源文件的文件夹物理路径。例如,您在`wwwroot/i18n`路径下放置了i18n资源文件，则代码写为`services.AddMasaI18nForServer("wwwroot/i18n");`。

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

> `$DefaultLanguage`是预置key，可以设置当前语言为默认语言

- I18n使用示例

 ```c#
 @inject I18n I18n

void Example()
{
    I18n.SetLang("zh-CN");//将语言切换成zh-CN
    var home = I18n.T("Home");//获取键值Home对应语言的值，此方法调用将返回"首页";
}
```
#### 如果您想在浏览器端保存用户的i18n语言配置来达到每次用户访问都可以使用之前的语言配置效果，则改为如下操作

<br/>

 ```c#
 @inject I18nConfig I18nConfig
 @inject I18n I18n

void Example()
{
    I18nConfig.Language = "en-US";//将语言切换成en-US
    var home = I18n.T("Home");//获取键值Home对应语言的值，此方法调用将返回"Home";
}
```

### 在Blazor WebAssembly项目中支持MasaI18n

<br/>

- 由于Blazor WebAssembly代码在浏览器端执行，所以需要使用http请求来读取i18n资源文件，program.cs增加代码如下：

```c#
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasmAsync($"builder.HostEnvironment.BaseAddress/{i18n directory api}");
```

- `i18n directory api` 为放置i18n资源文件的文件夹路由地址。例如,您在`wwwroot/i18n`路径下放置了i18n资源文件，则代码写为`await builder.Services.AddMasaI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/i18n")`。

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
[
  "zh-CN",
  "en-US"
]
```

> 注意：`languageConfig.json`必须与i18n资源文件在同一目录下

- I18n使用示例请参考Blazor Server模式，使用方式与Blazor Server模式一致

