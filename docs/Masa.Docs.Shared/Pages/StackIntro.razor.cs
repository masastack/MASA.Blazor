namespace Masa.Docs.Shared.Pages;

public partial class StackIntro
{
    private static string installation =
        """
        - Docker compose
        
          ```bash
          git clone https://github.com/masastack/helm.git
          ```
          ```bash
          cd helm/docker
          ```
          ```bash
          docker-compose up
          ```
        - [Helm](/stack/masa-stack-1.0/installation/helm)
        """;

    private ProductIntro _auth = new(
        "auth",
        """负责所有产品的权限、菜单、用户等。它包含了单点登录、用户管理、RBAC3、第三方平台接入、Ldap等企业级功能。除了可以用在企业内部管理系统，它还可以帮助管理C端用户。""");

    private ProductIntro _dcc = new(
        "dcc",
        """在整个 MASA Stack 产品中担任所有系统以及部分全局综合配置的功能。DCC配置中心提供了有独立界面修改的配置后台，其中包含应用程序目前配置历史配置以及一些公共配置。应用程序支持多环境集群关系组合，同时支持发布历史、回滚、撤销、删除等功能。目前配置支持多种编码（Json、Properties、Raw、Xml、Yaml）可以对其编码进行加密处理，确保配置的安全性。"""       );

    private ProductIntro _pm = new(
        "pm",
        """底层基建项目管理产品，提供从0到1的初始化内容。它可以从最初的底层环境创建、部署和创建对应的集群开始。用户可以编辑环境与集群的组合关系，并在所需求的环境集群上创建项目。""");

    private ProductIntro _mc = new(
        "mc",
        """担任全局消息系统的支持者，支持多渠道的配置和消息发送规则的配置，并可以配置多种消息模板以及特定用户组群。此外，它还可以与关联产品 Alert、TSC 等对接，提供一站式的故障问题触发和处理解决方案。""");

    private ProductIntro _alert = new(
        "alert",
        """提供告警规则以及制定相关指标。它需要借助几个产品的基础功能组合来发挥它的价值，比如故障排查控制台作为监测数据源，调度中心作为调度周期控制，消息中心作为发送消息的渠道。MASA Stack 会尽可能地复用基础功能，而不是重复性工作，所以单独部署它将失去意义，只有与其他产品结合使用，才能发挥告警中心的最大价值。"""       );

    private ProductIntro _scheduler = new(
        "scheduler",
        """负责处理应用程序任务执行的调度，以及自动重试等相关操作。在 MASA Stack 产品中，与 MASA MC、MASA TSC、MASA Alert 3 款产品结合，发挥最大的调度价值。当然 Scheduler 并不只是给 MASA Stack 产品使用，它同样可以为业务创造价值。"""       );

    private ProductIntro _tsc = new(
        "tsc",
        """负责对 MASA 整个系统中的项目/应用进行监测来排查故障情况，其中包含从项目维度视角来查看监测的故障情况。以及溯源到具体的链路日志中去""");

    private IEnumerable<(string? Title, string? Href, string Intro)>? _products;

    protected override async Task OnInitializedAsync()
    {
        var navs = await DocService.ReadNavsAsync("stack");
        SetHref(_auth);
        SetHref(_dcc);
        SetHref(_pm);
        SetHref(_mc);
        SetHref(_alert);
        SetHref(_scheduler);
        SetHref(_tsc);

        return;

        void SetHref(ProductIntro product)
        {
            var authNav = navs?.FirstOrDefault(u => u.Title == product.Title);
            product.Href = authNav?.Href ?? authNav?.Children?.FirstOrDefault()?.Href;
        }
    }

    private class ProductIntro
    {
        public ProductIntro(string title, string intro)
        {
            Title = title;
            Intro = intro;
        }

        public string Title { get; init; }

        public string Intro { get; init; }

        public string? Href { get; set; }
    }
}
