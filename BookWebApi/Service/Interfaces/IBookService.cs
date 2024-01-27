using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
using BookWebApi.Models.Entities;
using BookWebApi.ReturnModels;

namespace BookWebApi.Service.Interfaces;

public interface IBookService
{
    ReturnModel<List<Book>> GetAll();
    ReturnModel<Book> GetById(int id);
    ReturnModel<NoData> Update(BookUpdateRequestDto dto);
    ReturnModel<NoData> Add(BookAddRequestDto dto);
    ReturnModel<NoData> Delete(int id);

   ReturnModel<List<BookResponseDto>> GetAllDetails();
   ReturnModel<BookResponseDto> GetDetailsById(int id);
   ReturnModel<List<BookResponseDto>> GetByCategoryId(int categoryId);
   ReturnModel<List<BookResponseDto>> GetByAuthorId(int AuthorId);
   ReturnModel<List<BookResponseDto>> GetByPriceRangeDetails(double min,double max);
   ReturnModel<List<BookResponseDto>> GetByTitleContains(string title);



}
