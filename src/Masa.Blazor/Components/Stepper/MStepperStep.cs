﻿namespace Masa.Blazor
{
    public partial class MStepperStep : BStepperStep, IStepperStep
    {
        [CascadingParameter]
        public MStepper? Stepper { get; set; }

        [Parameter]
        public int Step { get; set; }

        [Parameter]
        public string ErrorIcon { get; set; } = "$error";

        [Parameter]
        public string CompleteIcon { get; set; } = "$complete";

        [Parameter]
        public string EditIcon { get; set; } = "$edit";

        [Parameter]
        public List<Func<bool>> Rules { get; set; } = new();

        [Parameter]
        public string? Color { get; set; } = "primary";

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        [Parameter]
        public bool Complete { get; set; }

        private bool _isActive;
        private bool _isInactive;

        public bool HasError => Rules.Any(validate => !validate());

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__step")
                        .AddIf("m-stepper__step--active", () => _isActive)
                        .AddIf("m-stepper__step--editable", () => Editable)
                        .AddIf("m-stepper__step--inactive", () => _isInactive)
                        .AddIf("m-stepper__step--error error--text", () => HasError)
                        .AddIf("m-stepper__step--complete", () => Complete);
                })
                .Apply("step", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__step__step")
                        .AddBackgroundColor(Color, () => !HasError && (Complete || _isActive));
                })
                .Apply("label", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__label");
                });

            AbstractProvider
                .ApplyStepperStepDefault()
                .Apply<BIcon, MIcon>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Stepper?.RegisterStep(this);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Attributes["ripple"] = Editable;
        }

        protected override Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync();
            }

            if (Editable)
            {
                Stepper?.StepClick(Step);
            }

            return base.HandleOnClickAsync(args);
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
