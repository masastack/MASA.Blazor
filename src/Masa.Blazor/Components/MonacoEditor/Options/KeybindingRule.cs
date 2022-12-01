namespace Masa.Blazor;

public class KeybindingRule
{
    public int Keybinding { get; set; }
    public string Command { get; set; }
    public object CommandArgs { get; set; }

    public string When { get; set; }
}