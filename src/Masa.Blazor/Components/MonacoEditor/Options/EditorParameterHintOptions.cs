namespace Masa.Blazor;

public class EditorParameterHintOptions
{
    /// <summary>
    /// Enable cycling of parameter hints.
    /// Defaults to false.
    /// </summary>
    public bool Cycle { get; set; }
    
    /// <summary>
    /// Enable parameter hints.
    /// Defaults to true.
    /// </summary>
    public bool Enabled { get; set; } = true;
}