# 国际化多语言 (i18n)

**MASA Blazor** 支持组件的语言国际化（i18n）。

## 语言支持

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

## 使用MASA Blazor已内置支持的I18n功能

### 在Blazor Server项目中支持MasaI18n

- 添加服务依赖I18n:

```csharp
services.AddMasaBlazor().AddI18nForServer("{i18n local directory path}");
```

- `i18n local directory path`为放置i18n资源文件的文件夹物理路径。例如,您在`wwwroot/i18n`路径下放置了i18n资源文件，则代码写为`services.AddMasaBlazor().AddI18nForServer("wwwroot/i18n");`。

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

> `$DefaultCulture`是预置key，可以设置当前语言为默认语言

- I18n使用示例

 ```csharp
 @using BlazorComponent.I18n
 @inject I18n I18n

void Example()
{
    I18n.SetCulture("zh-CN");//将语言切换成zh-CN
    var home = I18n.T("Home");//获取键值Home对应语言的值，此方法调用将返回"首页";
}
```

#### 嵌套

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

- 使用示例

 ```csharp
 @using BlazorComponent.I18n
 @inject I18n I18n

void Example()
{
    I18n.SetCulture("zh-CN");
    var name1 = I18n.T("Goods.Name");//输出：名称
    var name2 = I18n.T("User.Name");//输出：姓名
    var name3 = I18n.T("Name",true);//输出：姓名。注意：重复的Key会默认取第一个匹配的
    var name4 = I18n.T("Goods","Name");//输出：名称
    var age1 = I18n.T("User.Age");//输出：年龄
    var age2 = I18n.T("Age",true);//输出：年龄
    var price1 = I18n.T("Goods.Price");//输出：价格
    var price2 = I18n.T("Price",true);//输出：价格

    I18n.SetLang("en-US");
    name1 = I18n.T("Goods.Name");//输出：Goods.Name
    name2 = I18n.T("User.Name");//输出：User.Name
    name3 = I18n.T("Name",true);//输出：Name
    name4 = I18n.T("Goods","Name");//输出：Name
    age1 = I18n.T("User.Age");//输出：User.Age
    age2 = I18n.T("Age",true);//输出：Age
    price1 = I18n.T("Goods.Price");//输出：Goods.Price
    price2 = I18n.T("Price",true);//输出：Price
}
```

> I18n默认key找不到对应的数据时返回key，而key一般是英文的，所以en-US.json可以根据情况不用写。如果想找不到对应的数据时返回null，则使用`I18n.T("key",whenNullReturnKey:false)`即可。
> 支持在嵌套中递归嵌套，使用方式与示例一致

### 在Blazor WebAssembly项目中支持MasaI18n

- 由于Blazor WebAssembly代码在浏览器端执行，所以需要使用http请求来读取i18n资源文件，program.cs增加代码如下：

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaBlazor().AddI18nForWasmAsync($"builder.HostEnvironment.BaseAddress/{i18n directory api}");
```

- `i18n directory api` 为放置i18n资源文件的文件夹路由地址。例如,您在`wwwroot/i18n`路径下放置了i18n资源文件，则代码写为`await builder.Services.AddMasaBlazor().AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/i18n")`。

```
- Pages 
- Shared 
- wwwroot
  - i18n
    - supportedCultures.json
    - en-US.json
    - zh-CN.json
```

- `supportedCultures.json`配置文件格式如下

```
[
  "zh-CN",
  "en-US"
]
```

> 注意：`supportedCultures.json`必须与i18n资源文件在同一目录下

- I18n使用示例请参考Blazor Server模式，使用方式与Blazor Server模式一致

