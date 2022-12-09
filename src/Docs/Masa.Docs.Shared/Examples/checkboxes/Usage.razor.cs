namespace Masa.Docs.Shared.Examples.checkboxes
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override ParameterList<bool> GenToggleParameters() => new()
        {
            { nameof(MCheckbox.Dense), false },
        };

        public Usage() : base(typeof(MCheckbox)) { }

        bool _checkbox = true;

        string label => $"Checkbox 1: {_checkbox.ToString()}";

        protected override Dictionary<string, object>? GenAdditionalParameters()
        {
            return new Dictionary<string, object>()
            {
              { nameof(MCheckbox.Value), _checkbox },
              { nameof(MCheckbox.ValueChanged), EventCallback.Factory.Create<bool>(this, val =>
              {
                    _checkbox = val;
              }) },
              { nameof(MCheckbox.Label), label },
            };
        }
    }
}
