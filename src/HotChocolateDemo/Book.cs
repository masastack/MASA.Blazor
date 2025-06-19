namespace HotChocolateDemo;

public class Book
{
    public string Title { get; set; }

    public Author Author { get; set; }
}

public class Author
{
    public string Name { get; set; }
}

public record User(string Name, int Age, string Avatar, bool Done, DateTime Birthday, string[] FavoriteBooks, string Level, Position? Position = null);

public record Position(string Name);
