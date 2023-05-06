namespace Masa.Blazor
{
    public class MSpeedDial : BSpeedDial
    {
        protected override void SetComponentClass()
        {
            var prefix = "m-speed-dial";

            CssProvider
              .Apply(cssBuilder =>
              {
                  cssBuilder.Add(prefix);
                  cssBuilder.AddIf($"{prefix}--top", () => Top);
                  cssBuilder.AddIf($"{prefix}--right", () => Right);
                  cssBuilder.AddIf($"{prefix}--bottom", () => Bottom);
                  cssBuilder.AddIf($"{prefix}--left", () => Left);
                  cssBuilder.AddIf($"{prefix}--absolute", () => Absolute);
                  cssBuilder.AddIf($"{prefix}--fixed", () => Fixed);
                  cssBuilder.Add($"{prefix}--direction-{Direction}");
                  cssBuilder.AddIf($"{prefix}--is-active", () => Value);
              })
              .Apply("dial-list", cssBuilder =>
              {
                  cssBuilder.Add($"{prefix}__list");
              });
        }

    }
}
