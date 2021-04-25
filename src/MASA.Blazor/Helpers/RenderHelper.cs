using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor.Helpers
{
    public static class RenderHelper
    {
        public static RenderFragment RenderIcon(string icon, string originClass = "", string color = "", bool dark = false, int sequence = 0) => builder =>
       {
           builder.OpenComponent(sequence++, typeof(MIcon));

           builder.AddAttribute(sequence++, nameof(MIcon.Class), originClass);
           builder.AddAttribute(sequence++, nameof(MIcon.Color), color);
           builder.AddAttribute(sequence++, nameof(MIcon.Dark), dark);
           builder.AddAttribute(sequence++, nameof(MIcon.ChildContent), (RenderFragment)((builder) => builder.AddContent(sequence++, icon)));

           builder.CloseComponent();
       };

        public static RenderFragment RenderButton(Func<int, RenderFragment> childContent, EventCallback<MouseEventArgs> click = default, string originClass = null, bool icon = false, bool small = false, bool dark = false, int sequence = 0) => builder =>
       {
           builder.OpenComponent(sequence++, typeof(MButton));

           builder.AddAttribute(sequence++, nameof(MButton.Class), originClass);
           builder.AddAttribute(sequence++, nameof(MButton.Icon), icon);
           builder.AddAttribute(sequence++, nameof(MButton.Small), small);
           builder.AddAttribute(sequence++, nameof(MButton.Dark), dark);

           if (!click.Equals(default(EventCallback<MouseEventArgs>)))
               builder.AddAttribute(sequence++, nameof(MButton.Click), click);

           builder.AddAttribute(sequence++, nameof(MButton.ChildContent), childContent.Invoke(sequence++));

           builder.CloseComponent();
       };
    }
}
