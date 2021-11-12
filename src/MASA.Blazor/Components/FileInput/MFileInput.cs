using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MFileInput<TValue> : MTextField<TValue>, IFileInput<TValue>
    {
        [Parameter]
        public bool HideInput { get; set; }

        [Parameter]
        public bool Chips { get; set; }

        [Parameter]
        public bool SmallChips { get; set; }

        [Parameter]
        public RenderFragment<(int index, string text)> SelectionContent { get; set; }

        [Inject]
        public Document Document { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public bool ShowSize { get; set; }

        [Parameter]
        public override string PrependIcon { get; set; } = "mdi-paperclip";

        [Parameter]
        public override bool Clearable { get; set; } = true;

        [Parameter]
        public StringNumber TruncateLength { get; set; } = 22;

        [Parameter]
        public string Accept { get; set; }

        public override Dictionary<string, object> InputAttrs => new()
        {
            { "type", "file" },
            { "accept", Accept }
        };

        public bool HasChips => Chips || SmallChips;

        public IList<string> Text
        {
            get
            {
                if (!IsDirty && (IsFocused || !HasLabel))
                {
                    return new List<string>
                    {
                        Placeholder
                    };
                }

                var text = new List<string>();

                foreach (var file in Files)
                {
                    var truncatedText = TruncateText(file.Name);
                    text.Add(!ShowSize ? truncatedText : $"{truncatedText} ({HumanReadableFileSize(file.Size)})");
                }

                return text;
            }
        }

        public InputFile InputFile { get; set; }

        public override ElementReference InputElement
        {
            get => InputFile.Element.Value;
            set => base.InputElement = value;
        }

        public IList<IBrowserFile> Files
        {
            get
            {
                if (Value is IBrowserFile file)
                {
                    return new List<IBrowserFile>
                    {
                        file
                    };
                }
                else if (Value is IList<IBrowserFile> files)
                {
                    return files;
                }

                return new List<IBrowserFile>();
            }
        }

        public override StringNumber ComputedCounterValue
        {
            get
            {
                if (!ShowSize)
                {
                    return $"{Files.Count} files";
                }

                var bytes = Files.Sum(r => r.Size);
                return $"{Files.Count} files({HumanReadableFileSize(bytes)} in total) ";
            }
        }

        public override bool IsDirty => Files.Count > 0;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (typeof(TValue) != typeof(IBrowserFile) && typeof(TValue) != typeof(List<IBrowserFile>))
            {
                throw new ArgumentException("TValue should be IBrowserFile or List<IBrowserFile>");
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Multiple && typeof(TValue) != typeof(List<IBrowserFile>))
            {
                throw new ArgumentException("Multiple TValue should be List<IBrowserFile>");
            }
        }

        public string HumanReadableFileSize(long bytes, bool binary = false)
        {
            var @base = binary ? 1024M : 1000M;
            if (bytes < @base)
            {
                return $"{bytes} B";
            }

            var prefix = binary ? new string[] {"Ki", "Mi", "Gi"} : new string[] {"k", "M", "G"};
            var unit = -1;
            var size = Convert.ToDecimal(bytes);

            while (Math.Abs(size) > @base && unit < prefix.Length - 1)
            {
                size /= @base;
                ++unit;
            }

            return $"{Math.Round(size, 1)} {prefix[unit]}B";
        }

        public string TruncateText(string name)
        {
            if (name.Length < TruncateLength.ToInt32())
            {
                return name;
            }

            var charsKeepOneSide = Convert.ToInt32(Math.Floor((TruncateLength.ToInt32() - 1) / 2.0));
            return string.Concat(name.AsSpan(0, charsKeepOneSide), "…", name.AsSpan(name.Length - charsKeepOneSide));
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-file-input");
                })
                .Merge("control", mergeStyleAction: styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:none", () => HideInput);
                })
                .Apply("file-input-text", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-file-input__text")
                        .AddIf("m-file-input__text--placeholder", () => Placeholder != null && !IsDirty)
                        .AddIf("m-file-input__text--chips", () => HasChips && SelectionContent != null);
                });

            AbstractProvider
                .Apply(typeof(BChip), typeof(MChip), props => { props[nameof(MChip.Small)] = SmallChips; })
                .Merge(typeof(BTextFieldInput<,>), typeof(BFileInputInput<TValue, MFileInput<TValue>>))
                .Apply(typeof(BFileInputSelections<,>), typeof(BFileInputSelections<TValue, MFileInput<TValue>>))
                .Apply(typeof(BFileInputChips<,>), typeof(BFileInputChips<TValue, MFileInput<TValue>>))
                .Apply(typeof(BFileInputSelectionText<,>), typeof(BFileInputSelectionText<TValue, MFileInput<TValue>>));
        }

        public override async Task HandleOnPrependClickAsync(MouseEventArgs args)
        {
            await base.HandleOnPrependClickAsync(args);
            var input = Document.QuerySelector(InputFile.Element.Value);
            var @event = new MouseEvent("click");
            @event.StopPropagation();
            await input.DispatchEventAsync(@event);
        }

        public async Task HandleOnFileChangeAsync(InputFileChangeEventArgs args)
        {
            if (Multiple)
            {
                IList<IBrowserFile> files = new List<IBrowserFile>();

                foreach (var file in args.GetMultipleFiles())
                {
                    files.Add(file);
                }

                Value = (TValue)files;
            }
            else
            {
                if (args.FileCount > 0)
                {
                    Value = (TValue)args.File;
                }
                else
                {
                    Value = default;
                }
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
        }

        public override async Task HandleOnClickAsync(MouseEventArgs args)
        {
            await base.HandleOnClickAsync(args);

            var input = Document.QuerySelector(InputFile.Element.Value);
            var @event = new MouseEvent("click");
            @event.StopPropagation();
            await input.DispatchEventAsync(@event);
        }

        public override async Task HandleOnClearClickAsync(MouseEventArgs args)
        {
            var input = Document.QuerySelector(InputFile.Element.Value);
            await input.SetPropertyAsync("value", "");

            await base.HandleOnClearClickAsync(args);
        }
    }
}