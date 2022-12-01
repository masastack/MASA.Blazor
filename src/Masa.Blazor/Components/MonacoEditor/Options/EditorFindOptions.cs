namespace Masa.Blazor;

public class EditorFindOptions
{
    /// <summary>
    /// Controls whether the cursor should move to find matches while typing.
    /// </summary>
    public bool? CursorMoveOnType { get; set; }

    /// <summary>
    /// Controls if EditorFind in Selection flag is turned on in the editor.
    /// </summary>
    public bool AutoFindInSelection { get; set; }

    /// <summary>
    /// Controls if we seed search string in the EditorFind Widget with editor selection.
    /// 'never' | 'always' | 'selection'
    /// </summary>
    public string seedSearchStringFromSelection { get; set; }

    public bool AddExtraSpaceOnTop { get; set; }

    /// <summary>
    /// Controls whether the search automatically restarts from the beginning (or the end) when no further matches can be found
    /// </summary>
    public bool Loop { get; set; }
}