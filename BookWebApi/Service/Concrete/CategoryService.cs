using AutoMapper;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repository;
using BookWebApi.Service.Interfaces;

namespace BookWebApi.Service.Concrete;

public class CategoryService : ICategoryService
{
    private readonly BaseDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(BaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Add(CategoryAddRequestDto dto)
    {
        Category category = _mapper.Map<Category>(dto);
        _context.Categories.Add(category);
        _context.SaveChanges();

    }

    public List<Category> GetAll()
    {
        List<Category> result = _context.Categories.ToList();
        return result;
    }

    public Category GetById(int id)
    {
        Category? category = _context
            .Categories
            .SingleOrDefault(x=>x.Id==id);

        if (category == null )
        {
            throw new Exception($"id :{id} , kategori bulunamadı");
        }

        return category;

    }
}
