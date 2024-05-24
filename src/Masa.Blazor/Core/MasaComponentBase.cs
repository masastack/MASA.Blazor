using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Masa.Blazor.Components.ErrorHandler;

namespace Masa.Blazor.Core;

public abstract class MasaComponentBase : MasaNextTickComponentBase, IHandleEvent
{
    protected MasaComponentBase()
    {
        _watcher = new PropertyWatcher(GetType());
    }

    [Inject] public IJSRuntime Js { get; set; } = null!;

    [Inject] private ILoggerFactory LoggerFactory { get; set; } = null!;

    [CascadingParameter] protected IDefaultsProvider? DefaultsProvider { get; set; }

    /// <summary>
    /// Disable the default value provider.
    /// Components for internal use should not be affected by the default value provider.
    /// Just for internal use.
    /// </summary>
    [CascadingParameter(Name = "DisableDefaultsProvider")]
    public bool DisableDefaultsProvider { get; set; }

    [CascadingParameter] protected IErrorHandler? ErrorHandler { get; set; }

    [Parameter] public string? Id { get; set; }

    [Parameter]
    public ForwardRef RefBack
    {
        get => _refBack ??= new ForwardRef();
        set => _refBack = value;
    }

    [Parameter(CaptureUnmatchedValues = true)]
    public virtual IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

    public static readonly string ImplementedAssemblyName = "Masa.Blazor";

    private readonly PropertyWatcher _watcher;

    private ForwardRef? _refBack;
    private bool _shouldRender = true;
    private string[] _dirtyParameters = Array.Empty<string>();

    private ElementReference _ref;
    private ElementReference? _prevRef;
    private bool _elementReferenceChanged;

    protected ILogger Logger => LoggerFactory.CreateLogger(GetType());

    #region Build class and style

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    protected virtual bool NoClass => false;
    protected virtual bool NoStyle => false;

    public string? GetClass()
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in BuildComponentClass())
        {
            stringBuilder.Append(item);
            stringBuilder.Append(' ');
        }

        if (!NoClass)
        {
            stringBuilder.Append(Class);
        }

        var css = stringBuilder.ToString().Trim();
        return css.Length == 0 ? null : css;
    }

    public string? GetStyle()
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in BuildComponentStyle())
        {
            stringBuilder.Append(item);
            stringBuilder.Append("; ");
        }

        if (!NoStyle)
        {
            stringBuilder.Append(Style);
        }

        var style = stringBuilder.ToString().TrimEnd();
        return style.Length == 0 ? null : style;
    }

    protected virtual IEnumerable<string> BuildComponentClass() => Enumerable.Empty<string>();

    protected virtual IEnumerable<string> BuildComponentStyle() => Enumerable.Empty<string>();

    #endregion

    protected virtual string ComponentName
    {
        get
        {
            var type = this.GetType();

            while (type.Assembly.GetName().Name != ImplementedAssemblyName)
            {
                if (type.BaseType is null)
                {
                    break;
                }

                type = type.BaseType;
            }

            return type.IsGenericType ? type.Name.Split('`')[0] : type.Name;
        }
    }

    /// <summary>
    /// Returned ElementRef reference for DOM element.
    /// </summary>
    public virtual ElementReference Ref
    {
        get => _ref;
        set
        {
            if (_prevRef.HasValue)
            {
                if (_prevRef.Value.Id != value.Id)
                {
                    _prevRef = value;
                    _elementReferenceChanged = true;
                }
            }
            else
            {
                _prevRef = value;
            }

            _ref = value;
            RefBack?.Set(value);
        }
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        _dirtyParameters = parameters.ToDictionary().Keys.ToArray();

        var disableDefaultsExists =
            parameters.TryGetValue<bool>(nameof(DisableDefaultsProvider), out var disableDefaults);

        if ((!disableDefaultsExists || (disableDefaultsExists && !disableDefaults))
            && parameters.TryGetValue<IDefaultsProvider>(nameof(DefaultsProvider), out var defaultsProvider)
            && defaultsProvider.Defaults is not null
            && defaultsProvider.Defaults.TryGetValue(ComponentName, out var dictionary)
            && dictionary is not null)
        {
            var defaults = ParameterView.FromDictionary(dictionary);
            defaults.SetParameterProperties(this);
        }

        await base.SetParametersAsync(parameters);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            RegisterWatchers(_watcher);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        // TODO: is this necessary?
        if (_elementReferenceChanged)
        {
            _elementReferenceChanged = false;
            await OnElementReferenceChangedAsync();
        }
    }

    protected virtual Task OnElementReferenceChangedAsync()
    {
        return Task.CompletedTask;
    }

    protected override bool ShouldRender() => _shouldRender;

    /// <summary>
    /// Check whether the parameter has been assigned value.
    /// </summary>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    protected bool IsDirtyParameter(string parameterName)
    {
        return _dirtyParameters.Contains(parameterName);
    }

    protected void InvokeStateHasChanged()
    {
        if (!IsDisposed)
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }

    protected async Task InvokeStateHasChangedAsync()
    {
        if (!IsDisposed)
        {
            await InvokeAsync(StateHasChanged);
        }
    }

    protected void PreventRenderingUtil(params Action[] actions)
    {
        _shouldRender = false;
        actions.ForEach(action => action());
        _shouldRender = true;
    }

    protected async Task PreventRenderingUtil(params Func<Task>[] funcs)
    {
        _shouldRender = false;
        await funcs.ForEachAsync(func => func());
        _shouldRender = true;
    }

    /// <summary>
    /// Register watchers at the first render.
    /// </summary>
    /// <param name="watcher"></param>
    protected virtual void RegisterWatchers(PropertyWatcher watcher)
    {
    }

    protected TValue? GetValue<TValue>(TValue? @default = default, [CallerMemberName] string name = "")
    {
        return _watcher.GetValue(@default, name);
    }

    protected TValue? GetComputedValue<TValue>([CallerMemberName] string name = "")
    {
        return _watcher.GetComputedValue<TValue>(name);
    }

    protected TValue? GetComputedValue<TValue>(Expression<Func<TValue>> valueExpression,
        [CallerMemberName] string name = "")
    {
        return _watcher.GetComputedValue(valueExpression, name);
    }

    protected TValue? GetComputedValue<TValue>(Func<TValue> valueFactory, string[] dependencyProperties,
        [CallerMemberName] string name = "")
    {
        return _watcher.GetComputedValue(valueFactory, dependencyProperties, name);
    }

    protected void SetValue<TValue>(TValue value, [CallerMemberName] string name = "",
        bool disableIListAlwaysNotifying = false)
    {
        _watcher.SetValue(value, name, disableIListAlwaysNotifying);
    }

    /// <summary>
    /// Debounce a task in microseconds. 
    /// </summary>
    /// <param name="task">A task to run.</param>
    /// <param name="millisecondsDelay">Delay in milliseconds.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the last task.</param>
    protected static async Task RunTaskInMicrosecondsAsync(Func<Task> task, int millisecondsDelay,
        CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(millisecondsDelay, cancellationToken);
            await task.Invoke();
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
    }

    /// <summary>
    /// Debounce a task in microseconds.
    /// </summary>
    /// <param name="task">A task to run.</param>
    /// <param name="millisecondsDelay">Delay in milliseconds.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the last task.</param>
    protected static async Task RunTaskInMicrosecondsAsync(Action task, int millisecondsDelay,
        CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(millisecondsDelay, cancellationToken);
            task.Invoke();
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
    }

    #region IHandleEvent

    protected virtual bool AfterHandleEventShouldRender() => true;

    Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
    {
        var task = callback.InvokeAsync(arg);
        var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
                              task.Status != TaskStatus.Canceled;

        if (AfterHandleEventShouldRender())
        {
            StateHasChanged();
        }

        return shouldAwaitTask
            ? CallStateHasChangedOnAsyncCompletion(task)
            : Task.CompletedTask;
    }

    private async Task CallStateHasChangedOnAsyncCompletion(Task task)
    {
        try
        {
            await task;
        }
        catch (Exception ex) // avoiding exception filters for AOT runtime support
        {
            // Ignore exceptions from task cancellations, but don't bother issuing a state change.
            if (task.IsCanceled)
            {
                return;
            }

            if (ErrorHandler != null)
            {
                await ErrorHandler.HandleExceptionAsync(ex);
            }
            else
            {
                throw;
            }
        }

        if (AfterHandleEventShouldRender())
        {
            StateHasChanged();
        }
    }

    #endregion
}