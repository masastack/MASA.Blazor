# Rendering {#rendering}

## Basic Concept {#basic-concept}

In Blazor applications, `StateHasChanged` is a key method used to notify the framework that a component's state has changed and the UI needs to be re-rendered. 
In many cases, Blazor automatically calls this method (e.g., after event handling), but in some scenarios, you need to call it manually to ensure the UI is correctly updated.

Importantly, `StateHasChanged` must be called on the UI thread. When you need to trigger UI updates from non-UI threads (such as background tasks, timer callbacks, or JavaScript interop events), 
you must use `InvokeAsync(StateHasChanged)` to ensure the update operation executes on the correct thread.

## Usage {#usage}

### Automatic Invocation Scenarios {#automatic-invocation}

> For more details, refer to the Blazor official documentation [Rendering](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/rendering?view=aspnetcore-9.0#rendering-conventions-for-componentbase).

In the following situations, the Blazor framework automatically calls `StateHasChanged`, and manual calls are not needed:

1. **After applying updated parameters**: When a component receives updated parameters from its parent component.

2. **After applying updated cascade parameters**: When a component receives new cascade parameter values.

3. **After event notification and handling**: When using `EventCallback` or `EventCallback<T>` to handle events.

    ```razor
   <MButton OnClick="@HandleClick">Counter: @_counter</MButton>
   
   @code {
       private int _counter;
       private void HandleClick()
       {
           _counter++;
           // No need to call StateHasChanged here, the framework handles it automatically
       }
   }
    ```

### Manual Invocation Scenarios {#manual-invocation}

In the following situations, you need to manually call `StateHasChanged`:

1. **After first render**:

   When changing component state in the `OnAfterRender` or `OnAfterRenderAsync` lifecycle methods, you need to manually call `StateHasChanged` to trigger re-rendering:

   ```razor
   @inject IJSRuntime JS
   
   <div>Window width: @_windowWidth px</div>
   
   @code {
       private int _windowWidth;
       
       protected override async Task OnAfterRenderAsync(bool firstRender)
       {
           if (firstRender)
           {
               _windowWidth = await JS.InvokeAsync<int>(JsInteropConstants.GetProp, "window", "innerWidth");

               // Since the first render is complete, manually call StateHasChanged to update the UI
               StateHasChanged();
           }
       }
   }
   ```

   When modifying state after the first render, Blazor does not automatically trigger re-rendering because the rendering cycle is complete. In this case, you must manually call `StateHasChanged` 
   to reflect the changes in the UI.

2. **Multiple state updates within EventCallback**:

   ```razor
   <MButton OnClick="LoadDataAsync">Load Data</MButton>
   
   <div>
       @if (_isLoading)
       {
           <MProgressCircular Indeterminate />
       }
       else
       {
           <span>Number of data items: @(_data?.Count ?? 0)</span>
       }
   </div>
   
   @code {
       private List<string>? _data;
       private bool _isLoading = false;
   
       private async Task LoadDataAsync()
       {
           _isLoading = true;
           // Manually call StateHasChanged to show the loading indicator
           StateHasChanged();
   
           // Simulate data loading
           await Task.Delay(2000);
           _data = new List<string> { "Item 1", "Item 2", "Item 3" };
           
           _isLoading = false;
           // No need to call StateHasChanged here, the framework handles it automatically
       }
   }
   ```

3. **Updating UI from non-UI threads**:

   ```razor
   @implements IDisposable
   
   <h1>Current time: @_currentTime.ToString("HH:mm:ss")</h1>
   
   @code {
       private DateTime _currentTime = DateTime.Now;
       private Timer? _timer;
   
       protected override void OnInitialized()
       {
           // Create a timer that updates every second
           _timer = new Timer(async _ =>
           {
               _currentTime = DateTime.Now;
               
               // Timer callbacks execute on non-UI threads, must use InvokeAsync
               await InvokeAsync(StateHasChanged);
           }, null, 0, 1000);
       }
   
       public void Dispose()
       {
           _timer?.Dispose();
       }
   }
   ```

### Component Rendering Scope Optimization {#component-rendering-scope}

To optimize performance, you can extract frequently updated UI parts into separate child components. This way, re-rendering will only affect the child component and not cause the entire page to re-render:

```razor ParentComponent.razor
<div>Other content...</div>

<!-- Extract frequently updated parts into a separate component -->
<ClockComponent />
```

```razor ClockComponent.razor
@implements IDisposable

<div class="clock">
    Current time: @_currentTime.ToString("HH:mm:ss")
</div>

@code {
    private DateTime _currentTime = DateTime.Now;
    private Timer? _timer;

    protected override void OnInitialized()
    {
        // Create a timer that updates every second
        _timer = new Timer(async _ =>
        {
            _currentTime = DateTime.Now;
            await InvokeAsync(StateHasChanged);
        }, null, 0, 1000);
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
```

## Common Issues and Solutions {#common-issues}

### Issue: StateHasChanged does not work as expected in some scenarios. {#not-working}

**Issue**: In some scenarios, such as real-time data streams, WebSocket messages, high-frequency sensor data, 
or download progress, external events trigger very frequently, 
causing manual calls to `StateHasChanged` to fail to update the UI correctly or cause severe performance issues.

**Solution**: **Set an update threshold**: Only trigger UI updates when the data changes exceed a certain threshold:

```csharp
private double _currentValue;
private double _lastRenderedValue;
private readonly double _updateThreshold = 5.0; // Set threshold to 5.0

private void OnDataReceived(double newValue)
{
    _currentValue = newValue;
    
    // Only update the UI when the value changes beyond the threshold
    if (Math.Abs(_currentValue - _lastRenderedValue) >= _updateThreshold)
    {
        _lastRenderedValue = _currentValue;
        InvokeAsync(StateHasChanged);
    }
}
```
