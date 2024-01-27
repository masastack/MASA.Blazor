namespace Masa.Docs.Shared.Pages;

public partial class FrameworkIntro
{
    IEnumerable<(string? Title, string? Href)>? _buildingBlocks;
    IEnumerable<(string? Title, string? Href)>? _utils;

    private static string gettingStarted =
        """
        **MASA Framework** 是一个基于 `.NET6.0` 的后端框架，可被用于开发 Web 应用程序、`WPF`、控制台项目，在结构上被分为三个部分:
          * BuildingBlocks：提供接口标准和串接不同构建块能力，降低耦合的同时保证主线逻辑
          * Contrib：基于构建块的接口标准提供最佳实践的实现，可以被替换
          * Utils：通用类库，提供底层通用能力，可被 `BuildingBlocks` 和 `Contrib` 所使用
        """;
    
    private static string tutorial =
        """
        模拟一个CRUD场景，演示如何使用MASA Framework进行开发，技术栈包括：
          * MinimalAPIs （最小API）：提供API服务 
          * MasaDbContext （数据上下文）：提供数据增删改查 
          * EventBus：事件总线 
          * CQRS：读写分离 
          * Caching: 缓存 
          * DDD：领域驱动设计
        """;

    protected override async Task OnInitializedAsync()
    {
        var navs = await DocService.ReadNavsAsync("framework");

        _buildingBlocks = GetNavs("building-blocks");
        _utils = GetNavs("utils");
        return;

        IEnumerable<(string? Title, string? Href)> GetNavs(string title)
        {
            return navs.FirstOrDefault(u => u.Title == title)?.Children?.Select(u => (u.Title, u.Href ?? u.Children?.FirstOrDefault()?.Href)) ??
                   new List<(string?, string?)>();
        }
    }
}
