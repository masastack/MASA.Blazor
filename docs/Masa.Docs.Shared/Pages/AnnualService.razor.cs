namespace Masa.Docs.Shared.Pages;

public partial class AnnualService
{
    private static readonly List<AnnualServiceData> s_services = new()
    {
        new AnnualServiceData(
            "MASA Blazor",
            42000,
            new[]
            {
                "入门培训、部署安装、使用问题、配置调优、线上故障、小版本升级",
                "服务级别：5x8小时（工作日9：00-18：00）",
                "响应时间：2小时",
                "服务方式：电话、邮件、远程、微信专属服务群",
                "额外：培训服务折扣、新版本优先体验、线下活动名额",
            },
            new Icon[] { "$masaBlazor" },
            "#4F33FF",
            "#F6F2FF",
            "#130067"),
        new AnnualServiceData(
            "MASA Framework",
            56000,
            new[]
            {
                "入门培训、部署安装、使用问题、配置调优、线上故障、小版本升级",
                "服务级别：5x8小时（工作日9：00-18：00）",
                "响应时间：2小时",
                "服务方式：电话、邮件、远程、微信专属服务群",
                "额外：培训服务折扣、新版本优先体验、线下活动名额",
            },
            new Icon[] { "$masaFramework" },
            "#D47195",
            "#FFF8F8",
            "#4F0329"),
        new AnnualServiceData(
            "MASA Stack",
            160000,
            new[]
            {
                "入门培训、部署安装、使用问题、配置调优、线上故障、季度巡检、小版本升级",
                "服务级别：5x8小时（工作日9：00-18：00）",
                "响应时间：2小时",
                "服务方式：电话、邮件、远程、微信专属服务群",
                "额外：培训服务折扣、新版本优先体验、线下活动名额",
                "7x24小时，半小时响应服务另单独洽谈",
            },
            new Icon[] { "$masaStack" },
            "#008864",
            "#E7FFF1",
            "#00513B",
            true),
        new AnnualServiceData(
            "MASA 全系列",
            258000,
            new[]
            {
                "入门培训、部署安装、使用问题、配置调优、线上故障、季度巡检、小版本升级",
                "服务级别：5x8小时（工作日9：00-18：00）",
                "响应时间：2小时",
                "服务方式：电话、邮件、远程、微信专属服务群",
                "额外：培训服务折扣、新版本优先体验、线下活动名额",
                "7x24小时，半小时响应服务另单独洽谈",
            },
            new Icon[] { "$masaStack", "$masaFramework", "$masaBlazor" },
            "#E39D30",
            "#FFF8F4",
            "#372100",
            true),
    };
}

public class AnnualServiceData
{
    public string Name { get; init; }

    public int Cost { get; init; }

    public string[] Items { get; init; }

    public Icon[] Icons { get; init; }

    public string PrimaryColor { get; init; }

    public string BackgroundColor { get; init; }

    public string TextColor { get; init; }

    public bool Stack { get; set; }

    public AnnualServiceData(string name, int cost, string[] items, Icon[] icons, string primaryColor, string backgroundColor,
        string textColor, bool stack = false)
    {
        Name = name;
        Cost = cost;
        Items = items;
        Icons = icons;
        PrimaryColor = primaryColor;
        BackgroundColor = backgroundColor;
        TextColor = textColor;
        Stack = stack;
    }
}
