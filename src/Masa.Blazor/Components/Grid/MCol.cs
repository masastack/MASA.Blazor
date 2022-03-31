using Masa.Blazor.Extensions;
using System.Text.RegularExpressions;

namespace Masa.Blazor
{
    public partial class MCol : BCol
    {
        [Parameter]
        public StringNumber Sm { get; set; }

        [Parameter]
        public StringNumber Md { get; set; }

        [Parameter]
        public StringNumber Lg { get; set; }

        [Parameter]
        public StringNumber Xl { get; set; }

        /// <summary>
        /// 'auto', 'start', 'end', 'center', 'baseline', 'stretch'
        /// </summary>
        [Parameter]
        public
        StringEnum<AlignTypes> Align
        { get; set; }

        [Parameter]
        public StringNumber OrderLg { get; set; }

        [Parameter]
        public StringNumber OrderMd { get; set; }

        [Parameter]
        public StringNumber OrderSm { get; set; }

        [Parameter]
        public StringNumber OrderXl { get; set; }

        [Parameter]
        public StringNumber OffsetLg { get; set; }

        [Parameter]
        public StringNumber OffsetMd { get; set; }

        [Parameter]
        public StringNumber OffsetSm { get; set; }

        [Parameter]
        public StringNumber OffsetXl { get; set; }

        [Parameter]
        public StringNumber Flex { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .AddIf(() => $"col",
                            () => Cols == null || (Sm == null && Md == null && Lg == null && Xl == null))
                        .AddIf(() => $"col-{Cols.Value}", () => Cols != null)
                        .AddIf(() => $"col-sm-{Sm.Value}", () => Sm != null)
                        .AddIf(() => $"col-md-{Md.Value}", () => Md != null)
                        .AddIf(() => $"col-lg-{Lg.Value}", () => Lg != null)
                        .AddIf(() => $"col-xl-{Xl.Value}", () => Xl != null)
                        .AddIf(() => $"offset-{Offset.Value}", () => Offset != null)
                        .AddIf(() => $"offset-lg-{OffsetLg}", () => OffsetLg != null)
                        .AddIf(() => $"offset-md-{OffsetMd}", () => OffsetMd != null)
                        .AddIf(() => $"offset-sm-{OffsetSm}", () => OffsetSm != null)
                        .AddIf(() => $"offset-xl-{OffsetXl}", () => OffsetXl != null)
                        .AddIf(() => $"order-{Order.Value}", () => Order != null)
                        .AddIf(() => $"order-lg-{OrderLg}", () => OrderLg != null)
                        .AddIf(() => $"order-md-{OrderMd}", () => OrderMd != null)
                        .AddIf(() => $"order-sm-{OrderSm}", () => OrderSm != null)
                        .AddIf(() => $"order-xl-{OrderXl}", () => OrderXl != null)
                        .AddIf(Align.ToString(() =>
                                $"align-self-{Align}",
                            ("align-self-auto", AlignTypes.Auto),
                            ("align-self-start", AlignTypes.Start),
                            ("align-self-center", AlignTypes.Center),
                            ("align-self-end", AlignTypes.End),
                            ("align-self-baseline", AlignTypes.Baseline),
                            ("align-self-stretch", AlignTypes.Stretch)), () => Align != null)
                        .AddIf(SetHostFlexStyle, () => Flex != null);
                });
        }

        private string SetHostFlexStyle()
        {
            return this.Flex.Match(str =>
                {
                    if (Regex.Match(str, "^\\d+(\\.\\d+)?(px|em|rem|%)$").Success)
                    {
                        return $"flex: 0 0 {str}";
                    }

                    return $"flex: {str}";
                },
                num => $"flex: {num} {num} auto",
                _ => string.Empty);
        }
    }
}