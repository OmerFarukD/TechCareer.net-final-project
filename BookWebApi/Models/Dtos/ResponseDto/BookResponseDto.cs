namespace BookWebApi.Models.Dtos.ResponseDto;

public class BookResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public string CategoryName { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

}
