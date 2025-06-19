# 国际化多语言 (i18n) {#i18n}

**MASA Blazor** 支持组件的语言国际化（i18n）。

## 快速入门 {#getting-started}

要设置默认语言环境，请在 _Program.cs_ 中调用 `AddMasaBlazor` 时提供 `Locale` 选项：

```csharp Program.cs
services.AddMasaBlazor(options => {
    // new Locale(current, fallback);
    options.Locale = new Locale("zh-CN", "en-US");
})
```

### 使用 {#usage}

```razor
@inject I18n I18n

<h1>@I18n.T("$masaBlazor.search")</h1>

<MI18n Key="$masaBlazor.search"></MI18n>
```

### 切换语言 {#change-language}

``` razor
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

## 添加自定义的本地化 {#adding-a-custom-locale}

### 准备工作 {#preparation}

- 假设你的i18n资源文件放在了 _wwwroot/i18n_ 下：

  ``` shell
  - Pages
  - Shared
  - wwwroot
    - i18n
      - supportedCultures.json # Blazor WebAssembly 和 MAUI Blazor 模式下需要
      - en-US.json
      - zh-CN.json
  ```
  
  _supportedCultures.json_ 用于读取支持的语言列表，格式如下：
  
  ```json wwwroot/i18n/supportedCultures.json
  [
    "zh-CN",
    "en-US"
  ]
  ```
  
- 在 _wwwroot/i18n_ 下创建语言资源文件，文件名为语言代码（如：`zh-CN.json`、`en-US.json`），内容为 JSON 格式的键值对：

  ```json wwwroot/i18n/zh-CN.json
  {
    "Home": "首页",
    "Docs": "文档",
    "Blog": "博客",
    "Team": "团队",
    "Search": "搜索",
    "User": {
      "Name": "姓名"
    }
  }
  ```
  
  ```json wwwroot/i18n/en-US.json
  {
    "Home": "Home",
    "Docs": "Docs",
    "Blog": "Blog",
    "Team": "Team",
    "Search": "Search",
    "User": {
      "Name": "User Name"
    }
  }
  ```

- 修改项目文件 _*.csproj_，将本地化资源文件设置“复制到输出目录”：

  ```xml
  <ItemGroup>
      <Content Update="wwwroot\i18n\*" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>
  ```

### 在 Blazor Server 中 {#in-blazor-server}

添加I18n资源：

```csharp Program.cs
services.AddMasaBlazor().AddI18nForServer("wwwroot/i18n");
```

### 在 Blazor WebAssembly 中 {#in-blazor-webassembly}

由于 Blazor WebAssembly 代码在浏览器端执行，所以需要使用 http 请求来读取 i18n 资源文件。

```csharp Program.cs
var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaBlazor().AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/wwwroot/i18n");
```

### 在 MAUI Blazor 中 {#in-maui-blazor}

- 添加以下扩展方法

  ```csharp
  public static class MasaBlazorBuilderExtensions
  {
      public static IMasaBlazorBuilder AddI18nForMauiBlazor(this IMasaBlazorBuilder builder, string localesDirectory)
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

  MAUI Blazor静态资产受限于 Razor 组件，需要使用`FileSystem.OpenAppPackageFileAsync`来访问，详情请阅读 [微软文档](https://learn.microsoft.com/zh-cn/aspnet/core/blazor/hybrid/static-files?view=aspnetcore-7.0#static-assets-limited-to-razor-components)。

- 添加i18n资源：

  ```csharp MauiProgram.cs
  // 当i18n资源放在 wwwroot/i18n 下时
  builder.Services.AddMasaBlazor().AddI18nForMauiBlazor("wwwroot/i18n");
  ```
  
  ```csharp MauiProgram.cs
  // 当i18n资源放在 Resources/Raw/i18n 下时
  builder.Services.AddMasaBlazor().AddI18nForMauiBlazor("i18n");
  ```

## 支持的语言 {#supported-languages}

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
