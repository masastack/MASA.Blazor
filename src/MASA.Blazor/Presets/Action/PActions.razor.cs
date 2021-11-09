using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Action = MASA.Blazor.Presets.Action;

namespace MASA.Blazor.Presets;

public partial class PActions
{
    private List<PAction> Actions { get; } = new();
    
    private double Width { get; set; }

    internal ActionTypes? Type { get; set; }

    [Inject]
    public DomEventJsInterop DomEventJsInterop { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public string Color { get; set; }

    [Parameter]
    public bool Depressed { get; set; }

    [Parameter]
    public bool Divider { get; set; }

    [Parameter]
    public List<Action> Items { get; set; }

    [Parameter]
    public JustifyTypes Justify { get; set; }

    [Parameter]
    public bool Large { get; set; }

    [Parameter]
    public bool Outlined { get; set; }

    [Parameter]
    public bool Plain { get; set; }

    [Parameter]
    public bool Rounded { get; set; }

    [Parameter]
    public bool Small { get; set; }

    [Parameter]
    public bool Text { get; set; }

    [Parameter]
    public bool Tile { get; set; }

    [Parameter]
    public bool XSmall { get; set; }

    [Parameter]
    public bool XLarge { get; set; }

    private IEnumerable<Action> VisibleItems => Items?.Where(item => item.Visible);

    private string JustifyClass => Justify switch
    {
        JustifyTypes.Center => "justify-center",
        JustifyTypes.End => "justify-end",
        JustifyTypes.Start => "justify-start",
        JustifyTypes.SpaceAround => "justify-space-around",
        JustifyTypes.SpaceBetween => "justify-space-between",
        JustifyTypes.None => ""
    };

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            DomEventJsInterop.ResizeObserver<Dimensions[]>(Ref, ObserveSizeChange);
        }

        return base.OnAfterRenderAsync(firstRender);
    }

    private async void ObserveSizeChange(Dimensions[] entries)
    {
        if (entries.Any())
        {
            await CheckWidths(entries[0].Width);
        }
    }

    internal async Task CheckWidths(double? width = null)
    {
        if (width.HasValue)
        {
            Width = width.Value;
        }

        var paddingWidth = Actions.Count * 8;
        var dividerWidth = Divider ? (Actions.Count - 1) * 8 + Actions.Count : 0;
        var labelBtnWidth = Actions.Sum(a => a.LabelBtnWidth) + paddingWidth + dividerWidth;
        // 增加1px是为了预防宽度与Width几乎相同时无法触发的问题
        // 例如在MTable中使用时，Width会根据列内容变化会导致Width的长度跟列内容一样
        var actionsBtnWidth = Actions.Sum(a => a.BtnWidth) + paddingWidth + dividerWidth + 1;

        Type = Width switch
        {
            double v when v > actionsBtnWidth => ActionTypes.IconLabel,
            double v when v > labelBtnWidth => ActionTypes.Label,
            _ => ActionTypes.Icon
        };

        if (width.HasValue)
        {
            await InvokeStateHasChangedAsync();
        }
    }

    public void Register(PAction item)
    {
        if (Actions.Contains(item)) return;

        Actions.Add(item);
    }

    public void Unregister(PAction item)
    {
        Actions.Remove(item);
    }
}