﻿namespace Masa.Blazor
{
    public class MDataFooter : BDataFooter, IDataFooter
    {
        [Inject]
        protected I18n I18n { get; set; } = null!;

        [Inject]
        protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter, MasaApiParameter("$masaBlazor.dataFooter.itemsPerPageText")]
        public string? ItemsPerPageText { get; set; }

        [Parameter, EditorRequired]
        public DataOptions Options { get; set; } = null!;

        [Parameter, EditorRequired]
        public DataPagination Pagination { get; set; } = null!;

        [Parameter]
        public EventCallback<Action<DataOptions>> OnOptionsUpdate { get; set; }

        [Parameter]
        [MasaApiParameter("new List<OneOf<int, DataItemsPerPageOption>>(){5, 10, 15, -1}")]
        public IEnumerable<OneOf<int, DataItemsPerPageOption>>? ItemsPerPageOptions { get; set; } = new List<OneOf<int, DataItemsPerPageOption>>()
        {
            5,
            10,
            15,
            -1
        };

        [Parameter, MasaApiParameter("$prev")]
        public string? PrevIcon { get; set; } = "$prev";

        [Parameter, MasaApiParameter("$next")]
        public string? NextIcon { get; set; } = "$next";

        [Parameter, MasaApiParameter("$last")]
        public string? LastIcon { get; set; } = "$last";

        [Parameter, MasaApiParameter("$first")]
        public string? FirstIcon { get; set; } = "$first";

        [Parameter, MasaApiParameter("$masaBlazor.dataFooter.itemsPerPageAll")]
        public string? ItemsPerPageAllText { get; set; }

        [Parameter]
        public bool ShowFirstLastPage { get; set; }

        [Parameter]
        public bool ShowCurrentPage { get; set; }

        [Parameter]
        public bool DisablePagination { get; set; }

        [Parameter]
        public bool DisableItemsPerPage { get; set; }

        [Parameter, MasaApiParameter("$masaBlazor.dataFooter.pageText")]
        public string? PageText { get; set; }

        [Parameter]
        public RenderFragment<(int PageStart, int PageStop, int ItemsLength)>? PageTextContent { get; set; }

        [Parameter]
        public Action<IDataFooterParameters>? Parameters { get; set; }

        public IEnumerable<DataItemsPerPageOption> ComputedDataItemsPerPageOptions
        {
            get
            {
                if (ItemsPerPageOptions == null)
                {
                    return Enumerable.Empty<DataItemsPerPageOption>();
                }

                return ItemsPerPageOptions
                    .Select(r => r.IsT1
                        ? r.AsT1
                        : new DataItemsPerPageOption
                        {
                            Text = r.AsT0 == -1 ? ItemsPerPageAllText : r.AsT0.ToString(),
                            Value = r.AsT0
                        });
            }
        }

        public bool RTL => MasaBlazor.RTL;

        public bool DisableNextPageIcon => Options?.ItemsPerPage <= 0 || Options?.Page * Options?.ItemsPerPage >= Pagination?.ItemsLength ||
                                           Pagination?.PageStop < 0;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            ItemsPerPageText = I18n.T("$masaBlazor.dataFooter.itemsPerPageText");
            ItemsPerPageAllText = I18n.T("$masaBlazor.dataFooter.itemsPerPageAll");
            PageText = I18n.T("$masaBlazor.dataFooter.pageText");

            return base.SetParametersAsync(parameters);
        }

        protected override void OnParametersSet()
        {
            Parameters?.Invoke(this);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                MasaBlazor.OnRTLChange += OnRTLChange;
            }
        }

        private void OnRTLChange(bool obj)
        {
            InvokeStateHasChanged();
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-footer");
                })
                .Apply("select", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-footer__select");
                })
                .Apply("pagination", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-footer__pagination");
                })
                .Apply("icons-before", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-footer__icons-before");
                })
                .Apply("icons-after", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-footer__icons-after");
                });

            AbstractProvider
                .ApplyDataFooterDefault()
                .Apply(typeof(ISelect<,,>), typeof(MSelect<DataItemsPerPageOption, int, int>), attrs =>
                {
                    if (ComputedDataItemsPerPageOptions.Count() <= 1)
                    {
                        return;
                    }

                    var value = Options?.ItemsPerPage;
                    var result = ComputedDataItemsPerPageOptions.FirstOrDefault(r => r.Value == value);
                    if (result == null)
                    {
                        value = ComputedDataItemsPerPageOptions.First().Value;
                    }

                    Func<DataItemsPerPageOption, int> itemValue = r => r.Value;
                    Func<DataItemsPerPageOption, string?> itemText = r => r.Text;

                    attrs[nameof(MSelect<DataItemsPerPageOption, int, int>.Disabled)] = DisableItemsPerPage;
                    attrs[nameof(MSelect<DataItemsPerPageOption, int, int>.Items)] = ComputedDataItemsPerPageOptions.ToList();
                    attrs[nameof(MSelect<DataItemsPerPageOption, int, int>.Value)] = value;
                    attrs[nameof(MSelect<DataItemsPerPageOption, int, int>.ItemValue)] = itemValue;
                    attrs[nameof(MSelect<DataItemsPerPageOption, int, int>.ItemText)] = itemText;
                    attrs[nameof(MSelect<DataItemsPerPageOption, int, int>.HideDetails)] = (StringBoolean)true;
                    //TODO:auto
                    attrs[nameof(MSelect<DataItemsPerPageOption, int, int>.MinWidth)] = (StringNumber)"75px";
                    attrs[nameof(MSelect<DataItemsPerPageOption, int, int>.ValueChanged)] =
                        EventCallback.Factory.Create<int>(this, HandleOnChangeItemsPerPageAsync);
                })
                .Apply<BButton, MButton>()
                .Apply<BIcon, MIcon>();
        }

        public async Task HandleOnFirstPageAsync()
        {
            if (Options == null) return;

            Options.Page = 1;

            if (OnOptionsUpdate.HasDelegate)
            {
                await OnOptionsUpdate.InvokeAsync(options => options.Page = Options.Page);
            }
        }

        public async Task HandleOnLastPageAsync()
        {
            if (Options == null || Pagination == null) return;

            Options.Page = Pagination.PageCount;

            if (OnOptionsUpdate.HasDelegate)
            {
                await OnOptionsUpdate.InvokeAsync(options => options.Page = Options.Page);
            }
        }

        public async Task HandleOnNextPageAsync()
        {
            if (Options == null) return;

            Options.Page += 1;

            if (OnOptionsUpdate.HasDelegate)
            {
                await OnOptionsUpdate.InvokeAsync(options => options.Page = Options.Page);
            }
        }

        public async Task HandleOnPreviousPageAsync()
        {
            if (Options == null) return;

            Options.Page -= 1;

            if (OnOptionsUpdate.HasDelegate)
            {
                await OnOptionsUpdate.InvokeAsync(options => options.Page = Options.Page);
            }
        }

        private async Task HandleOnChangeItemsPerPageAsync(int itemsPerPage)
        {
            if (Options == null) return;

            Options.ItemsPerPage = itemsPerPage;
            Options.Page = 1;

            if (OnOptionsUpdate.HasDelegate)
            {
                await OnOptionsUpdate.InvokeAsync(options =>
                {
                    options.Page = Options.Page;
                    options.ItemsPerPage = Options.ItemsPerPage;
                });
            }
        }
    }
}
