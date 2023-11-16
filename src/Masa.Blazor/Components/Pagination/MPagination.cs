using BlazorComponent.Web;

namespace Masa.Blazor
{
    public partial class MPagination : BPagination, IPagination, IAsyncDisposable
    {
        [Inject]
        public MasaBlazor MasaBlazor { get; set; } = null!;

        [Inject]
        public Document Document { get; set; } = null!;

        [Inject]
        private IntersectJSModule IntersectJSModule { get; set; } = null!;

        [Parameter]
        public bool Circle { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// The format of the link href. It's useful for SEO.
        /// </summary>
        /// <example>
        /// ”/page/{0}“
        /// </example>
        [Parameter]
        public string? HrefFormat { get; set; }

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
        public string NextIcon { get; set; } = "$next";

        [Parameter]
        public string PrevIcon { get; set; } = "$prev";

        [Parameter]
        public StringNumber? TotalVisible { get; set; }

        [Parameter]
        public string? Color { get; set; } = "primary";

        public bool PrevDisabled => Value <= 1;

        public bool NextDisabled => Value >= Length;

        protected int MaxButtons;

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
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var el = Document.GetElementByReference(Ref);
                if (el is null) return;

                var clientWidth = await el.ParentElement.GetClientWidthAsync();
                if (clientWidth is > 0)
                {
                    CalcMaxButtons(clientWidth.Value);
                }
                else
                {
                    // clientWidth may be 0 when place in dialog
                    // so we need to observe the element
                    await IntersectJSModule.ObserverAsync(Ref, async e =>
                    {
                        if (e.IsIntersecting)
                        {
                            await InvokeAsync(async () =>
                            {
                                clientWidth = await el.ParentElement.GetClientWidthAsync();
                                if (clientWidth is > 0)
                                {
                                    CalcMaxButtons(clientWidth.Value);
                                }
                            });
                        }
                    });
                }
            }

            void CalcMaxButtons(double width)
            {
                MaxButtons = Convert.ToInt32(Math.Floor((width - 96.0) / 42.0));
                StateHasChanged();
            }
        }

        public string GetIcon(int index)
        {
            return index == (int)PaginationIconTypes.First ? (MasaBlazor.RTL ? NextIcon : PrevIcon) : (MasaBlazor.RTL ? PrevIcon : NextIcon);
        }

        private int ComputedTotalVisible
        {
            get
            {
                if (TotalVisible is null)
                {
                    return MaxButtons;
                }

                return TotalVisible.ToInt32();
            }
        }

        private IEnumerable<StringNumber> CreateRange(int length, int start = 0)
        {
            return Enumerable.Range(0, length).Select(i => (StringNumber)(start + i));
        }

        public IEnumerable<StringNumber> GetItems()
        {
            List<StringNumber> items = new();

            var start = 1;

            if (Length <= 0)
            {
                return items;
            }

            if (ComputedTotalVisible <= 1)
            {
                items.Add(Value);
                return items;
            }

            if (Length <= ComputedTotalVisible)
            {
                items.AddRange(CreateRange(Length, start));
                return items;
            }

            var even = ComputedTotalVisible % 2 == 0;
            var middle = even ? ComputedTotalVisible / 2d : Math.Floor(ComputedTotalVisible / 2d);
            var left = even ? middle : middle + 1;
            var right = Length - middle;

            if (left - Value >= 0)
            {
                items.AddRange(CreateRange(Math.Max(1, ComputedTotalVisible - 1), start));
                items.Add("...");
                items.Add(Length);
            }
            else if (Value - right >= (even ? 1 : 0))
            {
                var rangeLength = ComputedTotalVisible - 1;
                var rangeStart = Length - rangeLength + start;
                items.Add(start);
                items.Add("...");
                items.AddRange(CreateRange(rangeLength, rangeStart));
            }
            else
            {
                var rangeLength = Math.Max(1, ComputedTotalVisible - 3);
                var rangeStart = rangeLength == 1 ? Value : Value - Convert.ToInt32(Math.Ceiling(rangeLength / 2d)) + start;
                items.Add(start);
                items.Add("...");
                items.AddRange(CreateRange(rangeLength, rangeStart));
                items.Add("...");
                items.Add(Length);
            }

            return items;
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

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                await IntersectJSModule.UnobserveAsync(Ref);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
