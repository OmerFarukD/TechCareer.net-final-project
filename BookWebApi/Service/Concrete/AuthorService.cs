using AutoMapper;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repository;
using BookWebApi.Service.Interfaces;

namespace BookWebApi.Service.Concrete;
public class AuthorService : IAuthorService
{
    private readonly BaseDbContext _context;
    private readonly IMapper _mapper;

    public AuthorService(BaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Add(AuthorAddRequestDto dto)
    {
        Author author = _mapper.Map<Author>(dto);

        _context.Authors.Add(author);
        _context.SaveChanges();
    }

    public List<Author> GetAll()
    {
        List<Author> authors = _context.Authors.ToList();
        return authors;
    }

    public Author GetById(int id)
    {
        Author? author = _context.Authors.Find(id);
        if (author == null)
        {
            throw new Exception($"id : {id} Yazzar bulunamadı");
        }
        return author;
    }
}
