namespace Masa.Blazor
{
    public partial class MSkeletonLoader : BSkeletonLoader, ISkeletonLoader, IThemeable
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
        public Dictionary<string, string> Types { get; set; }

        public Dictionary<string, string> RootTypes { get; set; }

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
        public StringNumber Elevation { get; set; }

        private bool IsLoading => ChildContent is null || Loading;

        protected override void SetComponentClass()
        {
            var prefix = "m-skeleton-loader";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add(prefix)
                        .AddIf($"{prefix}--boilerplate", () => Boilerplate)
                        .AddIf($"{prefix}--is-loading", () => IsLoading)
                        .AddIf($"{prefix}--tile", () => Tile)
                        .AddTheme(IsDark)
                        .AddElevatable(this);
                }, styleBuilder =>
                {
                    if (IsLoading == true)
                    {
                        styleBuilder.AddMeasurable(this);
                    }
                });

            if (IsLoading && !Boilerplate)
            {
                Attributes.Add("aria-busy", true);
                Attributes.Add("aria-live", "polite");
                Attributes.Add("role", "alert");
            }

            RootTypes = new Dictionary<string, string>
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
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Types is not null)
            {
                foreach (var (key, value) in Types)
                {
                    RootTypes.Add(key, value);
                }
            }

            if (IsLoading == true)
            {
                SkeletonLoaderContent = GenSkeleton();
            }
            else
            {
                SkeletonLoaderContent = ChildContent;
            }
        }

        public RenderFragment GenSkeleton() => builder =>
        {
            if (Transition is null)
            {
                int sequence = 0;
                builder.OpenElement(sequence++, "div");
                builder.AddAttribute(sequence++, "style", "width:100%; Height:100%;");
                var childrenHtml = string.Join("", GenStructure());
                builder.AddMarkupContent(sequence++, childrenHtml);
                builder.CloseElement();
            }
        };

        public string Genbone(string text, List<string> childrens)
        {
            var divHtml = $"<div class=\"m-skeleton-loader__{text} m-skeleton-loader__bone\">";
            foreach (var child in childrens)
            {
                divHtml += child;
            }
            divHtml += "</div>";

            return divHtml;
        }

        private List<string> GenBones(string bones)
        {
            var childrens = new List<string>();
            var cutList = bones.Split('@').ToList();
            var bone = cutList.FirstOrDefault();
            var frequency = cutList.LastOrDefault();
            if (!string.IsNullOrWhiteSpace(bone) && !string.IsNullOrWhiteSpace(frequency))
            {
                for (int i = 0; i < int.Parse(frequency); i++)
                {
                    childrens.AddRange(GenStructure(bone));
                }
            }
            return childrens;
        }

        private List<string> GenStructure(string type = null)
        {
            var childrens = new List<string>();
            type = type ?? this.Type ?? "";
            RootTypes.TryGetValue(type, out string bone);

            if (type == bone)
            {

            }
            else if (type.Contains(","))
            {
                return MapBones(type);
            }
            else if (type.Contains("@"))
            {
                return GenBones(type);
            }
            else if (bone.Contains(","))
            {
                childrens = MapBones(bone);
            }
            else if (bone.Contains("@"))
            {
                childrens = GenBones(bone);
            }
            else if (bone is not null)
            {
                childrens.AddRange(GenStructure(bone));
            }

            return new List<string> { Genbone(type, childrens) };
        }

        private List<string> MapBones(string bones)
        {
            var childrens = new List<string>();
            var types = bones.Replace(" ", "").Split(",");
            foreach (var type in types)
            {
                childrens.AddRange(GenStructure(type));
            }
            return childrens;
        }
    }
}
