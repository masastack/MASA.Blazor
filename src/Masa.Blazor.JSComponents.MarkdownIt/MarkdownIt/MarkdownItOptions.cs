namespace Masa.Blazor;

public class MarkdownItOptions
{
    /// <summary>
    /// Enable HTML tags in source
    /// </summary>
    public bool Html { get; set; }

    /// <summary>
    /// Use '/' to close single tags (<br />).
    /// This is only for full CommonMark compatibility.
    /// </summary>
    public bool XHtmlOut { get; set; }

    /// <summary>
    /// Convert '\n' in paragraphs into br tag.
    /// </summary>
    public bool Breaks { get; set; }

    /// <summary>
    /// CSS language prefix for fenced blocks.
    /// Can be useful for external highlighters.
    /// </summary>
    public string? LangPrefix { get; set; } = "language-";

    /// <summary>
    /// Autoconvert URL-like text to links
    /// </summary>
    public bool Linkify { get; set; }

    /// <summary>
    /// Enable some language-neutral replacement + quotes beautification
    /// </summary>
    public bool Typographer { get; set; }

    /// <summary>
    /// Double + single quotes replacement pairs,
    /// when typographer enabled, and smartquotes on. 
    /// </summary>
    public string[]? Quotes { get; set; } = new string[] { "“", "”", "‘", "’" };
}
