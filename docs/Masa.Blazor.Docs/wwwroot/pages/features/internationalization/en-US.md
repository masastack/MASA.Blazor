# Internationalization (i18n)

MASA Blazor support component language internationalization (i18n).

## Getting started

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

## Use the I18n function that MASA Blazor has built-in support

### Support MasaI18n in Blazor Server project

- Add service dependency I18n:

```csharp
services.AddMasaBlazor().AddI18nForServer("{i18n local directory path}");
```

- `i18n local directory path` is the physical path of the folder where i18n resource files are placed. For example, if you place the i18n resource file under the path of `wwwroot/i18n`, the code is written as `services.AddMasaBlazor().AddI18nForServer("wwwroot/i18n");`.

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
      "$DefaultCulture": "true",
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
> `$DefaultCulture` is the preset key, you can set the current language as the default language

- I18n usage example

 ```csharp
 @using BlazorComponent.I18n
 @inject I18n I18n

void Example()
{
    I18n.SetCulture("zh-CN");//Switch language to zh-CN
    var home = I18n.T("Home");//Get the value of the language corresponding to the key value Home, this method call will return "Home";
}
```

#### Nested

- zh-CN.json

```
{
    "User":{
        "Name":"姓名",
        "Age":"年龄",
    },
    "Goods":{
        "Name":"名称",
        "Price":"价格"
    }
}
```

- en-US.json

```
{
    "User":{
        "Name":"Name",
        "Age":"Age",
    },
    "Goods":{
        "Name":"Name",
        "Price":"Price"
    }
}
```

- Usage example

 ```csharp
 @using BlazorComponent.I18n
 @inject I18n I18n

void Example()
{
    I18n.SetCulture("zh-CN");
    var name1 = I18n.T("Goods.Name");//output：名称
    var name2 = I18n.T("User.Name");//output：姓名
    var name3 = I18n.T("Name",true);//output：姓名. Note: Duplicate keys will take the first matching key by default
    var name4 = I18n.T("Goods","Name");//output：名称
    var age1 = I18n.T("User.Age");//output：年龄
    var age2 = I18n.T("Age",true);//output：年龄
    var price1 = I18n.T("Goods.Price");//output：价格
    var price2 = I18n.T("Price",true);//output：价格

    I18n.SetCulture("en-US");
    name1 = I18n.T("Goods.Name");//output：Goods.Name
    name2 = I18n.T("User.Name");//output：User.Name
    name3 = I18n.T("Name",true);//output：Name
    name4 = I18n.T("Goods","Name");//output：Name
    age1 = I18n.T("User.Age");//output：User.Age
    age2 = I18n.T("Age",true);//output：Age
    price1 = I18n.T("Goods.Price");//output：Goods.Price
    price2 = I18n.T("Price",true);//output：Price
}
```

> The default key of I18n returns the key when the corresponding data cannot be found, and the key is generally in English, so en-US.json can be written according to the situation. If you want to return null when the corresponding data cannot be found, use `I18n.T("key",whenNullReturnKey:false)`.
> Support recursive nesting in nesting, use the same way as the example

### Support MasaI18n in Blazor WebAssembly project

- Since the Blazor WebAssembly code is executed on the browser side, it is necessary to use an http request to read the i18n resource file. The program.cs code is as follows:

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaBlazor().AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/{i18n directory api}");
```

- `i18n directory api` is the routing address of the folder where i18n resource files are placed. For example, if you place the i18n resource file under the path of `wwwroot/i18n`, the code is written as `await builder.Services.AddMasaBlazor().AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/i18n")`.

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - supportedCultures.json
    - en-US.json
    - zh-CN.json
```

- `supportedCultures.json` configuration file format is as follows

```
[
  "zh-CN",
  "en-US"
]
```

> Note: `supportedCultures.json` must be in the same directory as the i18n resource file

- For an example of using I18n, please refer to Blazor Server mode, the usage method is the same as Blazor Server mode
