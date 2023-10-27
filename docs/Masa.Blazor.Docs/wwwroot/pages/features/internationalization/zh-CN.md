# 国际化多语言 (i18n)

**MASA Blazor** 支持组件的语言国际化（i18n）。

## 快速入门

要设置默认语言环境，请在 _Program.cs_ 中调用 `AddMasaBlazor` 时提供 `Locale` 选项：

```csharp Program.cs
@using BlazorComponent

services.AddMasaBlazor(options => {
    // new Locale(current, fallback);
    options.Locale = new Locale("zh-CN", "en-US");
})
```

### 使用

```razor
@using BlazorComponent.I18n
@inject I18n I18n

<h1>@I18n.T("$masaBlazor.search")</h1>

<MI18n Key="$masaBlazor.search"></MI18n>
```

### 切换语言

``` razor
@using BlazorComponent.I18n
@inject I18n I18n

<MButton OnClick="ChangeLanguage">Change Language</MButton>

@code {
    private void ChangeLanguage()
    {
        I18n.SetCulture(new CultureInfo("zh-CN"));
    }
}
```

> 关于切换语言后文本不更新的问题，请移步 [常见问题](/blazor/getting-started/frequently-asked-questions)。

## 添加自定义的本地化

### 在 Blazor Server 中

- 添加 I18n 的服务依赖:

```csharp Program.cs
services.AddMasaBlazor().AddI18nForServer("i18n-local-directory-path");
```

`i18n-local-directory-path`为放置i18n资源文件的文件夹物理路径。例如,您在`wwwroot/i18n`路径下放置了i18n资源文件，则`i18n-local-directory-path`应该用`wwwroot/i18n`代替。

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - en-US.json
    - zh-CN.json
```

i18n 资源文件格式如下：

```json wwwroot/i18n/zh-CN.json
{
  "Home": "首页",
  "Docs": "文档",
  "Blog": "博客",
  "Team": "团队",
  "Search": "搜索"
}
```

```json wwwroot/i18n/en-US.json
{
    "Home": "Home",
    "Docs": "Docs",
    "Blog": "Blog",
    "Team": "Team",
    "Search": "Search"
}
```

嵌套也是支持的：

```json wwwroot/i18n/zh-CN.json
{
    "User":{
        "Name":"姓名",
        "Age":"年龄"
    },
    "Goods":{
        "Name":"名称",
        "Price":"价格"
    }
}
```

```json wwwroot/i18n/en-US.json
{
  "User":{
    "Name":"Name",
    "Age":"Age"
  },
  "Goods":{
    "Name":"Name",
    "Price":"Price"
  }
}
```

### 在 Blazor WebAssembly 中

由于Blazor WebAssembly代码在浏览器端执行，所以需要使用 http 请求来读取 i18n 资源文件，_Program.cs_ 增加代码如下：

```csharp Program.cs
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaBlazor().AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/i18n-directory-api");
```

`i18n-directory-api` 为放置i18n资源文件的文件夹路由地址。例如,您在`wwwroot/i18n`路径下放置了i18n资源文件，则`i18n-directory-api`应该用`i18n`代替。

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - supportedCultures.json
    - en-US.json
    - zh-CN.json
```

`supportedCultures.json`配置文件格式如下

```json wwwroot/i18n/supportedCultures.json
[
  "zh-CN",
  "en-US"
]
```

> 注意：`supportedCultures.json`必须与i18n资源文件在同一目录下

### 在 MAUI Blazor 中

- 添加以下扩展方法:

```csharp
public static class BlazorComponentBuilderExtensions
{
    public static IBlazorComponentBuilder AddI18nForMauiBlazor(this IBlazorComponentBuilder builder, string localesDirectory)
    {
        string supportedCulturesPath = localesDirectory + "/supportedCultures.json";
        bool existsCultures = FileSystem.AppPackageFileExistsAsync(supportedCulturesPath).Result;
        if (!existsCultures)
        {
            throw new Exception("Can't find path：" + supportedCulturesPath);
        }

        using Stream streamCultures = FileSystem.OpenAppPackageFileAsync(supportedCulturesPath).Result;
        using StreamReader readerCultures = new(streamCultures);
        string contents = readerCultures.ReadToEnd();
        string[] cultures = JsonSerializer.Deserialize<string[]>(contents) ?? throw new Exception("Failed to read supportedCultures json file data!");
        List<(string culture, Dictionary<string, string>)> locales = new();
        foreach (string culture in cultures)
        {
            string culturePath = localesDirectory + "/" + culture + ".json";
            bool existsCulture = FileSystem.AppPackageFileExistsAsync(culturePath).Result;
            if (!existsCulture)
            {
                throw new Exception("Can't find path：" + culturePath);
            }

            using Stream stream = FileSystem.OpenAppPackageFileAsync(culturePath).Result;
            using StreamReader reader = new(stream);
            Dictionary<string, string> map = I18nReader.Read(reader.ReadToEnd());
            locales.Add((culture, map));
        }

        I18nServiceCollectionExtensions.AddI18n(builder, locales.ToArray());
        return builder;
    }
}
```

MAUI Blazor静态资产受限于 Razor 组件，需要使用`FileSystem.OpenAppPackageFileAsync`来访问，详情请阅读 [微软文档](https://learn.microsoft.com/zh-cn/aspnet/core/blazor/hybrid/static-files?view=aspnetcore-7.0#static-assets-limited-to-razor-components)。MauiProgram.cs增加代码如下：

```csharp Program.cs
builder.Services.AddMasaBlazor().AddI18nForMauiBlazor("i18n-directory-path");
```

`i18n-directory-path` 为放置i18n资源文件的文件夹路径，只支持`wwwroot`和`Resources/Raw`路径下。例如，

- 在`wwwroot/i18n`路径下放置了i18n资源文件，则 `i18n-directory-path` 应该用 `wwwroot/i18n` 代替
- 在`Resources/Raw/i18n`路径下放置了i18n资源文件，则 `i18n-directory-path` 应该用 `i18n` 代替

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - supportedCultures.json
    - en-US.json
    - zh-CN.json
```

> `supportedCultures.json`配置文件格式与Blazor WebAssembly模式一致

## 支持的语言

支持下列语言：

* **af-AZ** - Afrikaans (Afrikaans)
* **ar-EG** - Arabic (اللغة العربية)
* **az** - Azerbaijani (Azərbaycan)
* **bg-BG** - Bulgarian (български)
* **ca-ES** - Catalan (català)
* **ckb** - Central Kurdish (کوردی)
* **cs-CZ** - Czech (čeština)
* **de-DE** - German (Deutsch)
* **el-GR** - Greek (Ελληνικά)
* **en-GB** - English (Global)
* **en-US** - English
* **es-ES** - Spanish (Español)
* **et-EE** - Estonian (eesti)
* **fa-IR** - Persian (فارسی)
* **fi-FI** - Finnish (suomi)
* **fr-FR** - French (Français)
* **he-IL** - Hebrew (עברית)
* **hr-HR** - Croatian (hrvatski jezik)
* **hu-HU** - Hungarian (magyar)
* **id-ID** - Indonesian (Indonesian)
* **it-IT** - Italian (Italiano)
* **ja-JP** - Japanese (日本語)
* **ko-KR** - Korean (한국어)
* **lv-LV** - Latvian (latviešu valoda)
* **nb-NO** - Norwegian (Norsk)
* **nl-BE** - Dutch (Belgium)
* **nl-NL** - Dutch (Nederlands)
* **pl-PL** - Polish (język polski)
* **pt-BR** - Portuguese (Brazil)
* **pt-PT** - Portuguese (Português)
* **ro-RO** - Romanian (Română)
* **ru-RU** - Russian (Русский)
* **sk-SK** - Slovak (slovenčina)
* **sl_SI** - Slovene (slovenski jezik)
* **sr-Cyrl-CS** - Serbian (српски језик)
* **sr-Latn-CS** - Serbian (srpski jezik)
* **sv-SE** - Swedish (svenska)
* **th-TH** - Thai (ไทย)
* **tr-TR** - Turkish (Türkçe)
* **uk-UA** - Ukrainian (Українська)
* **vi-CN** - Vietnamese (Tiếng Việt)
* **zh-CN** - Chinese (简体中文)
* **zh-TW** - Chinese (正體中文)
