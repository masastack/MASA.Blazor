﻿namespace Masa.Blazor.Docs.Examples.components.editor
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        public Usage() : base(typeof(MEditor)) { }

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Basic>(0);
            builder.CloseComponent();
        };
    }
}
