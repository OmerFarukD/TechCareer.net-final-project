using AutoMapper;
using BookWebApi.Exceptions;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repository;
using BookWebApi.ReturnModels;
using BookWebApi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookWebApi.Service.Concrete;
//DRY - Dont Repeat Yourself
public class BookService : IBookService
{

    private readonly BaseDbContext _context;
    private readonly IMapper _mapper;

    public BookService(BaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //SİNGLE RESPONSİBLİTİY
    public ReturnModel<NoData> Add(BookAddRequestDto dto)
    {

        try
        {
            BookTitleMustBeUnique(dto.Title);

            Book book = _mapper.Map<Book>(dto);
            _context.Books.Add(book);
            _context.SaveChanges();

            return new ReturnModel<NoData>
            {
                Success = true,
                Message = "Kitap eklendi",
                StatusCode = HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new ReturnModel<NoData>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

  
    }

    public ReturnModel<NoData> Delete(int id)
    {
        try
        {
            BookIsPresent(id);

            Book? book = _context.Books.Find(id);

            _context.Books.Remove(book);
            _context.SaveChanges();


            return new ReturnModel<NoData>
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = $"İD : {id} ye ait kitap silindi. "
            };

        }
        catch (NotFoundException ex)
        {
            return new ReturnModel<NoData>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.NotFound
            };
        }


    }

    public ReturnModel<List<Book>> GetAll()
    {
        try
        {
            List<Book> books = _context.Books.ToList();
            BookListIsEmpty(books);

            return new ReturnModel<List<Book>>()
            {
                Data = books,
                Message = "Kitaplar Listelendi",
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (NotFoundException ex)
        {
            return new ReturnModel<List<Book>>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.NotFound
            };
        }


    }

    public ReturnModel<List<BookResponseDto>> GetAllDetails()
    {
        try
        {
            List<Book> books = _context.Books
    .Include(x => x.Author)
    .Include(x => x.Category)
    .ToList();
            BookListIsEmpty (books);

            List<BookResponseDto> responses = _mapper
                .Map<List<BookResponseDto>>(books);

            return new ReturnModel<List<BookResponseDto>>()
            {
                Data = responses,
                Message = "Detaylar listelendi",
                Success = true,
                StatusCode = HttpStatusCode.OK
            };

        }
        catch (NotFoundException ex)
        {

            return new ReturnModel<List<BookResponseDto>>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.NotFound
            };
        }



    }

    public ReturnModel<List<BookResponseDto>> GetByAuthorId(int AuthorId)
    {
        try
        {
            List<Book> books = _context.Books
       .Include(x => x.Author)
       .Include(x => x.Category)
       .Where(x => x.AuthorId == AuthorId)
       .ToList();

            BookListIsEmpty (books);

            List<BookResponseDto> responses = _mapper
                .Map<List<BookResponseDto>>(books);

            return new ReturnModel<List<BookResponseDto>>()
            {
                Data = responses,
                Message = $"AuthorId : {AuthorId} ye ait kitap getirildi.",
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (NotFoundException ex)
        {

            return new ReturnModel<List<BookResponseDto>>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.NotFound
            };
        }

   

    }

    public ReturnModel<List<BookResponseDto>> GetByCategoryId(int categoryId)
    {
        try
        {
            List<Book> books = _context.Books
.Include(x => x.Author)
.Include(x => x.Category)
.Where(x => x.CategoryId == categoryId)
.ToList();
            BookListIsEmpty(books);

            List<BookResponseDto> responses = _mapper
                .Map<List<BookResponseDto>>(books);

            return new ReturnModel<List<BookResponseDto>>()
            {
                Data = responses,
                Message = $"CategoryId : {categoryId} ait olan kitap getirildi",
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (NotFoundException ex)
        {
            return new ReturnModel<List<BookResponseDto>>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.NotFound
            };

        }

  
    }

    public  ReturnModel<Book> GetById(int id)
    {
        try
        {
            BookIsPresent(id);
            Book? book = _context.Books.Find(id);

            return new ReturnModel<Book>()
            {
                Data = book,
                Message = $"id si : {id} olan kitap getirildi.",
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (NotFoundException ex)
        {
            return new ReturnModel<Book>()
            {
                Message = ex.Message,
                Success = false,
                StatusCode = HttpStatusCode.NotFound
            };
        }

   

    }

    public ReturnModel<List<BookResponseDto>> GetByPriceRangeDetails(double min, double max)
    {
        List<Book> books = _context.Books
     .Include(x => x.Author)
     .Include(x => x.Category)
     .Where(x => x.Price<=max && x.Price>=min)
     .ToList();

        List<BookResponseDto> responses = _mapper
            .Map<List<BookResponseDto>>(books);

        return new ReturnModel<List<BookResponseDto>>()
        {
            Data = responses,
            StatusCode= HttpStatusCode.OK,
            Success = true,
            Message = $"Min : {min} ve Max : {max} değerindeki kitaplar listelendi."
        };
    }

    public ReturnModel<List<BookResponseDto>> GetByTitleContains(string title)
    {

        List<Book> books = _context.Books
 .Include(x => x.Author)
 .Include(x => x.Category)
 .Where(x => x.Title.Contains(title))
 .ToList();

        List<BookResponseDto> responses = _mapper
            .Map<List<BookResponseDto>>(books);

        return new ReturnModel<List<BookResponseDto>>()
        {
            Data= responses,
            StatusCode= HttpStatusCode.OK,
            Success = true,
            Message = "İlgili başlıkla eşleşen kitaplar listelendi."
        };
    }

    public ReturnModel<BookResponseDto> GetDetailsById(int id)
    {
        try
        {
            BookIsPresent(id);


            Book? book = _context.Books
                .Include(x => x.Author)
                .Include(x => x.Category)
                .SingleOrDefault(x => x.Id == id);


            BookResponseDto response = _mapper.Map<BookResponseDto>(book);
            return new ReturnModel<BookResponseDto>()
            {
                Data = response,
                Message = $"id : {id} ye ait detay sayfası getirildi.",
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (NotFoundException ex)
        {
            return new ReturnModel<BookResponseDto>()
            {
                Message = ex.Message,
                Success = false,
                StatusCode = HttpStatusCode.NotFound,
            };
        }




    }

    public ReturnModel<NoData> Update(BookUpdateRequestDto dto)
    {
        try
        {
            BookIsPresent(dto.Id);


            Book updatedBook = _mapper.Map<Book>(dto);

            _context.Books.Update(updatedBook);
            _context.SaveChanges();

            return new ReturnModel<NoData>()
            {
                Message = "Kitap Güncellendi",
                Success = true,
                StatusCode = HttpStatusCode.OK,

            };
        }
        catch (NotFoundException ex)
        {
            return new ReturnModel<NoData>()
            {
                Message = ex.Message,
                Success = false,
                StatusCode = HttpStatusCode.NotFound,

            };
        }


    }

    // Encapsulation
    private void BookIsPresent(int id)
    {
        var book = _context.Books.Any(x=> x.Id == id);
        if(book == false)
        {
            throw new NotFoundException($" id : {id} kitap bulunamadı.");
        }

    }

    private void BookTitleMustBeUnique(string title)
    {
        bool bookIsPresent = _context.Books.Any(x => x.Title == title);
        if (bookIsPresent == true)
        {
            throw new BusinessException("Kitap adı benzersiz olmalı : " + title);
        }
    }

    private void BookListIsEmpty(List<BookResponseDto> list)
    {
        if(list==null || list.Count == 0)
        {
            throw new NotFoundException("Kitaplar Bulunamadı.");
        }
    }

    private void BookListIsEmpty(List<Book> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new NotFoundException("Kitaplar Bulunamadı.");
        }
    }
}
