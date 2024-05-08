# Frequently asked questions

Stuck on a particular problem? Check some of these common gotchas before creating a ticket. If you still cannot find what you are looking for, you can submit an [issue](https://github.com/masastack/MASA.Blazor/issues) on GitHub or add wechat on the right to ask us.

## Table of contents

- [How do I vertically center content?](#vertical-center-content)
- [How do I automatically highlight the navigation corresponding to the route?](#highlight-navigation)
- [Why can't I use components starting with P?](#p-starting-components)
- [Cannot convert from 'method group' to 'EventCallback'](#cannot-convert-from-method-group-to-eventcallback)
- [How to make UI compact?](#how-to-make-ui-compact)
- [I18n text not updated after language change](#i18n-text-not-updated)
- [Avoid the entry animation of main and app bar](#avoid-the-entry-animation-of-main-and-app-bar)
- [Global click debounce](#global-click-debounce)

## Questions

- **How do I vertically center content?** { #vertical-center-content }

  Apply the `fill-height` css class to the **MContainer**. This helper class adds **height: 100%** but also adds the classes required to vertically center the content for containers.

- **How do I automatically highlight the navigation corresponding to the route?** { #highlight-navigation }

  Please visit the  [Automatic matching of navigation and route](/blazor/features/auto-match-nav) for more information.

- **Why can't I use components starting with P?** { #p-starting-components }

  The components starting with P are preset components, which are all under the namespace **MASA.Blazor.Presets**. You only need to specify the namespace to use, or add a global namespace reference in `_Imports.razor`.

- **cannot convert from 'method group' to 'EventCallback'** { #cannot-convert-from-method-group-to-eventcallback }

  If there are generic parameters in the method, you need to specify the generic type. For example, when using the `OnSelectedItemUpdate` event in the **MSelect** component, you need to specify the generic type as follows:

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
- **How to make UI compact** { #how-to-make-ui-compact }

  For now there is no one-click compact mode, but you can achieve this through non-CSS means, using [DefaultsProvider](https://docs.masastack.com/blazor/components/defaults-providers) to apply the compact style of the component:

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

- **I18n text not updated after language change** { #i18n-text-not-updated }

  - Notify the child component to refresh by changing the cascading parameter (recommended)

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

  - Notify the child component to refresh through the event of I18n

    ```razor MainLayout
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

- **Avoid the entry animation of main and app bar** { #avoid-the-entry-animation-of-main-and-app-bar }

  MASA Blazor will automatically calculate the position of **MMain** and **MAppBar**, and this process requires calling JS interop,
  so there will be some delay. After getting the calculated position and applying it, there will be a transition animation.
  It can be avoided by adding the following CSS:

  ```razor
  <MApp>
    <MAppBar Class="my-app" App></MAppBar>
    <MNavigationDrawer App></MNavigationDrawer>
    <MMain Class="my-main"></MMain>
  </MApp>
  ```

  ```css
  /* The default mobile breakpoint is md, and its value is 1264px */
  /* The default width of MNavigationDrawer is 300px */
  @media (min-width: 1264px) {
    /* Avoid the transition animation of MAppBar */
    .my-app:not(.app--sized){
      left: 300px !important;
    }
    
    /* Avoid the transition animation of MMain */
    .my-main:not(.app--sized){
      padding-left: 300px !important;
    }
  }
  ```

- **Global click debounce** { #global-click-debounce }

  Custom a base class inherited from **ComponentBase**, implement the **IHandleEvent** interface, judge whether it is an asynchronous request through **EventCallbackWorkItem**, and debounce the asynchronous request.
  All components inherited from this base class will enter the **HandleEventAsync** method when calling EventCallback.

  ```cs MyComponentBase.cs
  public class MyComponentBase : ComponentBase, IHandleEvent
  {
      private readonly Dictionary<EventCallbackWorkItem, bool> _asyncCallbacks = new();
      private CancellationTokenSource? _cancellationTokenSource;
  
      async Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
      {
          try
          {
              // Only debounce mouse click events
              if (arg is MouseEventArgs { Type: "click" })
              {
                  // Only debounce asynchronous requests
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
          await Task.Delay(1000); // Simulate asynchronous operation
      }
  }
  ```