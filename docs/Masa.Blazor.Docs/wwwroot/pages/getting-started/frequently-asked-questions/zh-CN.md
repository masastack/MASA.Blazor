# 常见问题

在特定问题上被卡住？ 在创建工单之前先回顾下这些常见的问题和答案。如果你仍然找不到你要找的东西，你可以在GitHub上提交[问题](https://github.com/masastack/MASA.Blazor/issues)，或者添加右侧的微信询问我们。

## 目录列表

- [如何垂直居中内容？](#vertical-center-content)
- [如何自动高亮对应路由的导航？](#highlight-navigation)
- [P开头的组件为什么无法使用？](#p-starting-components)
- [无法从“方法组”转换为“Microsoft.AspNetCore.Components.EventCallback”](#cannot-convert-from-method-group-to-eventcallback)
- [如何让UI紧凑？](#how-to-make-ui-compact)
- [I18n 切换语言后文本不更新](#i18n-text-not-updated)

## 问题专区

- **如何垂直居中内容？** { #vertical-center-content }

  将 `fill-height` css 应用于 **MContainer**。 这个辅助类通过只增加 **height: 100%**, 但是对于容器, 它还会添加应用所需的类将内容垂直居中。

- **如何自动高亮对应路由的导航？** { #highlight-navigation }

  开启 `Routable` 参数，此参数会自动高亮对应路由的导航。支持此特性的组件包括：**MList**, **MBreadcrumbs**, **MTabs** 和 **MBottomNavigation**。

- **P开头的组件为什么无法使用？** { #p-starting-components }

  P开头的组件是预置组件，预置组件都在命名空间 **MASA.Blazor.Presets** 下。你只需写明命名空间即可使用，或者在 `_Imports.razor` 中添加全局的命名空间引用。

- **无法从“方法组”转换为“Microsoft.AspNetCore.Components.EventCallback”** { #cannot-convert-from-method-group-to-eventcallback }

  如果方法里存在泛型参数，那你需要指明泛型类型。例如在 **MSelect** 组件使用 `OnSelectedItemUpdate` 事件时，你需要指明泛型类型，如下所示：

  ``` razor l:1
  <MSelect TItem="string"
           OnSelectedItemUpdate="OnUpdate">
  </MSelect>
  ```
- **如何让UI更紧凑** { #how-to-make-ui-compact }

  目前没有提供一键式的紧凑模式，但是你可以通过非CSS的途径来实现这一点，使用 [DefaultsProvider](https://docs.masastack.com/blazor/components/defaults-providers) 应用组件的紧凑样式：

  ```cs Program.cs
  builder.Services.AddMasaBlazor(options => {
      options.Defaults = new Dictionary<string, IDictionary<string, object?>?>()
      {
          { nameof(MIcon), new Dictionary<string, object?>() { { nameof(MIcon.Dense), true } } },
          { nameof(MAlert), new Dictionary<string, object?>() { { nameof(MAlert.Dense), true } } },
          { nameof(MButton), new Dictionary<string, object?>() { { nameof(MButton.Small), true } } },
          { "MCascaderColumn", new Dictionary<string, object?>() { { "Dense", true } } },
          { nameof(MChip), new Dictionary<string, object?>() { { nameof(MChip.Small), true } } },
          { "MDataTable", new Dictionary<string, object?>() { { "Dense", true } } },
          { nameof(MSimpleTable), new Dictionary<string, object?>() { { nameof(MSimpleTable.Dense), true } } },
          { nameof(MDescriptions), new Dictionary<string, object?>() { { nameof(MDescriptions.Dense), true } } },
          { nameof(MRow), new Dictionary<string, object?>() { { nameof(MRow.Dense), true } } },
          { "MAutocomplete", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MCascader", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MCheckbox", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MFileInput", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MRadioGroup", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MRangeSlider", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MSelect", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MSlider", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MSwitch", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MTextarea", new Dictionary<string, object?>() { { "Dense", true } } },
          { "MTextField", new Dictionary<string, object?>() { { "Dense", true } } },
          { nameof(MButtonGroup), new Dictionary<string, object?>() { { nameof(MButtonGroup.Dense), true } } },
          { nameof(MListItem), new Dictionary<string, object?>() { { nameof(MListItem.Dense), true } } },
          { nameof(MRating), new Dictionary<string, object?>() { { nameof(MRating.Dense), true } } },
          { nameof(MTimeline), new Dictionary<string, object?>() { { nameof(MTimeline.Dense), true } } },
          { nameof(MToolbar), new Dictionary<string, object?>() { { nameof(MToolbar.Dense), true } } },
          { "MTreeview", new Dictionary<string, object?>() { { "Dense", true } } },
          { nameof(PImageCaptcha), new Dictionary<string, object?>() { { nameof(PImageCaptcha.Dense), true } } }
      };
  })
  ```
  
- **I18n 切换语言后文本不更新** { #i18n-text-not-updated }

  - 通过级联参数变更通知子组件刷新（推荐）

    ```razor MainLayout
    @using BlazorComponent.I18n
    @inject I18n I18n

    <MApp>
      <CascadingValue Value="@I18n.Culture.ToString()" Name="Culture">
        @* AppBar Main.. *@
      </CascadingValue>
    </MApp>
    ```  
    
    ``` razor PageOrComponent.razor
    @using BlazorComponent.I18n
    @inject I18n I18n
    
    <h1>@I18n.T("$masaBlazor.search")</h1>
    
    @code {
        [CascadingParameter(Name = "Culture")]
        public string? Culture { get; set; }
    }
    ```

  - 通过I18n的事件通知子组件刷新

    ``` razor
    @using BlazorComponent.I18n
    @inject I18n I18n
    @implements IDisposable
    
    <h1>@I18n.T("$masaBlazor.search")</h1>
    
    @code {
        protected override void OnInitialized()
        {
            I18n.CultureChange += OnCultureChange;
        }
    
        private void OnCultureChange(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
    
        public void Dispose()
        {
            I18n.CultureChange -= OnCultureChange;
        }
    }
    ```
