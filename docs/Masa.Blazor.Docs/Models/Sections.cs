namespace Masa.Blazor.Docs;

public class Sections
{
    public string Code { get; set; }

    public string Language { get; set; }
    
    public Sections(string code, string language)
    {
        Code = code;
        Language = language;
    }
}
