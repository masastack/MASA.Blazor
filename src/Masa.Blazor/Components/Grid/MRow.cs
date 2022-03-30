using Masa.Blazor.Extensions;

namespace Masa.Blazor
{
    public partial class MRow : BRow
    {
        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> AlignLg { get; set; }

        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> AlignMd { get; set; }

        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> AlignSm { get; set; }

        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> AlignXl { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContentLg { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContentMd { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContentSm { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContentXl { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> JustifyLg { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> JustifyMd { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> JustifySm { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> JustifyXl { get; set; }

        /// <summary>
        /// Removes the gutter between v-cols.
        /// </summary>
        [Parameter]
        public bool NoGutters { get; set; }

        /// <summary>
        /// Reduces the gutter between v-cols.
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("row")
                        .AddIf("no-gutters", () => NoGutters)
                        .AddIf("row--dense", () => Dense)
                        //'start', 'end', 'center', 'baseline', 'stretch'
                        .AddIf(Align.ToString(() =>
                                $"align-{Align}",
                            ("align-auto", AlignTypes.Auto),
                            ("align-start", AlignTypes.Start),
                            ("align-center", AlignTypes.Center),
                            ("align-end", AlignTypes.End),
                            ("align-baseline", AlignTypes.Baseline),
                            ("align-stretch", AlignTypes.Stretch)), () => Align != null)
                        .AddIf(AlignLg.ToString(() =>
                                $"align-lg-{AlignLg}",
                            ("align-lg-auto", AlignTypes.Auto),
                            ("align-lg-start", AlignTypes.Start),
                            ("align-lg-center", AlignTypes.Center),
                            ("align-lg-end", AlignTypes.End),
                            ("align-lg-baseline", AlignTypes.Baseline),
                            ("align-lg-stretch", AlignTypes.Stretch)), () => AlignLg != null)
                        .AddIf(AlignMd.ToString(() =>
                                $"align-md-{AlignMd}",
                            ("align-md-auto", AlignTypes.Auto),
                            ("align-md-start", AlignTypes.Start),
                            ("align-md-center", AlignTypes.Center),
                            ("align-md-end", AlignTypes.End),
                            ("align-md-baseline", AlignTypes.Baseline),
                            ("align-md-stretch", AlignTypes.Stretch)), () => AlignMd != null)
                        .AddIf(AlignSm.ToString(() =>
                                $"align-sm-{AlignSm}",
                            ("align-sm-auto", AlignTypes.Auto),
                            ("align-sm-start", AlignTypes.Start),
                            ("align-sm-center", AlignTypes.Center),
                            ("align-sm-end", AlignTypes.End),
                            ("align-sm-baseline", AlignTypes.Baseline),
                            ("align-sm-stretch", AlignTypes.Stretch)), () => AlignSm != null)
                        .AddIf(AlignXl.ToString(() =>
                                $"align-xl-{AlignXl}",
                            ("align-xl-auto", AlignTypes.Auto),
                            ("align-xl-start", AlignTypes.Start),
                            ("align-xl-center", AlignTypes.Center),
                            ("align-xl-end", AlignTypes.End),
                            ("align-xl-baseline", AlignTypes.Baseline),
                            ("align-xl-stretch", AlignTypes.Stretch)), () => AlignXl != null)
                        .AddIf(Justify.ToString(() =>
                                $"justify-{Justify}",
                            ("justify-start", JustifyTypes.Start),
                            ("justify-center", JustifyTypes.Center),
                            ("justify-end", JustifyTypes.End),
                            ("justify-space-between", JustifyTypes.SpaceBetween),
                            ("justify-space-around", JustifyTypes.SpaceAround)), () => Justify != null)
                        .AddIf(JustifyLg.ToString(() =>
                                $"justify-lg-{JustifyLg}",
                            ("justify-lg-start", JustifyTypes.Start),
                            ("justify-lg-center", JustifyTypes.Center),
                            ("justify-lg-end", JustifyTypes.End),
                            ("justify-lg-space-between", JustifyTypes.SpaceBetween),
                            ("justify-lg-space-around", JustifyTypes.SpaceAround)), () => JustifyLg != null)
                        .AddIf(JustifyMd.ToString(() =>
                                $"justify-md-{JustifyMd}",
                            ("justify-md-start", JustifyTypes.Start),
                            ("justify-md-center", JustifyTypes.Center),
                            ("justify-md-end", JustifyTypes.End),
                            ("justify-md-space-between", JustifyTypes.SpaceBetween),
                            ("justify-md-space-around", JustifyTypes.SpaceAround)), () => JustifyMd != null)
                        .AddIf(JustifySm.ToString(() =>
                                $"justify-sm-{JustifySm}",
                            ("justify-sm-start", JustifyTypes.Start),
                            ("justify-sm-center", JustifyTypes.Center),
                            ("justify-sm-end", JustifyTypes.End),
                            ("justify-sm-space-between", JustifyTypes.SpaceBetween),
                            ("justify-sm-space-around", JustifyTypes.SpaceAround)), () => JustifySm != null)
                        .AddIf(JustifyXl.ToString(() =>
                                $"justify-xl-{JustifyXl}",
                            ("justify-xl-start", JustifyTypes.Start),
                            ("justify-xl-center", JustifyTypes.Center),
                            ("justify-xl-end", JustifyTypes.End),
                            ("justify-xl-space-between", JustifyTypes.SpaceBetween),
                            ("justify-xl-space-around", JustifyTypes.SpaceAround)), () => JustifyXl != null)
                        .AddIf(AlignContent.ToString(() =>
                                $"align-content-{AlignContent}",
                            ("align-content-start", AlignContentTypes.Start),
                            ("align-content-center", AlignContentTypes.Center),
                            ("align-content-end", AlignContentTypes.End),
                            ("align-content-space-between", AlignContentTypes.SpaceBetween),
                            ("align-content-space-space-around", AlignContentTypes.SpaceAround),
                            ("align-content-space-stretch", AlignContentTypes.Stretch)), () => AlignContent != null)
                        .AddIf(AlignContentLg.ToString(() =>
                                    $"align-content-lg-{AlignContentLg}",
                                ("align-content-lg-start", AlignContentTypes.Start),
                                ("align-content-lg-center", AlignContentTypes.Center),
                                ("align-content-lg-end", AlignContentTypes.End),
                                ("align-content-lg-space-between", AlignContentTypes.SpaceBetween),
                                ("align-content-lg-space-space-around", AlignContentTypes.SpaceAround),
                                ("align-content-lg-space-stretch", AlignContentTypes.Stretch)),
                            () => AlignContentLg != null)
                        .AddIf(AlignContentLg.ToString(() =>
                                    $"align-content-lg-{AlignContentLg}",
                                ("align-content-lg-start", AlignContentTypes.Start),
                                ("align-content-lg-center", AlignContentTypes.Center),
                                ("align-content-lg-end", AlignContentTypes.End),
                                ("align-content-lg-space-between", AlignContentTypes.SpaceBetween),
                                ("align-content-lg-space-space-around", AlignContentTypes.SpaceAround),
                                ("align-content-lg-space-stretch", AlignContentTypes.Stretch)),
                            () => AlignContentLg != null)
                        .AddIf(AlignContentMd.ToString(() =>
                                    $"align-content-md-{AlignContentMd}",
                                ("align-content-md-start", AlignContentTypes.Start),
                                ("align-content-md-center", AlignContentTypes.Center),
                                ("align-content-md-end", AlignContentTypes.End),
                                ("align-content-md-space-between", AlignContentTypes.SpaceBetween),
                                ("align-content-md-space-space-around", AlignContentTypes.SpaceAround),
                                ("align-content-md-space-stretch", AlignContentTypes.Stretch)),
                            () => AlignContentMd != null)
                        .AddIf(AlignContentSm.ToString(() =>
                                    $"align-content-sm-{AlignContentSm}",
                                ("align-content-sm-start", AlignContentTypes.Start),
                                ("align-content-sm-center", AlignContentTypes.Center),
                                ("align-content-sm-end", AlignContentTypes.End),
                                ("align-content-sm-space-between", AlignContentTypes.SpaceBetween),
                                ("align-content-sm-space-space-around", AlignContentTypes.SpaceAround),
                                ("align-content-sm-space-stretch", AlignContentTypes.Stretch)),
                            () => AlignContentSm != null)
                        .AddIf(AlignContentXl.ToString(() =>
                                    $"align-content-xl-{AlignContentXl}",
                                ("align-content-xl-start", AlignContentTypes.Start),
                                ("align-content-xl-center", AlignContentTypes.Center),
                                ("align-content-xl-end", AlignContentTypes.End),
                                ("align-content-xl-space-between", AlignContentTypes.SpaceBetween),
                                ("align-content-xl-space-space-around", AlignContentTypes.SpaceAround),
                                ("align-content-xl-space-stretch", AlignContentTypes.Stretch)),
                            () => AlignContentXl != null);
                });
        }
    }
}