using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
using BookWebApi.Models.Entities;

namespace BookWebApi.Service.Interfaces;

public interface IBookService
{
    List<Book> GetAll();
    Book GetById(int id);
    void Update(BookUpdateRequestDto dto);
    void Add(BookAddRequestDto dto);
    void Delete(int id);

    List<BookResponseDto> GetAllDetails();
    BookResponseDto GetDetailsById(int id);
    List<BookResponseDto> GetByCategoryId(int categoryId);
    List<BookResponseDto> GetByAuthorId(int AuthorId);
    List<BookResponseDto> GetByPriceRangeDetails(double min,double max);
    List<BookResponseDto> GetByTitleContains(string title);



}
