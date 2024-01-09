namespace BookWebApi.Models.Entities;

public class Author : Entity
{
    public string Name { get; set; }

    public List<Book> Books { get; set; }
}
