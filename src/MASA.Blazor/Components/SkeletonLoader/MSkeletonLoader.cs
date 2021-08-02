using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSkeletonLoader : BSkeletonLoader, IThemeable
    {
        [Parameter]
        public bool Boilerplate { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string Transition { get; set; }

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public Dictionary<string, string> Types { get; set; } = new Dictionary<string, string>
        {
            { "actions","button@2"},
            { "article", "heading, paragraph" },
            { "avatar", "avatar" },
            { "button", "button" },
            { "card", "image, card-heading" },
            { "card-avatar", "image, list-item-avatar" },
            { "card-heading", "heading" },
            { "chip", "chip" },
            { "date-picker", "list-item, card-heading, divider, date-picker-options, date-picker-days, actions" },
            { "date-picker-options", "text, avatar@2" },
            { "date-picker-days", "avatar@28" },
            { "heading", "heading" },
            { "image", "image" },
            { "list-item", "text" },
            { "list-item-avatar", "avatar, text" },
            { "list-item-two-line", "sentences" },
            { "list-item-avatar-two-line", "avatar, sentences" },
            { "list-item-three-line", "paragraph" },
            { "list-item-avatar-three-line", "avatar, paragraph" },
            { "paragraph", "text@3" },
            { "sentences", "text@2" },
            { "table", "table-heading, table-thead, table-tbody, table-tfoot" },
            { "table-heading", "heading, text" },
            { "table-thead", "heading@6" },
            { "table-tbody", "table-row-divider@6" },
            { "table-row-divider", "table-row, divider" },
            { "table-row", "table-cell@6" },
            { "table-cell", "text" },
            { "table-tfoot", "text@2, avatar@2" },
            { "text", "text" },
            { "divider", "divider" },
        };

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-skeleton-loader";

            CssProvider
                .AsProvider<BSkeletonLoader>()
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add(prefix)
                        .Add("m-application")
                        .AddIf($"{prefix}--boilerplate", () => Boilerplate)
                        .AddIf($"{prefix}--is-loading", () => Loading)
                        .AddIf($"{prefix}--tile", () => Tile)
                        .AddTheme(IsDark)
                        .AddElevation(2);
                }, styleBuilder =>
                {
                    if (Loading)
                    {
                        styleBuilder
                            .AddHeight(Height)
                            .AddWidth(Width)
                            .AddMinWidth(MinWidth)
                            .AddMaxWidth(MaxWidth)
                            .AddMinHeight(MinHeight)
                            .AddMaxHeight(MaxHeight);
                    }
                });

            GenStructure(Type);
            ChildContent = GenBone(Type);

            if (Loading && !Boilerplate)
            {
                Attributes.Add("aria-busy", true);
                Attributes.Add("aria-live", "polite");
                Attributes.Add("role", "alert");
            }
        }

        private string children = string.Empty;

        private RenderFragment GenBone(string type) => builder =>
        {
            int sequence = 0;
            builder.OpenElement(sequence++, "div");

            builder.AddAttribute(sequence++, "style", "width:100%; Height:100%;");

            if (!string.IsNullOrWhiteSpace(children))
            {
                builder.AddMarkupContent(sequence++, children);
            }

            builder.CloseElement();
        };

        private void GenStructure(string type)
        {
            Types.TryGetValue(type, out string bone);

            if (type.Contains(","))
            {
                MapBones(type);
            }
            else if (type.Contains("@"))
            {
                GenBones(type);
            }
            else if (bone.Contains(","))
            {
                MapBones(bone);
            }
            else if (bone.Contains("@"))
            {
                GenBones(bone);
            }
            else if (type != bone)
            {
                GenStructure(bone);
            }
            else
            {
                children += $"<div class=\"m-skeleton-loader__{type} m-skeleton-loader__bone\"></div>";
            }
        }

        private void MapBones(string bones)
        {
            var types = Regex.Replace(bones, @"\s", "").Split(',').ToList();
            foreach (var type in types)
            {
                if (type.Contains("@"))
                {
                    GenBones(type);
                }
                else
                {
                    children += $"<div class=\"m-skeleton-loader__{type} m-skeleton-loader__bone\">";
                    Types.TryGetValue(type, out string bone);
                    if (type != bone)
                    {
                        GenStructure(type);
                    }
                    children += "</div>";
                }
            }
        }

        private void GenBones(string bones)
        {
            var cutList = bones.Split('@').ToList();
            var bone = cutList.FirstOrDefault();
            var frequency = cutList.LastOrDefault();
            if (!string.IsNullOrWhiteSpace(bone) && !string.IsNullOrWhiteSpace(frequency))
            {
                for (int i = 0; i < int.Parse(frequency); i++)
                {
                    GenStructure(bone);
                }
            }
        }
    }
}
