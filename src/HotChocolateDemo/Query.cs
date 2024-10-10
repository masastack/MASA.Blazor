using Masa.Blazor.Components.TemplateTable;
using Masa.Blazor.Components.TemplateTable.ColumnConfigs;

namespace HotChocolateDemo;

public class Query
{
    [UseOffsetPaging]
    public IEnumerable<Book> GetBooks() =>
    [
        new Book() { Title = "C# in depth.", Author = new Author() { Name = "Jon Skeet" } },
        new Book() { Title = "Pro C# 7.", Author = new Author() { Name = "Andrew Troelsen" } },
        new Book() { Title = "CLR via C#.", Author = new Author() { Name = "Jeffrey Richter" } },
        new Book() { Title = "C# 9 and .NET 5.", Author = new Author() { Name = "Mark J. Price" } },
        new Book() { Title = "C# 9.0 in a Nutshell.", Author = new Author() { Name = "Joseph Albahari" } },
        new Book() { Title = "C# 9.0 Pocket Reference.", Author = new Author() { Name = "Joseph Albahari" } },
        new Book() { Title = "C# 9.0 All-in-One For Dummies.", Author = new Author() { Name = "John Paul Mueller" } },
        new Book() { Title = "C# 9.0 and .NET 5.0 Cookbook.", Author = new Author() { Name = "Dirk Strauss" } },
        new Book() { Title = "C# 9.0 Design Patterns.", Author = new Author() { Name = "Gaurav Aroraa" } },
        new Book()
        {
            Title = "C# 9.0 and .NET 5.0 Modern Cross-Platform Development.",
            Author = new Author() { Name = "Mark J. Price" }
        }
    ];

    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<User> GetUsers(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return _fakeUsers;
        }

        return _fakeUsers.Where(u => u.Name == name);
    }

    public Sheet GetSheet()
    {
        List<Column> fakeColumns =
        [
            new Column("Name", "Name", ColumnType.Text),
            new Column(id: "Age", name: "Age", type: ColumnType.Number),
            new Column(id: "Avatar", name: "Avatar", type: ColumnType.Image),
            new Column(id: "Done", name: "Done", type: ColumnType.Checkbox),
            new Column(id: "Birthday", name: "Birthday", type: ColumnType.Date),
            new Column(id: "FavoriteBooks", name: "Favorite Books", type: ColumnType.MultiSelect, new SelectConfig()
            {
                Color = true,
                Options =
                [
                    new SelectOption("C# in depth.", "C# in depth."),
                    new SelectOption("Pro C# 7.", "Pro C# 7."),
                    new SelectOption("CLR via C#.", "CLR via C#."),
                    new SelectOption("C# 9 and .NET 5.", "C# 9 and .NET 5."),
                    new SelectOption("C# 9.0 in a Nutshell.", "C# 9.0 in a Nutshell."),
                    new SelectOption("C# 9.0 Pocket Reference.", "C# 9.0 Pocket Reference."),
                    new SelectOption("C# 9.0 All-in-One For Dummies.", "C# 9.0 All-in-One For Dummies."),
                    new SelectOption("C# 9.0 and .NET 5.0 Cookbook.", "C# 9.0 and .NET 5.0 Cookbook."),
                    new SelectOption("C# 9.0 Design Patterns.", "C# 9.0 Design Patterns."),
                    new SelectOption("C# 9.0 and .NET 5.0 Modern Cross-Platform Development.",
                        "C# 9.0 and .NET 5.0 Modern Cross-Platform Development.")
                ]
            })
        ];

        List<ViewColumn> fakedViewColumns = fakeColumns.Select(u => new ViewColumn()
        {
            ColumnId = u.Id
        }).ToList();

        View fakeView = new()
        {
            Id = Guid.NewGuid(),
            Name = "Default",
            Columns = fakedViewColumns,
            RowHeight = RowHeight.Medium,
            Type = ViewType.Grid,
            HasActions = true,
            Filter = new Filter()
            {
                Options =
                [
                    // new FilterOption()
                    // {
                    //     ColumnId = "Name",
                    //     Func = "startsWith",
                    //     Expected = "J"
                    // }
                ]
            }
        };

        var fakeSheet = new Sheet()
        {
            Columns = fakeColumns,
            Views = [fakeView],
            ActiveViewId = fakeView.Id,
            DefaultViewId = fakeView.Id
        };

        return fakeSheet;
    }

    private static IEnumerable<User> _fakeUsers = new List<User>
    {
        new User("John", 20, "https://cdn.masastack.com/stack/images/website/masa-blazor/jack.png", true,
            new DateTime(2004, 1, 1), ["C# in depth.", "Pro C# 7."]),
        new User("Doe", 30, "https://cdn.masastack.com/stack/images/website/masa-blazor/doddgu.png", false,
            new DateTime(1994, 4, 12), []),
        new User("Jane", 25, "", true, new DateTime(1999, 5, 6), []),
        new User("Alice", 22, "https://cdn.masastack.com/stack/images/website/masa-blazor/marcus1.png", false,
            new DateTime(2002, 6, 7), ["CLR via C#.", "C# 9 and .NET 5."]),
        new User("Bob", 28, "", true, new DateTime(1996, 7, 8),
            ["C# 9.0 in a Nutshell.", "C# 9.0 Pocket Reference."]),
        new User("Eve", 30, "", false, new DateTime(1992, 8, 9), []),
        new User("Mallory", 45, "", true, new DateTime(1990, 9, 10), []),
        new User("Charlie", 35, "", false, new DateTime(1988, 10, 11), ["C# 9.0 All-in-One For Dummies."]),
        new User("David", 30, "", true, new DateTime(1986, 11, 12), []),
        new User("Frank", 50, "", false, new DateTime(1984, 12, 13), [])
    };
}