namespace Masa.Blazor;

public class MStepper : MSheet
{
    [Parameter] public bool Flat { get; set; }

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public bool AltLabels { get; set; }

    [Parameter] public bool NonLinear { get; set; }

    [Parameter]
    [MasaApiParameter(1)]
    public int Value
    {
        get => GetValue(1);
        set => SetValue(value);
    }

    [Parameter] public EventCallback<int> ValueChanged { get; set; }

    private readonly List<MStepperStep> _steps = new();
    private readonly List<MStepperContent> _content = new();

    private bool _isBooted;

    private bool IsReverse { get; set; }

    private Block _block = new("m-stepper");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(Flat)
                .And(_isBooted)
                .And(Vertical)
                .And(AltLabels)
                .And(NonLinear).GenerateCssClasses());
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            UpdateView();
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<int>(nameof(Value), (newVal, oldVal) =>
            {
                IsReverse = newVal < oldVal;

                if (oldVal != 0)
                {
                    _isBooted = true;
                }

                UpdateView();

                StateHasChanged();
            });
    }

    protected override RenderFragment GenBody() => builder =>
    {
        builder.OpenComponent<CascadingValue<MStepper>>(0);
        builder.AddAttribute(1, "Value", this);
        builder.AddAttribute(2, "IsFixed", true);
        builder.AddAttribute(3, "ChildContent", (RenderFragment)(sb => base.GenBody().Invoke(sb)));
        builder.CloseComponent();
    };

    public void RegisterStep(MStepperStep step)
    {
        _steps.Add(step);
    }

    public void RegisterContent(MStepperContent content)
    {
        _content.Add(content);
    }

    public void UnRegisterStep(MStepperStep stepperStep)
    {
        _steps.Remove(stepperStep);
    }

    public void UnRegisterContent(MStepperContent stepperContent)
    {
        _content.Remove(stepperContent);
    }

    private void UpdateView()
    {
        for (var index = _steps.Count; --index >= 0;)
        {
            _steps[index].Toggle(Value);
        }

        for (var index = _content.Count; --index >= 0;)
        {
            _content[index].Toggle(Value, IsReverse);
        }
    }

    public void StepClick(int step)
    {
        if (ValueChanged.HasDelegate)
        {
            ValueChanged.InvokeAsync(step);
        }
        else
        {
            Value = step;
        }
    }
}