using AutoMapper;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
using BookWebApi.Models.Entities;

namespace BookWebApi.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookAddRequestDto,Book>();
        CreateMap<BookUpdateRequestDto, Book>();
        CreateMap<AuthorAddRequestDto,Author>();
        CreateMap<CategoryAddRequestDto,Category>();
        CreateMap<Book,BookResponseDto>();
    }
}
