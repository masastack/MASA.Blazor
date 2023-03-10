namespace Masa.Blazor;

public class MonacoRegisterCompletionItemOptions
{
    public string Language { get; set; }

    public string[] TriggerCharacters { get; set; }

    public CompletionItem[] items { get; set; }
}