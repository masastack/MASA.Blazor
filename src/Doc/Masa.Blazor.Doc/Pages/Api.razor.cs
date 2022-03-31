using BlazorComponent.I18n;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Masa.Blazor.Doc.Pages
{
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
            "MBottomSheet",
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
            "MEditor",
            "MErrorHandler",
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
            "MOtpInput",
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
            "MMarkdown",
            "MMenu",
            "MMessages",
            "MNavigationDrawer",
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
        public I18nConfig I18nConfig { get; set; }

        [Parameter]
        public string ApiName { get; set; }

        [CascadingParameter(Name = "Lang")]
        public bool IsChinese { get; set; }

        [Inject]
        public DemoService Service { get; set; }

        private ApiModel _api;

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(ApiName))
            {
                return;
            }

            ApiName = StandardApiNames.FirstOrDefault(api => api.Equals(ApiName, StringComparison.OrdinalIgnoreCase));

            var lang = I18nConfig.Language ?? CultureInfo.CurrentCulture.Name;
            var apiUrl = $"_content/Masa.Blazor.Doc/docs/api/{ApiName}.{lang}.json";
            _api = await Service.GetApiAsync(apiUrl);
        }

        private (string id, string title, List<ApiColumn> apiData) FormatSection(string section)
        {
            var id = section;
            var title = section;

            if (IsChinese)
            {
                id = null;

                title = section switch
                {
                    "props" => "属性",
                    "events" => "事件",
                    "contents" => "插槽",
                    _ => throw new ArgumentOutOfRangeException(nameof(section), section, null)
                };
            }

            var apiData = section switch
            {
                "props" => _api.Props,
                "events" => _api.Events,
                "contents" => _api.Contents,
                _ => throw new ArgumentOutOfRangeException()
            };

            return (id, title, apiData);
        }
    }
}