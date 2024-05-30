﻿namespace Masa.Blazor
{
    public partial class MDataFooter : MasaComponentBase, IDataFooterParameters
    {
        [Inject]
        protected I18n I18n { get; set; } = null!;

        [Inject]
        protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter]
        public RenderFragment? PrependContent { get; set; }

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

        private int ItemsPagePageValue
        {
            get
            {
                if (ComputedDataItemsPerPageOptions.Any(u => u.Value == Options.ItemsPerPage))
                {
                    return Options.ItemsPerPage;
                }

                return ComputedDataItemsPerPageOptions.First().Value;
            }
        }
        
        private static Block _block = new("m-data-footer");

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _block.Name;
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
