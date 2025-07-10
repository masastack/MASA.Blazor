# TemplateTable

`TemplateTable` 是一个功能强大的 Blazor 组件，旨在通过 GraphQL 快速轻松地呈现数据。它具有高度的可扩展性，允许开发人员集成不同的 GraphQL 提供程序，支持数据查询、分页、排序、筛选和用户视图管理等功能。

## 项目结构

- **Masa.Blazor.Components.TemplateTable**: `TemplateTable` 组件的核心，必须引用
- **Masa.Blazor.Components.TemplateTable.Contracts**: 提供必要的类，用于将特定数据传递给 `TemplateTable` 组件
- **Masa.Blazor.Components.TemplateTable.Abstractions**: 当您需要扩展其他 GraphQL 提供程序时，需要依赖此项目
- **Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL**: `Cubejs.GraphQL` 的实现
- **Masa.Blazor.Components.TemplateTable.HotChocolate**: `HotChocolate` 的实现

## 快速开始

### 查看演示

1. 导航到 `samples` 目录
2. 运行 `TemplateTableSample.AppHost` 项目以查看演示

### 安装和配置

1. 安装 NuGet 包：
   ```bash
   # 核心组件
   dotnet add package Masa.Blazor.Components.TemplateTable
   
   # 选择一个 GraphQL 提供程序
   dotnet add package Masa.Blazor.Components.TemplateTable.HotChocolate
   # 或者
   dotnet add package Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL
   ```

2. 在 `Program.cs` 中配置服务：
   ```csharp
   builder.Services.AddMasaBlazor()
       .AddGraphQLClientForTemplateTable("http://your-server/graphql")
       .WithHotChocolate(); // 或 .WithCubejs()
   ```

3. 在您的 `.razor` 文件中添加引用：
   ```csharp
   @using Masa.Blazor.Components.TemplateTable
   @using Masa.Blazor.Components.TemplateTable.Contracts
   ```

## 基本用法

```razor
<MTemplateTable Sheet="_sheet"
                UserViews="_userViews"
                OnUserViewAdd="@OnUserViewAdd"
                OnUserViewUpdate="@OnUserViewUpdate"
                OnUserViewDelete="@OnUserViewDelete"
                OnSave="@Save"
                CheckboxColor="primary">
</MTemplateTable>
```

```csharp
@code {
    private Sheet? _sheet;
    private IList<View> _userViews = [];

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // 加载表格配置和用户视图
            _sheet = await SheetService.GetSheetAsync();
            _userViews = await SheetService.GetUserViewsAsync("userId");
            StateHasChanged();
        }
    }

    private async Task OnUserViewAdd(View view)
    {
        await SheetService.AddUserViewAsync("userId", view);
    }

    private async Task OnUserViewDelete(View view)
    {
        await SheetService.UserViewDeleteAsync("userId", view);
    }

    private async Task OnUserViewUpdate(View view)
    {
        await SheetService.UpdateUserViewAsync("userId", view);
    }
}
```

## 插槽内容

- **ViewActionsContent**: 视图操作区域的自定义内容
- **RowActionsContent**: 行操作区域的自定义内容

## 扩展

`TemplateTable` 支持通过 `Masa.Blazor.Components.TemplateTable.Abstractions` 项目进行扩展。您可以通过实现自己的 GraphQL 提供程序来扩展其功能。

### 创建自定义 GraphQL 提供程序

1. 引用 `Masa.Blazor.Components.TemplateTable.Abstractions`
2. 实现相应的接口
3. 在 `Program.cs` 中注册您的提供程序

参考 `Cubejs.GraphQL` 和 `HotChocolate` 项目了解具体实现方式。

## 示例项目

在 `samples` 目录中包含以下示例：

- **HotChocolateDemo**: 演示如何与 HotChocolate GraphQL 服务器集成
- **TemplateTableSample.UI**: 完整的示例应用程序
- **TemplateTableSample.AppHost**: 用于运行演示的主机项目
