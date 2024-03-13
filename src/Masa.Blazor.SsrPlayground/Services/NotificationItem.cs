using System.ComponentModel.DataAnnotations;

namespace Masa.Blazor.SsrPlayground.Services;

public class NotificationItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Message { get; set; }
}
