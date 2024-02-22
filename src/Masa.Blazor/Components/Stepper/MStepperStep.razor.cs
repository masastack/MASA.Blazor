namespace Masa.Blazor
{
    public partial class MStepperStep : BDomComponentBase
    {
        [CascadingParameter] public MStepper? Stepper { get; set; }

        [Parameter] public int Step { get; set; }

        [Parameter] public string ErrorIcon { get; set; } = "$error";

        [Parameter] public string CompleteIcon { get; set; } = "$complete";

        [Parameter] public string EditIcon { get; set; } = "$edit";

        [Parameter] public List<Func<bool>> Rules { get; set; } = new();

        [Parameter] public string? Color { get; set; } = "primary";

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public bool Editable { get; set; }

        [Parameter] public bool Complete { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        private bool _isActive;
        private bool _isInactive;

        public bool HasError => Rules.Any(validate => !validate());

        protected override void SetComponentCss()
        {
            CssProvider.UseBem("m-stepper")
                .Element("step", css =>
                {
                    css.Modifiers(m => m.Modifier("active", _isActive)
                        .And(Editable)
                        .And("inactive", _isInactive)
                        .And(Complete)
                        .And("error", HasError)
                        .AddColor("error", true, HasError));
                }, style => { style.Add(Style); }).Apply("content", css =>
                {
                    css.Add("m-stepper__step__step")
                        .AddBackgroundColor(Color, () => !HasError && (Complete || _isActive));
                }).Element("label");
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Stepper?.RegisterStep(this);
        }

        private async Task HandleOnClick(MouseEventArgs args)
        {
            await OnClick.InvokeAsync(args);

            if (Editable)
            {
                Stepper?.StepClick(Step);
            }
        }

        private async Task HandleOnKeyDown(KeyboardEventArgs args)
        {
            if (args.Key is " " or "Spacebar")
            {
                await HandleOnClick(new MouseEventArgs()
                {
                    AltKey = args.AltKey,
                    CtrlKey = args.CtrlKey,
                    MetaKey = args.MetaKey,
                    ShiftKey = args.ShiftKey,
                });
            }
        }

        public void Toggle(int step)
        {
            _isActive = this.Step == step;
            _isInactive = step < this.Step;

            StateHasChanged();
        }

        protected override ValueTask DisposeAsyncCore()
        {
            Stepper?.UnRegisterStep(this);

            return base.DisposeAsyncCore();
        }
    }
}