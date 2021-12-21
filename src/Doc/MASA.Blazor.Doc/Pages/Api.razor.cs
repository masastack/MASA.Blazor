using BlazorComponent;
using MASA.Blazor.Doc.Utils;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace MASA.Blazor.Doc.Pages
{
    public class ApiModel
    {
        public string Title { get; set; }
        public List<string> Components { get; set; }
        public List<Props> Props { get; set; }
        public List<Props1> Contents { get; set; }
        public List<Props1> Events { get; set; }
    }

    public class Props
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
        public string Description { get; set; }
    }

    public class Props1
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public partial class Api
    {
        private readonly static string[] StandardApiNames =
        {
            "MAlert",
            "MAlertDismissButton",
            "MAlertIcon",
            "MApp",
            "MAppBar",
            "MAppBarNavIcon",
            "MAppBarTitle",
            "MAutocomplete",
            "MAvatar",
            "MBadge",
            "MBanner",
            "MBorder",
            "MBreadcrumbs",
            "MBreadcrumbsDivider",
            "MBreadcrumbsItem",
            "MButton",
            "MButtonGroup",
            "MCalendar",
            "MCalendarCategory",
            "MCalendarDaily",
            "MCalendarMonthly",
            "MCalendarWeekly",
            "MCard",
            "MCardActions",
            "MCardSubtitle",
            "MCardText",
            "MCardTitle",
            "MCascader",
            "MCascaderMenu",
            "MCascaderSelectList",
            "MCascaderSelectOption",
            "MCheckbox",
            "MChip",
            "MChipGroup",
            "MCol",
            "MColorPicker",
            "MColorPickerCanvas",
            "MColorPickerEdit",
            "MColorPickerPreview",
            "MContainer",
            "MCounter",
            "MDataFooter",
            "MDataIterator",
            "MDataTable",
            "MDataTableHeader",
            "MDataTableRow",
            "MDataTableRowGroup",
            "MDatePicker",
            "MDatePickerDateTable",
            "MDatePickerHeader",
            "MDatePickerMonthTable",
            "MDatePickerTable",
            "MDatePickerTitle",
            "MDatePickerYears",
            "MDialog",
            "MDivider",
            "MECharts",
            "MExpansionPanel",
            "MExpansionPanelContent",
            "MExpansionPanelHeader",
            "MExpansionPanels",
            "MFileInput",
            "MFooter",
            "MForm",
            "MHintMessage",
            "MHover",
            "MIcon",
            "MImage",
            "MInput",
            "MItem",
            "MItemGroup",
            "MLabel",
            "MList",
            "MListGroup",
            "MListGroupItem",
            "MListGroupItemIcon",
            "MListItem",
            "MListItemAction",
            "MListItemActionText",
            "MListItemAvatar",
            "MListItemContent",
            "MListItemGroup",
            "MListItemIcon",
            "MListItemSubtitle",
            "MListItemTitle",
            "MMain",
            "MMenu",
            "MMessages",
            "MNavigationDrawer",
            "Modal",
            "MOverlay",
            "MPagination",
            "MPicker",
            "MPopover",
            "MProgressCircular",
            "MProgressLinear",
            "MRadio",
            "MRadioGroup",
            "MRangeSlider",
            "MRating",
            "MResponsive",
            "MRow",
            "MSelect",
            "MSelectList",
            "MSelectOption",
            "MSheet",
            "MSimpleCheckbox",
            "MSimpleTable",
            "MSkeletonLoader",
            "MSlideGroup",
            "MSlideItem",
            "MSlider",
            "MSnackbar",
            "MSpacer",
            "MStepper",
            "MStepperContent",
            "MStepperHeader",
            "MStepperItems",
            "MStepperStep",
            "MSubheader",
            "MSwitch",
            "MSystemBar",
            "MTab",
            "MTabItem",
            "MTable",
            "MTableCol",
            "MTableFooter",
            "MTableFooterButton",
            "MTableHeader",
            "MTableLoading",
            "MTabs",
            "MTabsBar",
            "MTabsItems",
            "MTabsSlider",
            "MTextarea",
            "MTextField",
            "MTimeline",
            "MTimelineItem",
            "MTimePicker",
            "MTimePickerClock",
            "MTimePickerTitle",
            "MToolbar",
            "MToolbarItems",
            "MToolbarTitle",
            "MTooltip",
            "MTree",
            "MTreeItem",
            "MTreeview",
            "MTreeviewNode",
            "MUpload",
            "MVirtualScroll",
            "MWindow",
            "MWindowItem"
        };

        [Inject]
        public GlobalConfigs GlobalConfig { get; set; }

        [Parameter]
        public string ApiName { get; set; }

        [CascadingParameter]
        public bool IsChinese { get; set; }

        private List<DataTableHeader<Props>> _headers = new List<DataTableHeader<Props>>
        {
            new DataTableHeader<Props>
            {
                Value = nameof(Props.Name),
                Text = "Name",
                Width = "20%",
                Sortable = false
            },
            new DataTableHeader<Props>
            {
                Value = nameof(Props.Type),
                Text = "Type",
                Width = "15%",
                Sortable = false
            },
            new DataTableHeader<Props>
            {
                Value = nameof(Props.Default),
                Text = "Default",
                Width = "12%",
                Sortable = false
            },
            new DataTableHeader<Props>
            {
                Value = nameof(Props.Description),
                Text = "Description",
                Sortable = false
            }
        };

        private List<DataTableHeader<Props1>> _headers2 = new List<DataTableHeader<Props1>>
        {
            new DataTableHeader<Props1>
            {
                Value = nameof(Props1.Name),
                Text = "Name",
                Width = "20%",
                Sortable = false
            },
            new DataTableHeader<Props1>
            {
                Value = nameof(Props1.Description),
                Text = "Description",
                Sortable = false
            }
        };

        private ApiModel _api;

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(ApiName))
            {
                return;
            }

            ApiName = StandardApiNames.FirstOrDefault(api => api.Equals(ApiName, StringComparison.OrdinalIgnoreCase));

            var lang = GlobalConfig.Language ?? CultureInfo.CurrentCulture.Name;
            var baseUrl = new Uri("http://127.0.0.1:5000");
            var apiUrl = new Uri(baseUrl, $"_content/MASA.Blazor.Doc/docs/api/{ApiName}.{lang}.json").ToString();
            _api = await Service.GetApiAsync(apiUrl);
        }
    }
}