using Azure;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
using BookWebApi.ReturnModels;
using BookWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Controller;
// okuma : HttpGet
// silme : HttpDelete , HttpPost
// ekleme : HttpPost
// güncelleme : HttpPatch, HttpPost, HttpPut


// Data
// Success
// Message
// StatusCode


[Route("api/[controller]")]
[ApiController]
public class BookController : BaseController
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {

        var responses = _service.GetAll();

        return ResponseForStatusCode(responses);

    }

    // 20.18 de dersteyiz
    [HttpGet("getbyid")]
    public IActionResult GetById([FromQuery] int id)
    {
 
        var response = _service.GetById(id);
        return ResponseForStatusCode(response);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody]BookUpdateRequestDto dto) 
    { 
        var response = _service.Update(dto);
        return ResponseForStatusCode(response);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] BookAddRequestDto dto)
    {
        var response = _service.Add(dto);
        return ResponseForStatusCode(response);
    }

    [HttpDelete("delete")]
    public IActionResult Delete([FromQuery] int id)
    {
        var response = _service.Delete(id);
        return ResponseForStatusCode(response);
    }

    [HttpGet("getalldetails")]
    public IActionResult GetAllDetails()
    {
       var result = _service.GetAllDetails();
        return ResponseForStatusCode(result);
    }

    [HttpGet("getdetailsbyid")]
    public IActionResult GetDetailsById([FromQuery] int id)
    {
        ReturnModel<BookResponseDto> result = _service.GetDetailsById(id);
        return ResponseForStatusCode(result);
    }

    [HttpGet("getbycategoryid")]
    public IActionResult GetByCategoryId([FromQuery] int categoryId)
    {
        var result = _service.GetByCategoryId(categoryId);
        return ResponseForStatusCode(result);
    }

    [HttpGet("getbyauthorid")]
    public IActionResult GetByAuthorId([FromQuery] int AuthorId)
    {
        var result = _service.GetByAuthorId(AuthorId);
        return ResponseForStatusCode(result);
    }

    [HttpGet("getbypricerange")]
    public IActionResult GetByPriceRangeDetails([FromQuery]double min, [FromQuery]double max)
    {
        var result= _service.GetByPriceRangeDetails(min,max);
        return ResponseForStatusCode(result);

    }

    [HttpGet("getbytitlecontains")]
    public IActionResult GetByTitleContains([FromQuery] string title)
    {
        var result = _service.GetByTitleContains(title);
        return ResponseForStatusCode(result);
    }


}
