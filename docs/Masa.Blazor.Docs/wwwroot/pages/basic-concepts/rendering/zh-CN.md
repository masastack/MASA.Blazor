# 渲染 {#rendering}

## 基本概念 {#basic-concept}

在Blazor应用程序中，`StateHasChanged`是一个关键方法，用于通知框架组件的状态已经发生变化，需要重新渲染UI。
在许多情况下，Blazor会自动调用此方法（如在事件处理后），但在某些场景下需要手动调用它来确保UI正确更新。

重要的是，`StateHasChanged`必须在UI线程上调用。当在非UI线程（如后台任务、定时器回调或JavaScript互操作事件）中需要触发UI更新时，
必须使用`InvokeAsync(StateHasChanged)`来确保更新操作在正确的线程上执行。

## 使用方式 {#usage}

### 自动调用场景 {#automatic-invocation}

> 详情可查阅 Blazor 官方文档 [渲染](https://learn.microsoft.com/zh-CN/aspnet/core/blazor/components/rendering?view=aspnetcore-9.0#rendering-conventions-for-componentbase)。

在以下情况下，Blazor框架会自动调用`StateHasChanged`，无需手动调用：

1. **在应用更新的参数后**：当组件从父组件接收到更新的参数时。

2. **在应用更新的级联参数后**：当组件接收到新的级联参数值时。

3. **事件通知并调用事件处理程序后**：使用`EventCallback`或`EventCallback<T>`处理事件时。

   ```razor
   <MButton OnClick="@HandleClick">Counter: @_counter</MButton>
   
   @code {
       private int _counter;
       private void HandleClick()
       {
           _counter++;
           // 这里不需要调用StateHasChanged，框架会自动处理
       }
   }
   ```

### 手动调用场景 {#manual-invocation}

在以下情况下需要手动调用`StateHasChanged`：

1. **第一次渲染后**：

   在`OnAfterRender`或`OnAfterRenderAsync`生命周期方法中更改组件状态后，需要手动调用触发重新渲染：

   ```razor
   @inject IJSRuntime JS
   
   <div>窗口宽度: @_windowWidth px</div>
   
   @code {
       private int _windowWidth;

       protected override async Task OnAfterRenderAsync(bool firstRender)
       {
           if (firstRender)
           {
               _windowWidth = await JS.InvokeAsync<int>(JsInteropConstants.GetProp, "window", "innerWidth");

               // 由于已经完成了首次渲染，需要手动调用StateHasChanged触发UI更新
               StateHasChanged();
           }
       }
   }
   ```

2. **EventCallback内多次更新状态**：

   ```razor
   <MButton OnClick="LoadDataAsync">加载数据</MButton>
   
   <div>
       @if (_isLoading)
       {
           <MProgressCircular Indeterminate />
       }
       else
       {
           <span>数据项数量: @(_data?.Count ?? 0)</span>
       }
   </div>
   
   @code {
       private List<string>? _data;
       private bool _isLoading = false;
   
       private async Task LoadDataAsync()
       {
           _isLoading = true;
           // 手动调用StateHasChanged显示加载指示器
           StateHasChanged();
   
           // 模拟数据加载
           await Task.Delay(2000);
           _data = new List<string> { "项目1", "项目2", "项目3" };
           
           _isLoading = false;
           // 数据加载完成后再次调用StateHasChanged更新UI
       }
   }
   ```

3. **在非UI线程上更新UI**：

   ```razor
   @implements IDisposable
   
   <h1>当前时间: @_currentTime.ToString("HH:mm:ss")</h1>
   
   @code {
       private DateTime _currentTime = DateTime.Now;
       private Timer? _timer;
   
       protected override void OnInitialized()
       {
           // 创建一个每秒更新一次的计时器
           _timer = new Timer(_ =>
           {
               _currentTime = DateTime.Now;
               
               // 计时器回调在非UI线程上执行，必须使用InvokeAsync
               InvokeAsync(StateHasChanged);
           }, null, 0, 1000);
       }
   
       public void Dispose()
       {
           _timer?.Dispose();
       }
   }
   ```

### 组件渲染范围优化 {#component-rendering-scope}

为了避免非必要的渲染，可以将频繁更新的UI部分提取到单独的子组件中，这样子组件渲染时只会影响该部分，而不会影响父组件的其他内容。

```razor ParentComponent.razor
<div>其他内容...</div>

<!-- 提取到单独组件的频繁更新部分 -->
<ClockComponent />
```

```razor ClockComponent.razor
@implements IDisposable

<div class="clock">
    当前时间: @_currentTime.ToString("HH:mm:ss")
</div>

@code {
    private DateTime _currentTime = DateTime.Now;
    private Timer? _timer;

    protected override void OnInitialized()
    {
        // 创建一个每秒更新一次的计时器
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

## 常见问题与解决方案 {#common-issues}

### 问题：`StateHasChanged` 在某些情况下不起作用 {#not-working}

**问题描述**：在某些场景中，如实时数据流、WebSocket消息、高频传感器数据，下载进度等，外部事件触发非常频繁，导致手动调用StateHasChanged无法正常更新UI或造成严重性能问题。

**解决方案**：**设置更新阈值**：只在数据变化达到一定阈值时才触发UI更新：

```csharp
private double _currentValue;
private double _lastRenderedValue;
private readonly double _updateThreshold = 5.0; // 设置阈值为5.0

private void OnDataReceived(double newValue)
{
    _currentValue = newValue;
    
    // 只有当值变化超过阈值时才更新UI
    if (Math.Abs(_currentValue - _lastRenderedValue) >= _updateThreshold)
    {
        _lastRenderedValue = _currentValue;
        InvokeAsync(StateHasChanged);
    }
}
```
