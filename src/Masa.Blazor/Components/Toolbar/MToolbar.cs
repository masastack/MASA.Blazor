﻿namespace Masa.Blazor
{
    public class MToolbar : MSheet, IToolbar
    {
        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Collapse { get; set; }

        [Parameter]
        public bool Floating { get; set; }

        [Parameter]
        public bool Prominent { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public string? Src { get; set; }

        [Parameter]
        public RenderFragment<Dictionary<string, object?>>? ImgContent { get; set; }

        [Parameter]
        public bool Short { get; set; }

        [Parameter]
        [MassApiParameter(48)]
        public StringNumber? ExtensionHeight { get; set; } = 48;

        [Parameter]
        public RenderFragment? ExtensionContent { get; set; }

        [Parameter]
        [MassApiParameter("header")]
        public override string Tag { get; set; } = "header";

        [Parameter]
        public bool Extended { get; set; }
        
        protected virtual bool IsCollapsed => Collapse;

        public bool IsExtended => Extended || ExtensionContent != null;

        protected virtual StringNumber ComputedContentHeight
        {
            get
            {
                if (Height != null)
                {
                    return Height;
                }

                if (IsProminent && Dense)
                {
                    return 96;
                }

                if (IsProminent && Short)
                {
                    return 112;
                }

                if (IsProminent)
                {
                    return 128;
                }

                if (Dense)
                {
                    return 48;
                }

                if (Short) //TODO:breakpoint
                {
                    return 56;
                }

                return 64;
            }
        }

        public StringNumber ComputedHeight
        {
            get
            {
                if (!IsExtended)
                {
                    return ComputedContentHeight;
                }

                return IsCollapsed ? ComputedContentHeight : ComputedContentHeight.ToInt32() + (ExtensionHeight?.ToInt32() ?? 0);
            }
        }

        protected virtual bool IsProminent => Prominent;

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-toolbar")
                        .AddIf("m-toolbar--absolute", () => Absolute)
                        .AddIf("m-toolbar--bottom", () => Bottom)
                        .AddIf("m-toolbar--collapse", () => Collapse)
                        .AddIf("m-toolbar--collapsed", () => IsCollapsed)
                        .AddIf("m-toolbar--dense", () => Dense)
                        .AddIf("m-toolbar--extended", () => IsExtended)
                        .AddIf("m-toolbar--flat", () => Flat)
                        .AddIf("m-toolbar--floating", () => Floating)
                        .AddIf("m-toolbar--prominent", () => IsProminent);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(ComputedHeight);
                })
                .Apply("content", cssBuilder => { cssBuilder.Add("m-toolbar__content"); }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(ComputedContentHeight);
                })
                .Apply("image", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-toolbar__image");
                })
                .Apply("extension", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-toolbar__extension");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(ExtensionHeight);
                });

            AbstractProvider
                .Merge(typeof(BSheetBody<>), typeof(BToolbarBody<IToolbar>))
                .Apply(typeof(IImage), typeof(MImage), attrs =>
                {
                    attrs[nameof(MImage.Height)] = ComputedHeight;
                    attrs[nameof(MImage.Src)] = Src;
                });
        }
    }
}
