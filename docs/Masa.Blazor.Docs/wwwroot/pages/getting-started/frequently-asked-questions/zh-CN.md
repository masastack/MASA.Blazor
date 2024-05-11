# 常见问题

在特定问题上被卡住？ 在创建工单之前先回顾下这些常见的问题和答案。如果你仍然找不到你要找的东西，你可以在GitHub上提交[问题](https://github.com/masastack/MASA.Blazor/issues)，或者添加右侧的微信询问我们。

## 目录列表

- [如何垂直居中内容？](#vertical-center-content)
- [如何自动高亮对应路由的导航？](#highlight-navigation)
- [P开头的组件为什么无法使用？](#p-starting-components)
- [无法从“方法组”转换为“Microsoft.AspNetCore.Components.EventCallback”](#cannot-convert-from-method-group-to-eventcallback)
- [如何让UI紧凑？](#how-to-make-ui-compact)
- [I18n 切换语言后文本不更新](#i18n-text-not-updated)
- [避免 MMain 和 MAppBar 初次加载的过渡动画](#avoid-the-entry-animation-of-main-and-app-bar)
- [全局点击防抖](#global-click-debounce)
- [Blazor Server 内存使用过高](#blazor-server-memory-usage-too-high)

## 问题专区

- **如何垂直居中内容？** { #vertical-center-content }

  将 `fill-height` css 应用于 **MContainer**。 这个辅助类通过只增加 **height: 100%**, 但是对于容器, 它还会添加应用所需的类将内容垂直居中。

- **如何自动高亮对应路由的导航？** { #highlight-navigation }

  请访问 [导航和路由的自动匹配](/blazor/features/auto-match-nav) 以获取更多信息。

- **P开头的组件为什么无法使用？** { #p-starting-components }

  P开头的组件是预置组件，预置组件都在命名空间 **MASA.Blazor.Presets** 下。你只需写明命名空间即可使用，或者在 `_Imports.razor` 中添加全局的命名空间引用。

- **无法从“方法组”转换为“Microsoft.AspNetCore.Components.EventCallback”** { #cannot-convert-from-method-group-to-eventcallback }

  如果方法里存在泛型参数，那你需要指明泛型类型。例如在 **MSelect** 组件使用 `OnSelectedItemUpdate` 事件时，你需要指明泛型类型，如下所示：

  ``` razor l:1-3
  <MSelect TItem="string"
           TValue="string"
           TItemValue="string"
           Items="@Items"
           ItemText="item => item"
           ItemValue="item => item"
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

- **避免 MMain 和 MAppBar 初次加载的过渡动画** { #avoid-the-entry-animation-of-main-and-app-bar }

  MASA Blazor 会自动计算 **MMain** 和 **MAppBar** 的位置，而这一过程需要调用 JS 互操作，所以会有一定的延迟。当得到计算后的位置并应用后，会有一个过渡动画。
  可以通过 CSS 来避免此过渡动画。例如以本文档为例：

  ```razor
  <MApp>
    <MAppBar Class="my-app" App></MAppBar>
    <MNavigationDrawer App></MNavigationDrawer>
    <MMain Class="my-main"></MMain>
  </MApp>
  ```

  ``` css
  /* 默认的 mobile 断点值是 md,其值是 1264px*/
  /* MNavigationDrawer 的默认宽度是 300px */
  @media (min-width: 1264px) {
    /* 避免 MAppBar 的过渡动画 */
    .my-app:not(.app--sized){
      left: 300px !important;
    }
  
    / * 避免 MMain 的过渡动画 * /
    .my-main:not(.app--sized){
      padding-left: 300px !important;
    }
  }
  ```

- **全局点击防抖** { #global-click-debounce }

  自定义一个继承自 **ComponentBase** 的基类，实现 **IHandleEvent** 接口，通过 **EventCallbackWorkItem** 判断是否为异步请求，对异步请求进行防抖处理。
  所有继承该基类的组件在调用EventCallback时都会进入 **HandleEventAsync** 方法。

  ```cs MyComponentBase.cs
  public class MyComponentBase : ComponentBase, IHandleEvent
  {
      private readonly Dictionary<EventCallbackWorkItem, bool> _asyncCallbacks = new();
      private CancellationTokenSource? _cancellationTokenSource;
  
      async Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
      {
          try
          {
              // 只对鼠标点击事件进行防抖
              if (arg is MouseEventArgs { Type: "click" })
              {
                  // 只对异步请求进行防抖
                  if (IsAsyncCallback(callback))
                  {
                      _cancellationTokenSource?.Cancel();
                      _cancellationTokenSource = new();
                      await Task.Delay(300, _cancellationTokenSource.Token);
                  }
              }
  
              await callback.InvokeAsync(arg);
              StateHasChanged();
          }
          catch (TaskCanceledException)
          {
          }
      }
  
      private bool IsAsyncCallback(EventCallbackWorkItem callback)
      {
          if (_asyncCallbacks.TryGetValue(callback, out var isAsync))
          {
              return isAsync;
          }
  
          var field = callback.GetType().GetField("_delegate", BindingFlags.NonPublic | BindingFlags.Instance);
          var getInvocationListMethod = field?.FieldType.GetMethod("GetInvocationList");
          var delegates = getInvocationListMethod?.Invoke(field?.GetValue(callback), null) as Delegate[];
          isAsync = delegates?.Any(u => u.Method.ReturnType == typeof(Task)) is true;
  
          _asyncCallbacks[callback] = isAsync;
          return isAsync;
      }
  }
  ```

  ```razor Test.razor
  @inherits MyComponentBase
  
  <button @onclick="OnClick">Click me</button>
  
  @code {
      private async Task OnClick(MouseEventArgs e)
      {
          await Task.Delay(1000); // 模拟异步操作
      }
  }
  ```
- **Server 模式内存使用过高** { #blazor-server-memory-usage-too-high }

  根据[官方文档](https://learn.microsoft.com/zh-cn/aspnet/core/blazor/host-and-deploy/server?view=aspnetcore-8.0#memory-usage-applied-to-blazor)的建议，可以通过以下几点优化：
  
  - [使用工作站垃圾回收并禁用并发垃圾回收](https://learn.microsoft.com/zh-cn/dotnet/standard/garbage-collection/workstation-server-gc)
    ```xml ServerApp.csproj
    <PropertyGroup>
      <ServerGarbageCollection>false</ServerGarbageCollection>
      <ConcurrentGarbageCollection>false</ConcurrentGarbageCollection>
    </PropertyGroup>
    ```
  - 减少断开连接的线路数和缩短允许线路处于断开连接状态的时间
    ```cs Program.cs
    services.AddServerSideBlazor().AddCircuitOptions(option =>
    {
        option.DisconnectedCircuitMaxRetained = 30; // default is 100
        option.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(2); // default is 3 minutes
    });
    ```