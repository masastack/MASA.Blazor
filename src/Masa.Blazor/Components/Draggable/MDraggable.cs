namespace Masa.Blazor
{
    public partial class MDraggable<Item> : BDragzone<Item>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var JsHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/draggable/sorttable-helper.js");
            await JsHelper.InvokeVoidAsync("init", Id,
                new
                {
                    group = "group",
                });
            base.OnAfterRenderAsync(firstRender);
        }

        //protected override void SetComponentClass()
        //{
        //    CssProvider
        //        .Apply("wrapper", css => { },
        //            style => { style.Add("position:relative;display:inherit;").Add(WrapperStyle); })
        //        .Apply(css =>
        //        {
        //            css.Add("m-border")
        //                .Add(() => Border switch
        //                {
        //                    Borders.Left => "m-border__left",
        //                    Borders.Right => "m-border__right",
        //                    Borders.Top => "m-border__top",
        //                    Borders.Bottom => "m-border__bottom",
        //                    _ => "",
        //                })
        //                .AddColor(Color)
        //                .AddRounded(Rounded);
        //        }, style =>
        //        {
        //            style
        //                .AddIf("display:none", () => Inactive)
        //                .AddIf(() =>
        //                {
        //                    var (number, unit) = ComputedWidth();
        //                    return number > 0 ? $"border-width:{number}{unit}" : "";
        //                }, () => Width != null)
        //                .AddIf(() =>
        //                {
        //                    var (number, unit) = ComputedWidth();
        //                    if (number <= 0) return "";

        //                    var calc = $"calc(1px - {number * 2}{unit})";

        //                    return Border switch
        //                    {
        //                        Borders.Left => $"left:{calc}",
        //                        Borders.Right => $"right:{calc}",
        //                        Borders.Top => $"top:{calc}",
        //                        Borders.Bottom => $"bottom:{calc}",
        //                        _ => "",
        //                    };
        //                }, () => Offset);
        //        });
        //}
    }
}
