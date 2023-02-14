namespace Masa.Blazor.Docs.Examples.components.checkboxes
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MCheckbox<bool>.Dense), false },
        };

        public Usage() : base(typeof(MCheckbox<bool>)) { }

        bool _checkbox = true;

        string label => $"Checkbox 1: {_checkbox.ToString()}";

        protected override Dictionary<string, object>? GenAdditionalParameters()
        {
            return new Dictionary<string, object>()
            {
              { nameof(MCheckbox<bool>.Value), _checkbox },
              { nameof(MCheckbox<bool>.ValueChanged), EventCallback.Factory.Create<bool>(this, val =>
              {
                    _checkbox = val;
              }) },
              { nameof(MCheckbox<bool>.Label), label },
            };
        }
    }
}
