using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MCard : BCard, IMCard
    {
        /// <summary>
        /// 在卡片的左上角和右下角应用较大的边框半径
        /// </summary>
        [Parameter]
        public bool Shaped { get; set; }
        /// <summary>
        /// 移除卡片的海拔
        /// </summary>
        [Parameter]
        public bool Flat { get; set; }
        /// <summary>
        /// 悬停时将应用 4dp 的海拔（默认值为 2dp）
        /// </summary>
        [Parameter]
        public bool Hover { get; set; }
        /// <summary>
        /// 指定组件为链接。当使用 href 或 **to ** 属性时，这是自动的设置的。
        /// </summary>
        [Parameter]
        public bool Link { get; set; }
        /// <summary>
        /// 移除组件的单击或 target 功能。
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }
        /// <summary>
        /// 指定较高的默认海拔（8dp）
        /// </summary>
        [Parameter]
        public bool Raised { get; set; }
        /// <summary>
        /// 将指定的颜色应用于控件 - 它可以是 material color 的名称（例如 success 或者 purple）或 css 颜色 (#033 或 rgba(255, 0, 0, 0.5))
        /// </summary>
        [Parameter]
        public string Color { get; set; }
        /// <summary>
        /// 将暗色主题变量应用到组件
        /// </summary>
        [Parameter]
        public bool Dark { get; set; }
        /// <summary>
        /// 为组件设置浅色主题
        /// </summary>
        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }
        /// <summary>
        /// 指定卡片的背景图。对于更高级的实现，建议您使用MImage组件
        /// </summary>
        [Parameter]
        public string Img { get; set; }
        /// <summary>
        /// 组件的海拔可接受 0 到 24 之间的值
        /// </summary>
        [Parameter]
        public StringNumber Elevation { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }
        /// <summary>
        /// 指定加载器的高度
        /// </summary>
        [Parameter]
        public StringNumber LoaderHeight { get; set; } = 4;
        /// <summary>
        /// 进度条插槽
        /// </summary>
        [Parameter]
        public RenderFragment ProgressContent { get; set; }
        /// <summary>
        /// 显示线性进度条。可以是指定将哪种颜色应用于进度条的字符串（任何 material 色彩——主要（primary）, 次要（secondary）, 成功（success）, 信息（info），警告（warning），错误（error）），或者使用组件的布尔值 color（由色彩属性设置——如果它被组件支持的话）还可以是原色。
        /// <para>true/false：展示/不展示</para>
        /// </summary>
        [Parameter]
        public StringBoolean Loading { get; set; } = false;
        /// <summary>
        /// 配置在链接激活时应用的 CSS 类
        /// </summary>
        [Parameter]
        public string ActiveClass {get;set;}
        /// <summary>
        /// 设置 append 属性总是会附加到当前路径的相对路径上
        /// </summary>
        [Parameter]
        public bool Append {get;set;}
        /// <summary>
        /// 完全匹配链接。如果没有这个链接，‘/’ 将匹配每个路由
        /// </summary>
        [Parameter]
        public bool? Exact {get;set;}
        /// <summary>
        /// 精确匹配链接, 忽略query和hash部分
        /// </summary>
        [Parameter]
        public bool ExactPath {get;set;}
        /// <summary>
        /// 配置在精准链接激活时应用的 CSS 类
        /// </summary>
        [Parameter]
        public string ExactActiveClass {get;set;}
        /// <summary>
        /// 指定组件为锚点并应用 href 属性
        /// </summary>
        [Parameter]
        public object Href {get;set;}
        /// <summary>
        /// 表示链接的目标路由
        /// </summary>
        [Parameter]
        public object To {get;set;}
        /// <summary>
        /// 指定链接是 nuxt-link
        /// </summary>
        [Parameter]
        public bool Nuxt {get;set;}
        /// <summary>
        /// 设置 replace 属性会在点击时调用 router.replace() 而不是 router.push()，这样导航就不会留下历史记录
        /// </summary>
        [Parameter]
        public bool Replace {get;set;}
        /// <summary>
        /// 应用 MRipple 指令
        /// </summary>
        [Parameter]
        public object Ripple {get;set;}
        /// <summary>
        /// 指定 target 属性。只在使用 href 属性时应用
        /// </summary>
        [Parameter]
        public string Target {get;set;}

        public bool HasClick => OnClick.HasDelegate;

        protected override void SetComponentClass() => (this as IMCard).SetCardComponentClass();
    }
}