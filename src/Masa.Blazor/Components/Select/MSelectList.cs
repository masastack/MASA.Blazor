using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public partial class MSelectList<TItem, TItemValue, TValue> : BSelectList<TItem, TItemValue, TValue>, ISelectList<TItem, TItemValue, TValue>
    {
        [Parameter]
        public bool Action { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool NoFilter { get; set; }

        [Parameter]
        public string SearchInput { get; set; }

        [Parameter]
        public EventCallback<TItem> OnSelect { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public RenderFragment<SelectListItemProps<TItem>> ItemContent { get; set; }

        [Parameter]
        public int SelectedIndex { get; set; }

        protected string TileActiveClass => new CssBuilder().AddTextColor(Color).Class;

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            AbstractProvider
                .ApplySelectListDefault<TItem, TItemValue, TValue>()
                .Apply(typeof(BList), typeof(MList), attrs =>
                {
                    attrs[nameof(Class)] = "m-select-list";
                    //TODO: themeClasses
                    attrs["role"] = "listbox";
                    attrs["tabindex"] = -1;
                    attrs[nameof(MList.Dense)] = Dense;
                })
                .Apply<BListItem, MListItem>(attrs =>
                {
                    if (attrs.Data is (TItem item, bool value))
                    {
                        attrs["aria-selected"] = value.ToString();
                        //TODO: id
                        attrs["role"] = "option";
                        attrs[nameof(MListItem.OnClick)] = CreateEventCallback<MouseEventArgs>(async _ =>
                        {
                            if (!GetDisabled(item) && OnSelect.HasDelegate)
                            {
                                await OnSelect.InvokeAsync(item);
                            }
                        });
                        attrs[nameof(MListItem.Value)] = (StringNumber)value.ToString();
                        attrs[nameof(MListItem.ActiveClass)] = TileActiveClass;
                        attrs[nameof(MListItem.Disabled)] = GetDisabled(item);
                        attrs[nameof(MListItem.Ripple)] = true;
                        attrs[nameof(MListItem.IsActive)] = value;//TODO: remove this when MListItem been refactored
                        attrs[nameof(MListItem.Highlighted)] = Items.IndexOf(item) == SelectedIndex;//TODO: remove this when MListItem been refactored
                    }
                })
                .Apply<BSimpleCheckbox, MSimpleCheckbox>(attrs =>
                {
                    attrs[nameof(MSimpleCheckbox.Color)] = Color;
                    attrs[nameof(MSimpleCheckbox.Ripple)] = false;
                    if (attrs.Data is TItem item)
                    {
                        attrs[nameof(MSimpleCheckbox.ValueChanged)] = CreateEventCallback<bool>(async _ =>
                        {
                            if (OnSelect.HasDelegate)
                            {
                                await OnSelect.InvokeAsync(item);
                            }
                        });
                    }
                })
                .Apply<BListItem, MListItem>("no-data", attrs =>
                 {
                     attrs["role"] = "undefined";
                 });
        }

        bool ISelectList<TItem, TItemValue, TValue>.HasItem(TItem item) => HasItem(item);

        protected bool GetDisabled(TItem item)
        {
            return ItemDisabled != null && ItemDisabled(item);
        }

        public string GetFilteredText(TItem item)
        {
            var text = ItemText(item);

            if (SearchInput == null || NoFilter)
            {
                return text;
            }

            var (start, middle, end) = GetMaskedCharacters(text);
            return $"{start}{GetHighlight(middle)}{end}";
        }

        protected static string GetHighlight(string middle)
        {
            return $"<span class=\"m-list-item__mask\">{middle}</span>";
        }

        protected (string, string, string) GetMaskedCharacters(string text)
        {
            var searchInput = (SearchInput ?? "").ToLowerInvariant();
            var index = text.ToLowerInvariant().IndexOf(searchInput);

            if (index == -1)
            {
                return (text, "", "");
            }

            var start = text[..index];
            var middle = text.Substring(index, searchInput.Length);
            var end = text[(index + searchInput.Length)..];

            return (start, middle, end);
        }
    }
}