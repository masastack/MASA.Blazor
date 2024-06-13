using Element = BemIt.Element;

namespace Masa.Blazor
{
    public partial class MStepperStep : MasaComponentBase
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

        private static Block _block = new("m-stepper__step");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

        private bool _isActive;
        private bool _isInactive;

        public bool HasError => Rules.Any(validate => !validate());

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder
                .Add("active", _isActive)
                .Add(Editable)
                .Add("inactive", _isInactive)
                .Add(Complete)
                .Add("error", HasError)
                .AddColor("error", true, HasError)
                .Build();
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