namespace BookWebApi.Models.Dtos.RequestDto;

public class BookAddRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
}
