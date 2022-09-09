namespace Masa.Maui.Data.App.Todo.Dto;

public class TodoDto
{
    public int Id { get; set; }

    public bool IsChecked { get; set; }

    public bool IsImportant { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsDeleted { get; set; }

    [Required]
    public string Title { get; set; } = "";

    public string Assignee { get; set; } = "";

    public int Avatar { get; set; }

    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required]
    public List<string> Tag { get; set; } = new List<string>();

    public string? Description { get; set; }

    public TodoDto() { }

    public TodoDto(int id, bool isChecked, bool isImportant, bool isCompleted, bool isDeleted, string title, string assignee, int avatar, DateOnly dueDate, List<string> tags, string description)
    {
        Id = id;
        IsChecked = isChecked;
        IsImportant = isImportant;
        IsCompleted = isCompleted;
        IsDeleted = isDeleted;
        Title = title;
        Assignee = assignee;
        Avatar = avatar;
        DueDate = dueDate;
        Tag = tags;
        Description = description;
    }
}

