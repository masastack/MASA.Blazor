using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public partial class MPagination : BPagination, IPagination
    {
        [Parameter]
        public bool Circle { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<int> OnInput { get; set; }

        [Parameter]
        public EventCallback OnPrevious { get; set; }

        [Parameter]
        public EventCallback OnNext { get; set; }

        [Parameter]
        public int Length { get; set; }

        [Parameter]
        public string NextIcon { get; set; } = "mdi-chevron-right";

        [Parameter]
        public string PrevIcon { get; set; } = "mdi-chevron-left";

        [Parameter]
        public StringNumber TotalVisible { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        [Inject]
        public Document Document { get; set; }

        public bool PrevDisabled => Value <= 1;

        public bool NextDisabled => Value >= Length;

        protected int _maxButtons = 0;

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply("list", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-pagination")
                        .AddIf("m-pagination--circle", () => Circle)
                        .AddIf("m-pagination--disabled", () => Disabled)
                        .AddTheme(IsDark);
                })
                .Apply("navigation", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-pagination__navigation");
                })
                .Apply("navigation-disabled", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-pagination__navigation")
                        .Add("m-pagination__navigation--disabled");
                })
                .Apply("more", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-pagination__more");
                })
                .Apply("current-item", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-pagination__item")
                        .Add("m-pagination__item--active")
                        .AddBackgroundColor(Color);
                })
                .Apply("item", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-pagination__item");
                });

            AbstractProvider
                .ApplyPaginationDefault()
                .Apply<BIcon, MIcon>();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var el = Document.GetElementByReference(Ref);
                var clientWidth = await el.ParentElement.GetClientWidthAsync();
                if (clientWidth != null)
                {
                    _maxButtons = Convert.ToInt32(Math.Floor((clientWidth.Value - 96.0) / 42.0));
                    StateHasChanged();
                }
            }
        }

        public string GetIcon(int index)
        {
            return index == (int)PaginationIconTypes.First ?
                (MasaBlazor.RTL ? NextIcon : PrevIcon) :
                (MasaBlazor.RTL ? PrevIcon : NextIcon);
        }

        public IEnumerable<StringNumber> GetItems()
        {
            if (TotalVisible != null && TotalVisible.ToInt32() == 0)
            {
                return Enumerable.Empty<StringNumber>();
            }

            int Min(int v1, int v2, int v3)
            {
                var min = Math.Min(v1, v2);
                return Math.Min(min, v3);
            }

            int Max(StringNumber v1, StringNumber v2, int v)
            {
                int max;

                if (v1 == null || v2 == null)
                {
                    max = 0;
                }
                else
                {
                    max = Math.Max(v1.ToInt32(), v2.ToInt32());
                }

                //Use v to ensure max always greater than 0
                return max == 0 ? v : max;
            }

            var maxLength = Min(
                Max(0, TotalVisible, Length),
                Max(0, _maxButtons, Length),
                Length);

            if (Length <= maxLength)
            {
                return Range(1, Length);
            }

            var items = new List<StringNumber>();
            var even = maxLength % 2 == 0 ? 1 : 0;
            var left = Convert.ToInt32(Math.Floor(maxLength / 2M));
            var right = Length - left + 1 + even;

            if (Value > left && Value < right)
            {
                var start = Value - left + 2;
                var end = Value + left - 2 - even;

                items.Add(1);
                items.Add("...");
                items.AddRange(Range(start, end));
                items.Add("...");
                items.Add(Length);

                return items;
            }
            else if (Value == left)
            {
                var end = Value + left - 1 - even;

                items.AddRange(Range(1, end));
                items.Add("...");
                items.Add(Length);

                return items;
            }
            else if (Value == right)
            {
                var start = Value - left + 1;
                items.Add(1);
                items.Add("...");
                items.AddRange(Range(start, Length));

                return items;
            }
            else
            {
                items.AddRange(Range(1, left));
                items.Add("...");
                items.AddRange(Range(right, Length));

                return items;
            }
        }

        protected static IEnumerable<StringNumber> Range(int start, int end)
        {
            return Enumerable.Range(start, end - start + 1).Select(r => (StringNumber)r);
        }

        public virtual async Task HandlePreviousAsync(MouseEventArgs args)
        {
            if (PrevDisabled)
            {
                return;
            }

            Value--;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(Value);
            }

            if (OnPrevious.HasDelegate)
            {
                await OnPrevious.InvokeAsync();
            }
        }

        public virtual async Task HandleNextAsync(MouseEventArgs args)
        {
            if (NextDisabled)
            {
                return;
            }

            Value++;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(Value);
            }

            if (OnNext.HasDelegate)
            {
                await OnNext.InvokeAsync();
            }
        }

        public virtual async Task HandleItemClickAsync(StringNumber item)
        {
            Value = item.AsT1;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(item.AsT1);
            }
        }
    }
}
