namespace Masa.Blazor;

public class CompletionItem
{
    /// <summary>
    /// 此完成项的标签。默认情况下
    /// 这也是在选择时插入的文本
    /// 此完成。
    /// </summary>
    public string? label { get; set; }

    /// <summary>
    /// 完成项的类型。根据种类
    /// 编辑器选择图标。
    /// </summary>
    public CompletionItemKind kind { get; set; }

    /// <summary>
    /// 具有附加信息的人类可读字符串
    /// 关于这个项目，比如类型或符号信息。
    /// </summary>
    public string? detail { get; set; }

    /// <summary>
    /// 表示文档注释的人类可读字符串。
    /// </summary>

    public string? Documentation { get; set; }

    /// <summary>
    /// 比较该项时应使用的字符串
    /// 与其他物品。当“falsy”{@link CompletionItem。标签标签}
    /// 使用。
    /// </summary>
    public string? sortText { get; set; }

    /// <summary>
    /// 属性集合筛选时应使用的字符串
    /// 完成项目。当“falsy”{@link CompletionItem。标签标签}
    /// 使用*。
    /// </summary>
    public string? filterText { get; set; }

    /// <summary>
    /// 显示时选择此项。*注意*只能选择一个完成项和
    /// 编辑器决定是哪个项目。规则是这些的* 第一*项
    /// 选择匹配最好的。
    /// </summary>
    public bool? preselect { get; set; }

    /// <summary>
    ///  选择时应插入到文档中的字符串或代码片段
    /// 此完成。
    /// </summary>
    public string? insertText { get; set; }

    /// <summary>
    ///  一个可选的字符集，当此补全激活时按下它将首先接受它
    /// 然后输入该字符。*注意*所有提交字符都应该有' length= 1 '和多余的
    /// 字符将被忽略。
    /// </summary>
    public string[]? commitCharacters { get; set; }

    public CompletionItem()
    {
    }

    public CompletionItem(string? label = null, CompletionItemKind kind = CompletionItemKind.Function,
        string? detail = null, string? documentation = null, string? sortText = null, string? filterText = null,
        bool? preselect = null, string insertText = null, string[]? commitCharacters = null)
    {
        this.label = label;
        this.kind = kind;
        this.detail = detail;
        Documentation = documentation;
        this.sortText = sortText;
        this.filterText = filterText;
        this.preselect = preselect;
        this.insertText = insertText;
        this.commitCharacters = commitCharacters;
    }
}

public class SingleEditOperation
{
}

public enum CompletionItemKind
{
    Method = 0,
    Function = 1,
    Constructor = 2,
    Field = 3,
    Variable = 4,
    Class = 5,
    Struct = 6,
    Interface = 7,
    Module = 8,
    Property = 9,
    Event = 10,
    Operator = 11,
    Unit = 12,
    Value = 13,
    Constant = 14,
    Enum = 15,
    EnumMember = 16,
    Keyword = 17,
    Text = 18,
    Color = 19,
    File = 20,
    Reference = 21,
    Customcolor = 22,
    Folder = 23,
    TypeParameter = 24,
    User = 25,
    Issue = 26,
    Snippet = 27
}