#nullable enable
using System.ComponentModel.DataAnnotations;

namespace Masa.Blazor;

public class PromptValue
{
    [Required(ErrorMessage = "Please enter as prompted.")]
    public string? Value { get; set; }
}