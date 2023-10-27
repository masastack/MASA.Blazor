# Internationalization (i18n)

MASA Blazor support component language internationalization (i18n).

## Getting started

To set the default locale, supply the `Locale` option when calling `AddMasaBlazor` in `Program.cs`:

```csharp Program.cs
@using BlazorComponent

services.AddMasaBlazor(options => {
    // new Locale(current, fallback);
    options.Locale = new Locale("zh-CN", "en-US");
})
```

### Usage

```razor
@using BlazorComponent.I18n
@inject I18n I18n

<h1>@I18n.T("$masaBlazor.search")</h1>

<MI18n Key="$masaBlazor.search"></MI18n>
```

### Change language

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

> For the problem that the text is not updated after switching the language, please go to [FAQ](/blazor/getting-started/frequently-asked-questions).

## Adding a custom locale

### In Blazor Server

Add the i18n service dependency:

```csharp Program.cs
services.AddMasaBlazor().AddI18nForServer("i18n-local-directory-path");
```

`i18n-local-directory-path` is the physical path of the folder where i18n resource files are placed. For example, if you place the i18n resource file under the path of `wwwroot/i18n`, the `i18n-local-directory-path` should be `wwwroot/i18n`.

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - en-US.json
    - zh-CN.json
```

The i18n resource file format is as follows:

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

Nesting is also supported:

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

### In Blazor WebAssembly

Since the Blazor WebAssembly code is executed on the browser side, it is necessary to use an http request to read the i18n resource file. The _Program.cs_ code is as follows:

```csharp Program.cs
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaBlazor().AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/i18n-directory-api");
```

`i18n-directory-api` is the routing address of the folder where i18n resource files are placed. For example, if you place the i18n resource file under the path of `wwwroot/i18n`, the `i18n-directory-api` should be `i18n`.

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - supportedCultures.json
    - en-US.json
    - zh-CN.json
```

`supportedCultures.json` configuration file format is as follows

```json wwwroot/i18n/supportedCultures.json
[
  "zh-CN",
  "en-US"
]
```

> `supportedCultures.json` must be in the same directory as the i18n resource file

### In MAUI Blazor

Add the extension method following the below:

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

MAUI Blazor Static assets limited to Razor components, You need to use `FileSystem.OpenAppPackageFileAsync` to access, read more [Microsoft Doc](https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/static-files?view=aspnetcore-7.0#static-assets-limited-to-razor-components). The MauiProgram.cs code is as follows:

```csharp Program.cs
builder.Services.AddMasaBlazor().AddI18nForMauiBlazor("i18n-directory-path");
```

`i18n-directory-path` is the path of the folder where i18n resource files are placed. Only supported under `wwroot` and `Resources/Raw` paths. For example,

- if you place the i18n resource file under the path of `wwwroot/i18n`, the `i18n-directory-path` should be `wwwroot/i18n`; 
- if you place the i18n resource file under the path of `Resources/Raw/i18n`, the `i18n-directory-path` should be `i18n`.

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - supportedCultures.json
    - en-US.json
    - zh-CN.json
```

> `supportedCultures.json` configuration file format is consistent with Blazor Web Assembly mode

## Supported languages

the following languages support:

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
