namespace Masa.Blazor
{
    public partial class MStepper : MSheet
    {
        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public bool AltLabels { get; set; }

        [Parameter]
        public bool NonLinear { get; set; }

        [Parameter]
        [MassApiParameter(1)]
        public int Value
        {
            get => GetValue(1);
            set => SetValue(value);
        }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        private readonly List<MStepperStep> _steps = new();
        private readonly List<MStepperContent> _content = new();

        private bool _isBooted;
        
        private bool IsReverse { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper")
                        .AddIf("m-stepper--flat", () => Flat)
                        .AddIf("m-stepper--is-booted", () => _isBooted)
                        .AddIf("m-stepper--vertical", () => Vertical)
                        .AddIf("m-stepper--alt-labels", () => AltLabels)
                        .AddIf("m-stepper--non-linear", () => NonLinear);
                });
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
                });
        }

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
}
