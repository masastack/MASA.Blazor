using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Test.Input
{
    /// <summary>
    /// Use to test SetComponentClass
    /// </summary>
    public class TestInput : MInput<string>
    {
        [Parameter]
        public bool? MockHasState { get; set; }

        public override bool HasState => MockHasState ?? base.HasState;

        [Parameter]
        public bool? MockShowDetails { get; set; }

        public override bool ShowDetails => MockShowDetails ?? base.ShowDetails;

        [Parameter]
        public bool? MockIsDisabled { get; set; }

        public override bool IsDisabled => MockIsDisabled ?? base.IsDisabled;

        [Parameter]
        public string MockValidationState { get; set; }

        public override string ValidationState => MockValidationState ?? base.ValidationState;

        [Parameter]
        public bool? MockIsDark { get; set; }

        public override bool IsDark => MockIsDark ?? base.IsDark;
    }
}
