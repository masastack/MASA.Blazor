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
        public int Value
        {
            get
            {
                return GetValue(1);
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        protected bool IsReverse { get; set; }

        protected bool IsBooted { get; set; } = true;

        protected List<MStepperStep> Steps = new();

        protected List<MStepperContent> Content = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher
                .Watch<int>(nameof(Value), (newVal, oldVal) =>
                {
                    IsReverse = newVal < oldVal;

                    if (oldVal != 0)
                    {
                        IsBooted = true;
                    }

                    UpdateView();
                });
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper")
                        .AddIf("m-stepper--flat", () => Flat)
                        .AddIf("m-stepper--is-booted", () => IsBooted)
                        .AddIf("m-stepper--vertical", () => Vertical)
                        .AddIf("m-stepper--alt-labels", () => AltLabels)
                        .AddIf("m-stepper--non-linear", () => NonLinear);
                });
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                UpdateView();
            }
        }

        public void RegisterStep(MStepperStep step)
        {
            Steps.Add(step);
        }

        public void RegisterContent(MStepperContent content)
        {
            Content.Add(content);
        }

        public void UnRegisterStep(MStepperStep stepperStep)
        {
            Steps.Remove(stepperStep);
        }

        public void UnRegisterContent(MStepperContent stepperContent)
        {
            Content.Remove(stepperContent);
        }

        public void UpdateView()
        {
            for (var index = Steps.Count; --index >= 0;)
            {
                Steps[index].Toggle(Value);
            }

            for (var index = Content.Count; --index >= 0;)
            {
                Content[index].Toggle(Value, IsReverse);
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
