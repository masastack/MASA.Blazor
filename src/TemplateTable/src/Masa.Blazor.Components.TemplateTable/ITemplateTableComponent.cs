namespace Masa.Blazor.Components.TemplateTable;

public interface ITemplateTableComponent
{
    RenderFragment RenderFilterDialog(I18n i18N, StringNumber width, bool show, EventCallback<bool> triggerShow, EventCallback<ModalActionEventArgs> onSave, EventCallback<ModalActionEventArgs> onCancel, RenderFragment child);

    RenderFragment RenderFilterDialogColumnInfo(I18n i18N, FilterModel filter, IList<ColumnInfo> columns, EventCallback<string> valueChanged, EventCallback<ValueTuple<ColumnInfo, bool>> onSelect);

    RenderFragment RenderFilterDialogFunc(I18n i18N, FilterModel filter, IList<StandardFilter> filters, EventCallback<StandardFilter> valueChanged, Func<StandardFilter, ColumnType, string> i18nFunc);

    RenderFragment RenderFilterDialogMultiValues(I18n i18N, FilterModel filter, EventCallback<List<string>> valueChanged);

    RenderFragment RenderFilterDialogValue(I18n i18N, FilterModel filter, EventCallback<string> valueChanged);

    RenderFragment RenderFilterDialogDateTimeValue(I18n i18N, FilterModel filter, EventCallback<DateTime?> valueChanged);

    RenderFragment RenderFilterDialogTextValue(I18n i18N, FilterModel filter, EventCallback<string> valueChanged);

    RenderFragment RenderFilterDialogAddButton(I18n i18N, EventCallback<MouseEventArgs> onClick);

    RenderFragment RenderFilterDialogRemoveButton(I18n i18N, FilterModel filter, EventCallback<MouseEventArgs> onClick);

    public RenderFragment RenderSortDialog(I18n i18N, StringNumber width, bool show, EventCallback<bool> triggerShow, EventCallback<ModalActionEventArgs> onSave, EventCallback<ModalActionEventArgs> onCancel, RenderFragment child);

    RenderFragment RenderSortDialogSortColumn(I18n i18N, SortModel sort, IList<ValueTuple<SortOrder, string>> columns, EventCallback<SortOrder> valueChanged);

    RenderFragment RenderSortDialogAddButton(I18n i18N, EventCallback<MouseEventArgs> onClick);

    RenderFragment RenderSortDialogRemoveButton(I18n i18N, EventCallback<MouseEventArgs> onClick);
}
