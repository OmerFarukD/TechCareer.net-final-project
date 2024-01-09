using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
using BookWebApi.Models.Entities;
using BookWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Controller;
// okuma : HttpGet
// silme : HttpDelete , HttpPost
// ekleme : HttpPost
// güncelleme : HttpPatch, HttpPost, HttpPut
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        List<Book> books = _service.GetAll();
        return Ok(books);

    }

    [HttpGet("getbyid")]
    public IActionResult GetById([FromQuery] int id)
    {
        try
        {
            Book book = _service.GetById(id);
            return Ok(book);
        }catch (Exception ex)
        {
            return NotFound(ex.Message);
        }


    }

    [HttpPut("update")]
    public IActionResult Update([FromBody]BookUpdateRequestDto dto) 
    {
        try
        {
            _service.Update(dto);
            return Ok("Güncelleme başarılı");
        }catch(Exception ex)
        {
            return NotFound(ex.Message);
        }
 

    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] BookAddRequestDto dto)
    {
        _service.Add(dto);
        return Ok("Ekleme başarılı.");
    }

    [HttpDelete("delete")]
    public IActionResult Delete([FromQuery] int id)
    {
        try
        {
            _service.Delete(id);
            return Ok("Silme başarılı.");
        }catch (Exception ex)
        {
            return NotFound(ex.Message);
        }


    }

    [HttpGet("getalldetails")]
    public IActionResult GetAllDetails()
    {
        List<BookResponseDto> result = _service.GetAllDetails();
        return Ok(result);
    }

    [HttpGet("getdetailsbyid")]
    public IActionResult GetDetailsById([FromQuery] int id)
    {
        BookResponseDto result = _service.GetDetailsById(id);
        return Ok(result);
    }

    [HttpGet("getbycategoryid")]
    public IActionResult GetByCategoryId([FromQuery] int categoryId)
    {
        List<BookResponseDto> result = _service.GetByCategoryId(categoryId); 
        return Ok(result);
    }

    [HttpGet("getbyauthorid")]
    public IActionResult GetByAuthorId([FromQuery] int AuthorId)
    {
        List<BookResponseDto> result = _service.GetByAuthorId(AuthorId);
        return Ok(result);
    }

    [HttpGet("getbypricerange")]
    public IActionResult GetByPriceRangeDetails([FromQuery]double min, [FromQuery]double max)
    {
        List<BookResponseDto> result= _service.GetByPriceRangeDetails(min,max);
        return Ok(result);

    }

    [HttpGet("getbytitlecontains")]
    public IActionResult GetByTitleContains([FromQuery] string title)
    {
        List<BookResponseDto> result = _service.GetByTitleContains(title);
        return Ok(result);
    }


}
