using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public class MToolbar : MSheet, IThemeable, IToolbar
    {
        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Collapse { get; set; }

        public bool IsCollapsed => Collapse;

        public bool IsExtended => Extended || ExtensionContent != null;

        [Parameter]
        public bool Floating { get; set; }

        [Parameter]
        public bool Prominent { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public string Src { get; set; }

        [Parameter]
        public RenderFragment<ImgProps> ImgContent { get; set; }

        [Parameter]
        public bool Short { get; set; }

        [Parameter]
        public StringNumber ExtensionHeight { get; set; } = 48;

        public StringNumber ComputedContentHeight
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

                if (Short)//TODO:breakpoint
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

                return IsCollapsed ? ComputedContentHeight : ComputedContentHeight.ToInt32() + ExtensionHeight.ToInt32();
            }
        }

        [Parameter]
        public RenderFragment ExtensionContent { get; set; }

        [Parameter]
        public override string Tag { get; set; } = "header";

        [Parameter]
        public bool Extended { get; set; }

        public bool IsProminent => Prominent;

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
                .Apply("content", cssBuilder =>
                {
                    cssBuilder.Add("m-toolbar__content");
                }, styleBuilder =>
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
                .Merge(typeof(BSheetBody<>), typeof(BToolbarBody<IToolbar>), props =>
                {
                    //TODO: Remove this
                    props[nameof(BToolbarBody<IToolbar>.CssProvider)] = CssProvider;
                    props[nameof(BToolbarBody<IToolbar>.AbstractProvider)] = AbstractProvider;
                    props[nameof(BToolbarBody<IToolbar>.ImgContent)] = ImgContent;
                    props[nameof(BToolbarBody<IToolbar>.Src)] = Src;
                    props[nameof(BToolbarBody<IToolbar>.Height)] = ComputedHeight;
                    props[nameof(BToolbarBody<IToolbar>.IsExtended)] = IsExtended;
                    props[nameof(BToolbarBody<IToolbar>.ExtensionContent)] = ExtensionContent;
                })
                .Apply(typeof(IImage), typeof(MImage), props =>
                {
                    props[nameof(MImage.Height)] = ComputedHeight;
                    props[nameof(MImage.Src)] = Src;
                });
        }
    }
}
