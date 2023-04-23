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
                }, styleBuilder =>
                {
                    styleBuilder
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
            var maxLength = Math.Clamp(Length, 0, Math.Min(_maxButtons, TotalVisible?.ToInt32() ?? 0));
		    var odd = maxLength & 1;
		    var halfRange = maxLength >> 1;
		    if (Value <= halfRange)
		    {
		    	return Range(1, maxLength);
		    }
		    else if (Value > Length - halfRange)
		    {
		    	return Range(Length - maxLength + 1, Length);
		    }
		    else
		    {
		    	return Range(Value - halfRange + 1, Value + halfRange + odd);
			//return Range(Value - halfRange + 1 - odd, Value + halfRange);此返回在偶数时，显示更多可用的右侧按钮。
		    }
        }

        protected static IEnumerable<StringNumber> Range(int from, int to)
        {
            from = from > 0 ? from : 1;
            return Enumerable.Range(from,  to - from + 1).Select(r => (StringNumber)r);
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
