using Microsoft.AspNetCore.Components.Web;
using ExampleComponents = Masa.Docs.Shared.Examples.components;

namespace Masa.Docs.Shared;

public static class CircuitRootComponentOptionsExtensions
{
    public static void RegisterCustomElementsOfMasaDocs(this IJSComponentConfiguration options)
    {
        options.RegisterCustomElement<Masa.Docs.Shared.Components.Example>("masa-example");
        options.RegisterCustomElement<Masa.Docs.Shared.Components.AppHeading>("app-heading");
        options.RegisterCustomElement<Masa.Docs.Shared.Components.AppLink>("app-link");
        options.RegisterCustomElement<Masa.Docs.Shared.Components.AppAlert>("app-alerts");

        options.RegisterCustomElement<Masa.Docs.Shared.Examples.about.meet_the_team.TeamMembers>("team-members");
        options.RegisterCustomElement<Masa.Docs.Shared.Examples.features.breakpoints.BreakpointTable>("breakpoint-table");
        options.RegisterCustomElement<Masa.Docs.Shared.Examples.getting_started.browser_support.BrowserSupportTable>("browser-support-table");

        options.RegisterCustomElement<ExampleComponents.alerts.Usage>("alerts-usage");
        options.RegisterCustomElement<ExampleComponents.avatars.Usage>("avatars-usage");
        options.RegisterCustomElement<ExampleComponents.badges.Usage>("badges-usage");
        options.RegisterCustomElement<ExampleComponents.banners.Usage>("banners-usage");
        options.RegisterCustomElement<ExampleComponents.app_bars.Usage>("app-bars-usage");
        options.RegisterCustomElement<ExampleComponents.toolbars.Usage>("toolbars-usage");
        options.RegisterCustomElement<ExampleComponents.system_bars.Usage>("system-bars-usage");
        options.RegisterCustomElement<ExampleComponents.bottom_navigation.Usage>("bottom-navigation-usage");
        options.RegisterCustomElement<ExampleComponents.bottom_sheets.Usage>("bottom-sheets-usage");
        options.RegisterCustomElement<ExampleComponents.breadcrumbs.Usage>("breadcrumbs-usage");
        options.RegisterCustomElement<ExampleComponents.buttons.Usage>("buttons-usage");
        options.RegisterCustomElement<ExampleComponents.cards.Usage>("cards-usage");
        options.RegisterCustomElement<ExampleComponents.chips.Usage>("chips-usage");
        options.RegisterCustomElement<ExampleComponents.borders.Usage>("borders-usage");
        options.RegisterCustomElement<ExampleComponents.dialogs.Usage>("dialogs-usage");
        options.RegisterCustomElement<ExampleComponents.dividers.Usage>("dividers-usage");
        options.RegisterCustomElement<ExampleComponents.drag_zone.Usage>("drag-zone-usage");
        options.RegisterCustomElement<ExampleComponents.echarts.Usage>("echarts-usage");
        options.RegisterCustomElement<ExampleComponents.editor.Usage>("editor-usage");
        options.RegisterCustomElement<ExampleComponents.error_handler.Usage>("error-handler-usage");
        options.RegisterCustomElement<ExampleComponents.expansion_panels.Usage>("expansion-panels-usage");
        options.RegisterCustomElement<ExampleComponents.floating_action_buttons.Usage>("floating-action-buttons-usage");
        options.RegisterCustomElement<ExampleComponents.footers.Usage>("footers-usage");
        options.RegisterCustomElement<ExampleComponents.autocomplete.Usage>("autocomplete-usage");
        options.RegisterCustomElement<ExampleComponents.cascaders.Usage>("cascaders-usage");
        options.RegisterCustomElement<ExampleComponents.checkboxes.Usage>("checkboxes-usage");
        options.RegisterCustomElement<ExampleComponents.file_inputs.Usage>("file-inputs-usage");
        options.RegisterCustomElement<ExampleComponents.forms.Usage>("forms-usage");
        options.RegisterCustomElement<ExampleComponents.otp_input.Usage>("otp-input-usage");
        options.RegisterCustomElement<ExampleComponents.radio.Usage>("radio-usage");
        options.RegisterCustomElement<ExampleComponents.range_sliders.Usage>("range-sliders-usage");
        options.RegisterCustomElement<ExampleComponents.selects.Usage>("selects-usage");
        options.RegisterCustomElement<ExampleComponents.sliders.Usage>("sliders-usage");
        options.RegisterCustomElement<ExampleComponents.switches.Usage>("switches-usage");
        options.RegisterCustomElement<ExampleComponents.textareas.Usage>("textareas-usage");
        options.RegisterCustomElement<ExampleComponents.text_fields.Usage>("text-fields-usage");
        options.RegisterCustomElement<ExampleComponents.button_groups.Usage>("button-groups-usage");
        options.RegisterCustomElement<ExampleComponents.chip_groups.Usage>("chip-groups-usage");
        options.RegisterCustomElement<ExampleComponents.item_groups.Usage>("item-groups-usage");
        options.RegisterCustomElement<ExampleComponents.list_item_groups.Usage>("list-item-groups-usage");
        options.RegisterCustomElement<ExampleComponents.slide_groups.Usage>("slide-groups-usage");
        options.RegisterCustomElement<ExampleComponents.hover.Usage>("hover-usage");
        options.RegisterCustomElement<ExampleComponents.icons.Usage>("icons-usage");
        options.RegisterCustomElement<ExampleComponents.image_captcha.Usage>("image-captcha-usage");
        options.RegisterCustomElement<ExampleComponents.images.Usage>("images-usage");
        options.RegisterCustomElement<ExampleComponents.infinite_scroll.Usage>("infinite-scroll-usage");
        options.RegisterCustomElement<ExampleComponents.lists.Usage>("lists-usage");
        options.RegisterCustomElement<ExampleComponents.markdown.usages.Usage>("markdown-usage");
        options.RegisterCustomElement<ExampleComponents.menus.Usage>("menus-usage");
        options.RegisterCustomElement<ExampleComponents.modals.Usage>("modals-usage");
        options.RegisterCustomElement<ExampleComponents.navigation_drawers.Usage>("navigation-drawers-usage");
        options.RegisterCustomElement<ExampleComponents.overlays.Usages.Usage>("overlays-usage");
        options.RegisterCustomElement<ExampleComponents.pagination.Usage>("pagination-usage");
        options.RegisterCustomElement<ExampleComponents.date_pickers.Usage>("date-pickers-usage");
        options.RegisterCustomElement<ExampleComponents.date_pickers_month.Usage>("date-pickers-month-usage");
        options.RegisterCustomElement<ExampleComponents.time_pickers.Usage>("time-pickers-usage");
        options.RegisterCustomElement<ExampleComponents.progress_circular.Usage>("progress-circular-usage");
        options.RegisterCustomElement<ExampleComponents.progress_linear.Usage>("progress-linear-usage");
        options.RegisterCustomElement<ExampleComponents.ratings.Usage>("ratings-usage");
        options.RegisterCustomElement<ExampleComponents.sheets.Usage>("sheets-usage");
        options.RegisterCustomElement<ExampleComponents.skeleton_loaders.Usages.Usage>("skeleton-loaders-usage");
        options.RegisterCustomElement<ExampleComponents.steppers.Usage>("steppers-usage");
        options.RegisterCustomElement<ExampleComponents.subheaders.Usage>("subheaders-usage");
        options.RegisterCustomElement<ExampleComponents.data_iterators.Usage>("data-iterators-usage");
        options.RegisterCustomElement<ExampleComponents.data_tables.Usage>("data-tables-usage");
        options.RegisterCustomElement<ExampleComponents.simple_tables.Usage>("simple-tables-usage");
        options.RegisterCustomElement<ExampleComponents.tabs.Usage>("tabs-usage");
        options.RegisterCustomElement<ExampleComponents.timelines.Usage>("timelines-usage");
        options.RegisterCustomElement<ExampleComponents.toast.Usages.Usage>("toast-usage");
        options.RegisterCustomElement<ExampleComponents.tooltips.Usage>("tooltips-usage");
        options.RegisterCustomElement<ExampleComponents.treeview.Usage>("treeview-usage");
        options.RegisterCustomElement<ExampleComponents.virtual_scroll.Usages.Usage>("virtual-scroll-usage");
        options.RegisterCustomElement<ExampleComponents.aspect_ratios.Usage>("aspect-ratios-usage");
        options.RegisterCustomElement<ExampleComponents.block_text.Usage>("block-text-usage");
        options.RegisterCustomElement<ExampleComponents.drawers.Usage>("drawers-usage");
        options.RegisterCustomElement<ExampleComponents.carousels.Usage>("carousels-usage");
        options.RegisterCustomElement<ExampleComponents.copyable_text.Usage>("copyable-text-usage");
    }
}
